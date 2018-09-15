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
    private List<Texture> _loadedTexture; 
    
    public void ConfigClick()
    {
        FileBrowser.SetFilters( true, new FileBrowser.Filter( "Images", ".jpg", ".png" ), new FileBrowser.Filter( "Text Files", ".txt", ".pdf" ) );

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
        Debug.LogError(_loadedTexture);
        _loadedTexture.Add(fileDir.texture);
        Debug.LogError(_loadedTexture);
        ApplyLoadedPicToImageTexture();
        SaveFileToDataPath();
    }

    public void AddEntryClick()
    {
        Debug.LogError("AddEntry");
        var newEntry = Instantiate((GameObject)Resources.Load("Prefabs/ImageEntry"));
        newEntry.transform.parent = _uiCanvasGameObject.transform;
        _appliedImage.Add(newEntry.GetComponent<Image>());
    }
   
    private void SaveFileToDataPath()
    {
       // System.IO.File.WriteAllBytes();_appliedImage.material.mainTexture}
    }

    private void ApplyLoadedPicToImageTexture()
    {
        for (var i = 0; i < _appliedImage.Count; i ++)
        {
            if (_loadedTexture[i] != null)
            {
                _appliedImage[i].material = new Material(Shader.Find("Sprites/Default"));
                _appliedImage[i].material.mainTexture = _loadedTexture[i];
            }
        }
    }
    
}
