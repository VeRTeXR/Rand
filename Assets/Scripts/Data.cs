using System.Collections;
using UnityEngine;
using SimpleFileBrowser;

public class Data : MonoBehaviour
{
	public static Data instance = null;
	void Start () {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);
		
		DontDestroyOnLoad(gameObject);
	}

	
	
}
