using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayPanelController : MonoBehaviour
{

	public ShowPanels ShowPanels;
	private bool _inputAvailable;

	private void Start()
	{
		_inputAvailable = true;
	}

	public void SetInputAvailability(bool isEnable)
	{
		_inputAvailable = isEnable;
	} 
	
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.A) && _inputAvailable)
		{
			Debug.LogError("going Back");
			GoBackToConfig();
		}
	}

	private void GoBackToConfig()
	{
		if (ShowPanels != null)
		{
			ShowPanels.HideGameplayPanel(); 
			ShowPanels.ShowConfigPanel();
			_inputAvailable = false;
		}
	}
}
