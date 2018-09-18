using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleFileBrowser;
using UnityEngine.UI;

[System.Serializable]
public class FileLoader : MonoBehaviour
{
    [SerializeField]
    private GameObject _uiCanvasGameObject;
    [SerializeField]
    private List<Image> _appliedImage;
    [SerializeField]
    private List<Sprite> _loadedSprites; 
    
    public void LoadImage()
    {
        FileBrowser.SetFilters( false, new FileBrowser.Filter( "Images", ".jpg", ".png" ), new FileBrowser.Filter( "Text Files", ".txt", ".pdf" ) );

        StartCoroutine("ShowLoadDialogCoroutine");
    } 
	
    IEnumerator ShowLoadDialogCoroutine()
    {
        // Show a load file dialog and wait for a response from user
        // Load file/folder: file, Initial path: default (Documents), Title: "Load File", submit button text: "Load"
        yield return FileBrowser.WaitForLoadDialog( false, null, "Load File", "Load" );

        // Dialog is closed
        // Print whether a file is chosen (FileBrowser.Success)
        // and the path to the selected file (FileBrowser.Result) (null, if FileBrowser.Success is false)
        Debug.Log( FileBrowser.Success + " " + FileBrowser.Result );

        WWW fileDir = new WWW("file://" + FileBrowser.Result);
        while (!fileDir.isDone)
        {
            yield return null;
        }
        Texture2D loadedTex = new Texture2D( fileDir.texture.width, fileDir.texture.height, TextureFormat.ARGB32, false);
       
        fileDir.LoadImageIntoTexture(loadedTex);
        Rect rec = new Rect(0, 0, loadedTex.width, loadedTex.height);
        var spriteToUse = Sprite.Create(loadedTex, rec, new Vector2(0.5f, 0.5f), 100);
        _loadedSprites.Add(spriteToUse); 
        ApplyLoadedPicToImageTexture();
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
        var newEntry = Instantiate((GameObject)Resources.Load("Prefabs/ImageEntry"));
        newEntry.transform.parent = _uiCanvasGameObject.transform;
        var imgComponent = newEntry.GetComponentInChildren<Image>();
        _appliedImage.Add(imgComponent);
    }
   

    private void ApplyLoadedPicToImageTexture()
    {
        for (var i = 0; i < _appliedImage.Count; i ++)
        {
            if (_loadedSprites[i] != null)
                _appliedImage[i].sprite = _loadedSprites[i];
           
        }
    }
    
}
