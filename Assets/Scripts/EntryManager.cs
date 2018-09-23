using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleFileBrowser;
using UnityEngine.UI;

public class EntryManager : MonoBehaviour
{
    [SerializeField] private GameObject _uiCanvasGameObject;
    [SerializeField] private List<Image> _appliedImage;
    [SerializeField] private List<Sprite> _loadedSprites;
    public List<ImageEntry> ImageEntry;

    public void LoadImage(int appliedImageIndex)
    {
        FileBrowser.SetFilters(false, new FileBrowser.Filter("Images", ".jpg", ".png"),
            new FileBrowser.Filter("Text Files", ".txt", ".pdf"));

        StartCoroutine(ShowLoadDialogCoroutine(appliedImageIndex));
    }

    public List<Image> GetAppliedImageList()
    {
        return _appliedImage;
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

        fileDir.LoadImageIntoTexture(loadedTex);
        Rect rec = new Rect(0, 0, loadedTex.width, loadedTex.height);
        var spriteToUse = Sprite.Create(loadedTex, rec, new Vector2(0.5f, 0.5f), 100);
        _loadedSprites.RemoveAt(appliedImageIndex);
        _loadedSprites.Insert(appliedImageIndex, spriteToUse);
        ApplyLoadedPicToImageTexture(appliedImageIndex);
    }

    public void DeleteEntry(GameObject entryGameObject)
    {
        var entryImgComponent = entryGameObject.GetComponentInChildren<Image>();
        if (entryImgComponent.sprite != null && _loadedSprites.Contains(entryImgComponent.sprite))
        {
            for (var i = 0; i < _loadedSprites.Count; i++)
            {
                if (entryImgComponent.sprite == _loadedSprites[i])
                    _loadedSprites.Remove(_loadedSprites[i]);
            }
        }
        if (_appliedImage.Contains(entryImgComponent))
        {
            for (var i = 0; i < _appliedImage.Count; i++)
            {
                if (entryImgComponent == _appliedImage[i])
                    _appliedImage.Remove(_appliedImage[i]);
            }
        }
        Destroy(entryGameObject);
    }

    public void AddEntryClick()
    {
        Debug.LogError("AddEntry");
        var newEntry = Instantiate((GameObject) Resources.Load("Prefabs/ImageEntry"));
        newEntry.transform.parent = _uiCanvasGameObject.transform;
        var imgComponent = newEntry.GetComponentInChildren<Image>();
        newEntry.GetComponent<ImageEntry>().SetEntryIndex(_appliedImage.Count);
        _appliedImage.Add(imgComponent);
        _loadedSprites.Add(null);
        ImageEntry.Add(newEntry.GetComponent<ImageEntry>());
    }


    private void ApplyLoadedPicToImageTexture(int appliedImageIndex)
    {
        if (_loadedSprites[appliedImageIndex] != null)
            _appliedImage[appliedImageIndex].sprite = _loadedSprites[appliedImageIndex];
    }
//
//    public void SaveData()
//    {
//        //https://unity3d.com/learn/tutorials/topics/scripting/persistence-saving-and-loading-data
//        BinaryFormatter binF = new BinaryFormatter();
//        
//        Debug.LogError(Application.persistentDataPath);
//        FileStream file = File.Open(Application.persistentDataPath + "/managerInfo.dat", FileMode.OpenOrCreate);
//        ManagerData dat = new ManagerData();
//        dat.entryImages = _appliedImage;
//        dat.LoadedSprites = _loadedSprites;
//        binF.Serialize(file, dat);
//        file.Close();
//    }
//
//    public void LoadData()
//    {
//        BinaryFormatter binF = new BinaryFormatter();
//        
//        Debug.LogError(Application.persistentDataPath);
//        FileStream file = File.Open(Application.persistentDataPath + "/managerInfo.dat", FileMode.Open);
//
//        ManagerData loadedData = (ManagerData)binF.Deserialize(file);
//        file.Close();        
//        
//    }

//    public void SaveDataClick()
//    {
//        Debug.LogError("save list");
//        SaveData();
//    }
    

}

//[Serializable]
//class ManagerData
//{
//    public List<Image> entryImages;
//    public List<Sprite> LoadedSprites;
//}