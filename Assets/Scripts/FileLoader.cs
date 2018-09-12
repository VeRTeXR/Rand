using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using  SimpleFileBrowser;
using UnityEditor;
using UnityEngine.UI;

[System.Serializable]
public class FileLoader : MonoBehaviour
{
    [SerializeField]
    private Image _appliedImage;
    private Texture _loadedTexture; 
    
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
        _loadedTexture = fileDir.texture;
        Debug.LogError(_loadedTexture);
        ApplyLoadedPicToImageTexture();
    }

    private void ApplyLoadedPicToImageTexture()
    {
        Debug.LogError("1 applied "+ _appliedImage.material);
        _appliedImage.material = new Material(Shader.Find("Sprites/Default"));
        Debug.LogError("2 applied "+ _appliedImage);
        _appliedImage.material.mainTexture = _loadedTexture;
    }
    
}
