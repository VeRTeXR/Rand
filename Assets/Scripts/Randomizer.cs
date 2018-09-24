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
			var loopToIndex = Manager.instance.EntryManager.ImageEntry.Count;
			_currentEntries = new List<ImageEntry>(10);
			for (var i = 0; i < 10; i++)
			{
				if (Manager.instance.EntryManager.ImageEntry[i] != null)
				{
					_currentEntries.Add(Manager.instance.EntryManager.ImageEntry[i]);
					Debug.LogError("add : "+Manager.instance.EntryManager.ImageEntry[i]);
					if (_currentIndex < Manager.instance.EntryManager.ImageEntry.Count)
						_currentIndex++;
					else
						_currentIndex = 0;
				}
			}
		}
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.A)) 
			RepopulateEntryList();
	}
	
	public void RepopulateEntryList()
	{
		_currentEntries = null; 
		_currentEntries = new List<ImageEntry>(10);
		for (var i = 0; i < _currentIndex; i++)
		{
			_currentEntries.Add(Manager.instance.EntryManager.ImageEntry[_currentIndex + i]);
			Debug.LogError("add : "+Manager.instance.EntryManager.ImageEntry[_currentIndex + i]+" : "+(_currentIndex+i));
			if (_currentIndex < Manager.instance.EntryManager.ImageEntry.Count)
				_currentIndex++;
			else
				_currentIndex = 0;
		}
		
	}

	public List<ImageEntry> GetCurrentEntries()
	{
		Debug.LogError("GetCurrentEntries");
		return _currentEntries;
	}
}
