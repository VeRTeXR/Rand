using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WheelController : MonoBehaviour
{
	public GameObject WheelEntryPrefab;
	public List<ImageEntry> RandomizedImageEntries;
	public List<WheelEntry> CurrentWheelEntries;
	public List<Transform> SpawnTransforms;
	public List<Image> imageList;
	public Randomizer Randomizer;

	private void Start()
	{
		Randomizer = GetComponent<Randomizer>();
		imageList = Manager.instance.EntryManager.GetAppliedImageList();
		RandomizedImageEntries = Randomizer.GetCurrentEntries();
		PopulateWheelEntries();
	}

	private void PopulateWheelEntries()
	{
		Debug.LogError("populate");
		for (var i = 0; i < RandomizedImageEntries.Count; i++)
			CreateWheelEntry(i);
	}

	private void CreateWheelEntry(int i)
	{
		Debug.LogError("create :: "+i);
		var wheelEntry = Instantiate(WheelEntryPrefab, SpawnTransforms[i]);
		if (imageList[i].sprite != null)
		{
			CurrentWheelEntries.Add(wheelEntry.GetComponent<WheelEntry>());
			CurrentWheelEntries[i].SetEntryImage(imageList[i].sprite);
		}
//		CurrentWheelEntries.Add(wheelEntry.GetComponent<WheelEntry>());
	}
}
