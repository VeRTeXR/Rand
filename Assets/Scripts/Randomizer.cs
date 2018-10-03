using System.Collections.Generic;
using UnityEngine;

public class Randomizer : MonoBehaviour
{
	private List<ImageEntry> _currentEntries;
	private List<GameObject> _spiningWheelEntry;
	private ImageEntry _outEntry;
	private int _currentIndex;
	private WheelController _wheelController;
	private GameObject _rewardEntry;
	
	void Awake ()
	{
		_wheelController = GetComponent<WheelController>();
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

	public void OnRespinClick()
	{
		
		PickFromList();
	}
	
	private void PickFromList()
	{
		//disable arrow seq animation
		var randIndex = 0;
		randIndex = Random.RandomRange(0, _currentEntries.Count-1);
		_outEntry = _currentEntries[randIndex];
		_outEntry.DecreaseCurrentEntryCount();
		Debug.LogError("index : "+randIndex+ " cur :: "+_currentIndex);
		RewardSequence(randIndex);
	}

	public void RewardSequence(int randIndex)
	{
		_wheelController.StartWheelRotationAnimation(randIndex);
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
