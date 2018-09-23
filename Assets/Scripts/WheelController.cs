using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WheelController : MonoBehaviour
{
	public GameObject WheelEntryPrefab;
	public List<WheelEntry> CurrentWheelEntries;
	public List<Transform> SpawnTransforms;
	public List<Image> imageList;

	private void Start()
	{
		imageList = Manager.instance.EntryManager.GetAppliedImageList();
		Debug.LogError(imageList);
		PopulateWheelEntries();
	}

	private void PopulateWheelEntries()
	{
		Debug.LogError("populate Wheel Entry");
		for (var i = 0; i < Manager.instance.EntryManager.ImageEntry.Count; i++)
			CreateWheelEntry(i);
	}

	private void CreateWheelEntry(int i)
	{
		var wheelEntry = Instantiate(WheelEntryPrefab, SpawnTransforms[i]);
		if (imageList[i].sprite != null)
		{
			Debug.LogError(imageList[i].sprite);
			CurrentWheelEntries.Add(wheelEntry.GetComponent<WheelEntry>());
			Debug.LogError(CurrentWheelEntries[i].name);
			CurrentWheelEntries[i].SetEntryImage(imageList[i].sprite);
		}
		CurrentWheelEntries.Add(wheelEntry.GetComponent<WheelEntry>());
	}
}
