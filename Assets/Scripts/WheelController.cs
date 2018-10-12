﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WheelController : MonoBehaviour
{
	public GameObject WheelEntryPrefab;
	public List<ImageEntry> RandomizedImageEntries;
	public List<WheelEntry> CurrentWheelEntries;
	public List<Transform> SpawnTransforms;
	public List<Image> ImageList;
	public GameObject RotateObj;
	public Randomizer Randomizer;
	public GameObject RewardEntry;
	public GameObject SpinButton;
	public List<ConfettiGenerator> ConfettiGenerator;
	private int _currentIndex;
	
	
	private void Start()
	{
		Randomizer = GetComponent<Randomizer>();
		ImageList = Manager.instance.EntryManager.GetAppliedImageList();
		RandomizedImageEntries = Randomizer.GetCurrentEntries();
		PopulateWheelEntries(); 
	}

	public void PopulateWheelEntries()
	{
//		Debug.LogError("populate");
		for (var i = 0; i < RandomizedImageEntries.Count; i++)
		{
			CreateWheelEntry(i);
			_currentIndex++;
		}
	}

	public void RepopulateWheelEntries()
	{
		RandomizedImageEntries = Randomizer.GetCurrentEntries();
		for (var i = 0; i < RandomizedImageEntries.Count-1; i++)
		{
			Debug.LogError(i + "no");
			if (RandomizedImageEntries[i] != null)
			{
				if (RandomizedImageEntries[i].GetComponentInChildren<Image>().sprite != null)
					CurrentWheelEntries[i].SetEntryImage(RandomizedImageEntries[i].GetComponentInChildren<Image>().sprite);
				CurrentWheelEntries[i].SetLinkEntry(RandomizedImageEntries[i]);
			}
		}
	}

	public void StartWheelRotationAnimation(ImageEntry rewardEntry)
	{
		WheelEntry rotateToEntry = null;
		Debug.LogError("rewardEntry : "+rewardEntry);
		for (var i = 0; i < CurrentWheelEntries.Count; i++)
		{
			if (rewardEntry == CurrentWheelEntries[i].GetLinkEntry())
			{
				rotateToEntry = CurrentWheelEntries[i];
				Debug.LogError("rotateToEntry : "+rotateToEntry);
			}
			Debug.LogError(i);
		}
		if (rotateToEntry != null)
		{
			Debug.LogError(rotateToEntry.transform.parent.name);
			var rotateToIndex = rotateToEntry.transform.parent.GetComponent<SpawnPosition>().Id;
			Debug.LogError(rotateToIndex);
			float[] angleList =
			{
				0, 36, 72, 108, 144, 180, 216, 252, 288, 324, 360
			};
			var randomFinalAngle = angleList[rotateToIndex];
			var fullCircles = 5;
			var _finalAngle = fullCircles * 360 + randomFinalAngle;
			var rotateAnim = LeanTween.rotate(RotateObj, new Vector3(0, 0, _finalAngle), 5f).setEaseInOutElastic()
				.setOnComplete(() => CongratulationSequence(rotateToIndex));
		}
		else
		{
			Debug.LogError("RotateToEntry");
		}
		// reward entry seq done, respin 

	}

	private void CongratulationSequence(int randImageIndex )
	{
		for (var i = 0; i < ConfettiGenerator.Count; i++)
			ConfettiGenerator[i].ThrowConf();
		
		if(RandomizedImageEntries[randImageIndex]!=null)
		RewardEntry.GetComponent<RewardEntry>().SetRewardSprite(RandomizedImageEntries[randImageIndex].GetComponentInChildren<Image>().sprite);
	 	RewardEntry.GetComponent<RewardEntry>().StartAnimation();
	}

	public void ResetWheelState()
	{
		RepopulateWheelEntries();
		Randomizer.RewardSequenceFinished();
	}
	
	private void CreateWheelEntry(int i)
	{
		var wheelEntry = Instantiate(WheelEntryPrefab, SpawnTransforms[i]);
		CurrentWheelEntries.Add(wheelEntry.GetComponent<WheelEntry>());
		if (ImageList[i].sprite != null)
			CurrentWheelEntries[i].SetEntryImage(ImageList[i].sprite);
		CurrentWheelEntries[i].SetLinkEntry(RandomizedImageEntries[i]);
	}
}
