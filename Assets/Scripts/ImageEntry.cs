using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageEntry : MonoBehaviour
{

	private int MaxCount;
	private int CurrentCount;
	public GameObject IncreaseButton;
	public GameObject DecreaseButton;

	void Start()
	{
		
	}


	public void DeleteEntry()
	{
		Destroy(gameObject);
		//Manager.instance.FileLoader
	}
	
	
	public void IncreaseCurrentCount()
	{
		CurrentCount++;
	}

	public void DecreaseCurrentCount()
	{
		CurrentCount--;
	}

	public int GetCurrentCount()
	{
		return CurrentCount;
	}

}
