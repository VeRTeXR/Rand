using UnityEngine;

public class ShowPanels : MonoBehaviour {

	public GameObject OptionsPanel;							
	public GameObject OptionsTint;							
	public GameObject MenuPanel;							
	public GameObject PausePanel;							 
	public GameObject GameplayPanel;
	public GameObject ConfigPanel;

	public void ShowConfigPanel()
	{
		ConfigPanel.SetActive(true); 
	}

	public void HideConfigPanel()
	{
		ConfigPanel.SetActive(false);
	}
	
	public void ShowGameplayPanel()
	{
		GameplayPanel.SetActive(true);
	}

	public void HideGameplayPanel()
	{
		GameplayPanel.SetActive(false);
	}
	
	public void ShowOptionsPanel()
	{
		OptionsPanel.SetActive(true);
		OptionsTint.SetActive(true);
	}
	
	public void HideOptionsPanel()
	{
		OptionsPanel.SetActive(false);
		OptionsTint.SetActive(false);
	}

	public void ShowMenu()
	{
		MenuPanel.SetActive (true);
	}

	public void HideMenu()
	{
		MenuPanel.SetActive (false);
	}
	
	public void ShowPausePanel()
	{
		PausePanel.SetActive (true);
		OptionsTint.SetActive(true);
	}

	public void HidePausePanel()
	{
		PausePanel.SetActive (false);
		OptionsTint.SetActive(false);
	}
}
