using System.Collections.Generic;
using UnityEngine;

public class Randomizer : MonoBehaviour
{
	private List<ImageEntry> _currentEntries;
	private List<GameObject> _spiningWheelEntry;
	private int _currentIndex;


	void Awake ()
	{
		_currentEntries = null;
			PopulateEntryList();
	}

	private void PopulateEntryList()
	{
		if (_currentEntries == null)
		{
			var loopToIndex = Manager.instance.EntryManager.ImageEntry.Count / 3;
			_currentEntries = new List<ImageEntry>(loopToIndex);
			for (var i = 0; i < loopToIndex; i++)
			{
				if(Manager.instance.EntryManager.ImageEntry[i] != null)
					_currentEntries.Add(Manager.instance.EntryManager.ImageEntry[i]);
			}
//		_currentIndex = _currentIndex + _currentIndex;
//		Debug.LogError(_currentIndex);
		}
	}

	public List<ImageEntry> GetCurrentEntries()
	{
		return _currentEntries;
	}
}
