using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using EZObjectPools;
using UnityEngine;
using SimpleFileBrowser;
using UnityEngine.UI;

public class EntryManager : MonoBehaviour
{
    [SerializeField] private GameObject _uiCanvasGameObject;
    [SerializeField] private List<Image> _appliedImage;
    [SerializeField] private List<Sprite> _loadedSprites;
    public List<ImageEntry> ImageEntry;
    public ConfigPanelController ConfigPanelController;
    public Sprite BackgroundImage;
    public Image AppliedBackgroundImage;
    public EZObjectPool ImageEntryPool;
    private int _entryCount;
    public List<string> PathList;
    
    public List<Image> GetAppliedImageList()
    {
        return _appliedImage;
    }

    public void LoadBackgroundClick()
    {
        FileBrowser.SetFilters(false, new FileBrowser.Filter("Images", ".jpg", ".png"));
        StartCoroutine(ShowLoadBackgroundDialogCoroutine());
    }

    private IEnumerator ShowLoadBackgroundDialogCoroutine()
    {
        yield return FileBrowser.WaitForLoadDialog(false, null, "Load File", "Load");

        Debug.Log(FileBrowser.Success + " " + FileBrowser.Result);

        WWW fileDir = new WWW("file://" + FileBrowser.Result);
        while (!fileDir.isDone)
        {
            yield return null;
        }
        Texture2D loadedTex = new Texture2D(fileDir.texture.width, fileDir.texture.height, TextureFormat.ARGB32, false);

        fileDir.LoadImageIntoTexture(loadedTex);
        Rect rec = new Rect(0, 0, loadedTex.width, loadedTex.height);
        var spriteToUse = Sprite.Create(loadedTex, rec, new Vector2(0.5f, 0.5f), 100);
        BackgroundImage = spriteToUse;
        ApplyLoadedSpriteToBackground(spriteToUse);
    }

    public void ApplyLoadedSpriteToBackground(Sprite loadedSprite)
    {
        if (AppliedBackgroundImage != null)
        {
            AppliedBackgroundImage.sprite = loadedSprite;
            AppliedBackgroundImage.color = Color.white;
        }
    }


    public void LoadImage(int appliedImageIndex)
    {
        FileBrowser.SetFilters(false, new FileBrowser.Filter("Images", ".jpg", ".png"));

        StartCoroutine(ShowLoadDialogCoroutine(appliedImageIndex));
    }

    IEnumerator ShowLoadDialogCoroutine(int appliedImageIndex)
    {
        yield return FileBrowser.WaitForLoadDialog(false, null, "Load File", "Load");

        Debug.Log(FileBrowser.Success + " " + FileBrowser.Result);

        WWW fileDir = new WWW("file://" + FileBrowser.Result);
        while (!fileDir.isDone)
        {
            yield return null;
        }
        Texture2D loadedTex = new Texture2D(fileDir.texture.width, fileDir.texture.height, TextureFormat.ARGB32, false);
        PathList.Add(FileBrowser.Result);
        fileDir.LoadImageIntoTexture(loadedTex);
        Rect rec = new Rect(0, 0, loadedTex.width, loadedTex.height);
        var spriteToUse = Sprite.Create(loadedTex, rec, new Vector2(0.5f, 0.5f), 100);
        Debug.LogError("applied Image Ind : " + appliedImageIndex);
        _loadedSprites.RemoveAt(appliedImageIndex);
        _loadedSprites.Insert(appliedImageIndex, spriteToUse);
        ApplyLoadedPicToImageTexture(appliedImageIndex);
    }

    public void DeleteEntry(GameObject entryGameObject)
    {
        var entryImageEntry = entryGameObject.GetComponent<ImageEntry>();
        var entryImgComponent = entryGameObject.GetComponentInChildren<Image>();
        if (entryImgComponent.sprite != null && _loadedSprites.Contains(entryImgComponent.sprite))
        {
            _loadedSprites.Remove(entryImgComponent.sprite);
        }
        if (_appliedImage.Contains(entryImgComponent))
        {
            _appliedImage.Remove(entryImgComponent);
        }
        if (ImageEntry.Contains(entryImageEntry))
        {
            ImageEntry.Remove(entryImageEntry);
        }
       
        Destroy(entryGameObject);
        
        if(ImageEntry.Count < 10)
            ConfigPanelController.SetStartButtonState(false);
        
    }


    public void AddEntryClick()
    {
        var newEntry = Instantiate((GameObject) Resources.Load("Prefabs/ImageEntry"));
        newEntry.transform.SetParent(_uiCanvasGameObject.transform);
        newEntry.transform.localScale = new Vector3( 1,1,1);
        var imgComponent = newEntry.GetComponentInChildren<Image>();
        _appliedImage.Add(imgComponent);
        _loadedSprites.Add(null);
        ImageEntry.Add(newEntry.GetComponent<ImageEntry>());
        
        if(ImageEntry.Count > 10)
            ConfigPanelController.SetStartButtonState(true);
    }


    private void ApplyLoadedPicToImageTexture(int appliedImageIndex)
    {
        if (_loadedSprites[appliedImageIndex] != null)
            _appliedImage[appliedImageIndex].sprite = _loadedSprites[appliedImageIndex];
    }

    private void LoadEntry() {}
    
    
    public void SaveData()
    {
        //https://unity3d.com/learn/tutorials/topics/scripting/persistence-saving-and-loading-data
        BinaryFormatter binF = new BinaryFormatter();
        
        Debug.LogError(Application.persistentDataPath);
        FileStream file = File.Create(Application.persistentDataPath + "/managerInfo.dat");
        ManagerData dat = new ManagerData();
        dat.EntryCount = ImageEntry.Count;
        dat.EntryPathList = PathList;
        
        binF.Serialize(file, dat);
        file.Close();
    }
    
    public void LoadData()
    {
        BinaryFormatter binF = new BinaryFormatter();
        
        FileStream file = File.Open(Application.persistentDataPath + "/managerInfo.dat", FileMode.Open);

        ManagerData loadedData = (ManagerData)binF.Deserialize(file);
        for (var i = 0; i < loadedData.EntryCount; i++)
        {
            AddEntryClick();
        }

        for (var i = 0; i < loadedData.EntryPathList.Count; i++)
        {
            WWW fileDir = new WWW("file://" +loadedData.EntryPathList[i]);
            Texture2D loadedTex = new Texture2D(fileDir.texture.width, fileDir.texture.height, TextureFormat.ARGB32, false);
            fileDir.LoadImageIntoTexture(loadedTex);
            Rect rec = new Rect(0, 0, loadedTex.width, loadedTex.height);
            var spriteToUse = Sprite.Create(loadedTex, rec, new Vector2(0.5f, 0.5f), 100);
            _loadedSprites.RemoveAt(i);
            _loadedSprites.Insert(i, spriteToUse);
            ApplyLoadedPicToImageTexture(i);
            
        }
        file.Close();           
    }
}

[Serializable]
class ManagerData
{
    public int EntryCount;
    public List<string> EntryPathList;
}