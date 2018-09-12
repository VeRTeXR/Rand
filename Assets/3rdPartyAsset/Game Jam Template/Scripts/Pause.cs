using UnityEngine;

public class Pause : MonoBehaviour {
	
	private ShowPanels _showPanels;						
	public bool IsPaused;								
	private StartOptions _startScript;					

	void Awake()
	{
		_showPanels = GetComponent<ShowPanels> ();
		_startScript = GetComponent<StartOptions> ();
	}
	
	private void Update () {
		if (Input.GetButtonDown ("Cancel") && !IsPaused && !_startScript.InMainMenu) 
		{
			DoPause();
			_startScript.SetPlayerState(false);
			_showPanels.ShowPausePanel();
		} 
		else if (Input.GetButtonDown ("Cancel") && IsPaused && !_startScript.InMainMenu)
		{
			UnPause ();
			HidePausePanelAndEnablePlayerControl();
		}
	
	}

	private void HidePausePanelAndEnablePlayerControl()
	{
		_startScript.SetPlayerState(true);
		_showPanels.HidePausePanel();
	}


	public void DoPause()
	{
		IsPaused = true;
		Time.timeScale = 0;
	}


	public void UnPause()
	{
		IsPaused = false;
		HidePausePanelAndEnablePlayerControl();
		Time.timeScale = 1;
	}


}
