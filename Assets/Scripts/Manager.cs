using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour {
	
	public static Manager instance = null; 
	public GameObject StartMenu;
	public Pause PauseScript;


	void Start () {
		
		if (instance == null) {
			instance = this;
		}
		else if (instance != this){
			Destroy (gameObject);
		}

		DontDestroyOnLoad (gameObject);
		StartMenu = GameObject.FindGameObjectWithTag("StartMenu");
		if (StartMenu != null)
		{
			PauseScript = StartMenu.GetComponent<Pause>();
		}
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
