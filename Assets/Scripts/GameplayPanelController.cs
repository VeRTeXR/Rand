using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayPanelController : MonoBehaviour
{

	public ShowPanels ShowPanels;
	private bool _inputAvailable;
	public Image BackgroundImage;
	public WheelController WheelController;

	private void Start()
	{
		BackgroundImage.sprite = Manager.instance.EntryManager.BackgroundImage;
		BackgroundImage.color = Color.white;
		_inputAvailable = true;
		WheelController = GetComponentInChildren<WheelController>();
	}

	private void OnEnable()
	{
		BackgroundImage.sprite = Manager.instance.EntryManager.BackgroundImage;
		BackgroundImage.color = Color.white;
		Debug.LogError("enabler");
		if (WheelController != null)
			WheelController.Reinit();
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
