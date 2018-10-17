using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigPanelController : MonoBehaviour
{

    public GameObject StartButton;
    public GameObject LoadButton;
    
    public void SetStartButtonState(bool isEnable)
    {
        StartButton.SetActive(isEnable);
    }

    public void SetLoadButtonState(bool isEnable)
    {
        LoadButton.SetActive(isEnable);
    }
    
}       
