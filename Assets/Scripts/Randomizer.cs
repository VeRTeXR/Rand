using System.Collections.Generic;
using UnityEngine;

public class Randomizer : MonoBehaviour
{
	private List<ImageEntry> _currentEntries;
	private List<GameObject> _spiningWheelEntry;
	private int _currentIndex;
	public GameObject WheelEntryTemplate;

	public Randomizer(List<ImageEntry> currentEntries)
	{
		_currentEntries = currentEntries;
	}

	void Start () {
		if (_currentEntries == null)
		{
			PopulateEntryList();
		}
	}

	private void PopulateEntryList()
	{
		for (var i = 0; i < Manager.instance.EntryManager.ImageEntry.Count / 3; i++)
		{
			_currentEntries.Add(Manager.instance.EntryManager.ImageEntry[i]);
			GenerateSpiningWheelEntry();
		}
		
	}

	private void GenerateSpiningWheelEntry()
	{
		var entryInstance = Instantiate(WheelEntryTemplate);
		entryInstance.GetComponent<WheelEntry>();
		throw new System.NotImplementedException();
	}

	// Update is called once per frame
	void Update () {
		
	}
}
