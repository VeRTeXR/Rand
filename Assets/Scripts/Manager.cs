using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour {
	
	public static Manager instance = null; 
	public GameObject StartMenu;
	public Pause PauseScript;
	public FileLoader FileLoader;


	void Start () {
		
		if (instance == null) {
			instance = this;
		}
		else if (instance != this){
			Destroy (gameObject);
		}

		DontDestroyOnLoad (gameObject);
		FileLoader = GetComponent<FileLoader>();
	}

	void OnLevelWasLoaded(int index)  {
		FindObjectOfType<Score> ().Save (); // save everything on level load
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.Z)) {
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
	}



}
