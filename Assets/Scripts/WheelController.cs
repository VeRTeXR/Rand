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
	public List<Image> imageList;
	public Randomizer Randomizer;
	private int _currentIndex;
	
	
	private void Start()
	{
		Randomizer = GetComponent<Randomizer>();
		imageList = Manager.instance.EntryManager.GetAppliedImageList();
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
		if (curIndex > imageList.Count)
			curIndex = 0;
		RandomizedImageEntries = Randomizer.GetCurrentEntries();
		for (var i = 0; i < CurrentWheelEntries.Count; i++)
		{
			CurrentWheelEntries[i].SetEntryImage(imageList[curIndex+i].sprite);
		}
	}
	
//	public void RepopulateWheelEntries(int index)
//	{
//		_currentIndex = index;
//		RandomizedImageEntries = Randomizer.GetCurrentEntries();
//		var remainingAddingIndex = 10;
//		var i = 0;
//		while (remainingAddingIndex > 0)
//		{
//			Debug.LogError("repopulating : "+i);
//			CurrentWheelEntries[i].SetEntryImage(imageList[_currentIndex].sprite);
//			if (_currentIndex < Manager.instance.EntryManager.ImageEntry.Count - 1)
//				_currentIndex++;
//			else
//				_currentIndex = 0;
//			i++;
//			remainingAddingIndex--;
//		}
//	}
//	
	private void CreateWheelEntry(int i)
	{
		Debug.LogError("create :: "+i);
		var wheelEntry = Instantiate(WheelEntryPrefab, SpawnTransforms[i]);
		
		CurrentWheelEntries.Add(wheelEntry.GetComponent<WheelEntry>());
		if (imageList[i].sprite != null)
		{
			CurrentWheelEntries[i].SetEntryImage(imageList[i].sprite);
		}
	}
}
