using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Randomizer : MonoBehaviour
{
	private List<ImageEntry> _currentEntries;
	private List<GameObject> _spiningWheelEntry;
	private ImageEntry outEntry;
	private int _currentIndex;


	void Awake ()
	{
		_currentEntries = null;
		PopulateEntryList();
	}
	
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.A)) 
			PopulateEntryList();

		if (Input.GetKeyDown(KeyCode.Q))
		{
			PickFromList();
		}
	}

	private void PickFromList()
	{
		var randIndex = 0;
		randIndex = Random.RandomRange(0, _currentEntries.Count);
		outEntry = _currentEntries[randIndex];
		outEntry.DecreaseCurrentEntryCount();
		Debug.LogError(outEntry.GetEntryIndex());
		PopulateEntryList();
	}

	public void PopulateEntryList()
	{
		_currentEntries = null; 
		_currentEntries = new List<ImageEntry>(10);
		var remainingAddingIndex = 10;
		while (remainingAddingIndex > 0)
		{
			_currentEntries.Add(Manager.instance.EntryManager.ImageEntry[_currentIndex]);
//			Debug.LogError("add : " + Manager.instance.EntryManager.ImageEntry[_currentIndex] + " : " + (_currentIndex));
			if (_currentIndex < Manager.instance.EntryManager.ImageEntry.Count - 1)
				_currentIndex++;
			else
				_currentIndex = 0;
			remainingAddingIndex--;
		}

	}

	public List<ImageEntry> GetCurrentEntries()
	{
		Debug.LogError("GetCurrentEntries");
		return _currentEntries;
	}
}
