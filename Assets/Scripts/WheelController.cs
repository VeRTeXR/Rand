using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.NetworkSystem;
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
		Debug.LogError("populate");
		for (var i = 0; i < RandomizedImageEntries.Count; i++)
		{
			CreateWheelEntry(i);
			_currentIndex++;
		}
	}

	public void RepopulateWheelEntries()
	{
		
		//shit is wrong
		RandomizedImageEntries = Randomizer.GetCurrentEntries();
		for (var i = 0; i < CurrentWheelEntries.Count; i++)
		{	
			CurrentWheelEntries[i].SetEntryImage(RandomizedImageEntries[i].GetComponentInChildren<Image>().sprite);
		}
		Debug.LogError("Repopulate Wheel Entries :  " +_currentIndex);
	}

	public void StartWheelRotationAnimation(int randEntryIndex)
	{
		Debug.LogError( "startWheelRotationRotateTo::: "+randEntryIndex);
		var _startAngle = this.transform.eulerAngles.z;
		float[] angleList = new float[]
		{
			0, 36, 72, 108, 144, 180, 216, 252, 288, 324, 360
		};
		var randomFinalAngle = angleList[randEntryIndex];
		var fullCircles = 5;
		var _finalAngle = fullCircles * 360 + randomFinalAngle;
		var rotateAnim = LeanTween.rotate(RotateObj, new Vector3(0, 0, _finalAngle), 5f).setEaseInOutElastic()
			.setOnComplete(() => CongratulationSequence(randEntryIndex));

		// reward entry seq done, respin 

	}

	private void CongratulationSequence(int randImageIndex )
	{
		
		Debug.LogError("randImageIndex : "+ randImageIndex);
		for (var i = 0; i < ConfettiGenerator.Count; i++)
		{
			ConfettiGenerator[i].ThrowConf();
		}	
		RewardEntry.GetComponent<RewardEntry>().SetRewardSprite(RandomizedImageEntries[randImageIndex].GetComponentInChildren<Image>().sprite);
		RewardEntry.GetComponent<RewardEntry>().StartAnimation();
	}

	public void ResetWheelState()
	{
		Debug.LogError("resetWheelState");
		//turn on the arrow
		RepopulateWheelEntries();
		Randomizer.RewardSequenceFinished();
	}
	
	private void CreateWheelEntry(int i)
	{
		var wheelEntry = Instantiate(WheelEntryPrefab, SpawnTransforms[i]);
		CurrentWheelEntries.Add(wheelEntry.GetComponent<WheelEntry>());
		if (ImageList[i].sprite != null)
		{
			CurrentWheelEntries[i].SetEntryImage(ImageList[i].sprite);
		}
	}
}
