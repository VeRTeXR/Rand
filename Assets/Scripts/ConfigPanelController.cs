using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigPanelController : MonoBehaviour
{

    public GameObject StartButton;

    public void SetStartButtonState(bool isEnable)
    {
        StartButton.SetActive(isEnable);
    }
    
}       
