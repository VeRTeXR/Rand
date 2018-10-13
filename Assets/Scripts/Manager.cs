using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{

	public static Manager instance = null;
	public StartOptions StartOptions;
	public Pause PauseScript;
	public EntryManager EntryManager;


	void Start()
	{

		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			Destroy(gameObject);
		}

		DontDestroyOnLoad(gameObject);
		EntryManager = GetComponent<EntryManager>();
		StartOptions = FindObjectOfType<StartOptions>();
	}
}


