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
			CreateWheelEntry(i);
	}

	public void DepopulateWheelEntries(int curIndex)
	{
		if (curIndex > ImageList.Count-1)
			curIndex = 0;
		RandomizedImageEntries = Randomizer.GetCurrentEntries();
		for (var i = 0; i < CurrentWheelEntries.Count; i++)
		{
			var imgIndex = curIndex + i;
			if (imgIndex > ImageList.Count-1)
				imgIndex = 0;
			else
				imgIndex = curIndex + i;
			CurrentWheelEntries[i].SetEntryImage(ImageList[imgIndex].sprite);
		}
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
		
//		LeanTween.moveX(cubeToRemove, -5.5f, 1f).setOnComplete(() => RemoveMe(foo.id, cubeToRemove));
		//launch reward entry seq 
		// reward entry seq done, respin 

	}

	private void CongratulationSequence(int randImageIndex )
	{
		Debug.LogError("randImageIndex : "+ randImageIndex);
		RewardEntry.GetComponent<RewardEntry>().SetRewardSprite(ImageList[randImageIndex].sprite);
		RewardEntry.GetComponent<RewardEntry>().StartAnimation();
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
