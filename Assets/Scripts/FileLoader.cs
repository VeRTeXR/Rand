using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  SimpleFileBrowser;

public class FileLoader : MonoBehaviour {

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
    }
}
