using System.Collections.Generic;
using System.Deployment.Internal;
using NUnit.Framework;
using UnityEngine;

public class Randomizer : MonoBehaviour
{
	private List<ImageEntry> _currentEntries;
	private List<GameObject> _spiningWheelEntry;
	private ImageEntry _outEntry;
	private int _currentIndex;
	private WheelController _wheelController;
	private GameObject _rewardEntry;
	private SpinButton _spinButton;
	private GameplayPanelController _gameplayPanel;
	private List<int> randNumbers1 = new List<int>();
	
	void Awake ()
	{
		_wheelController = GetComponent<WheelController>();
		PopulateEntryList();
	}

	private void Start()
	{
		_spinButton = _wheelController.SpinButton.GetComponent<SpinButton>();
		_gameplayPanel = _wheelController.GetComponentInParent<GameplayPanelController>();
		Debug.LogError(_spinButton);
		_spinButton.SetButtonState(true);
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Q))
		{	
				PickFromList();
		}
	}

	public void OnRespinClick()
	{
		Debug.LogError("nahhhh do it ");
		PickFromList();
		_spinButton.SetButtonState(false);
		_gameplayPanel.SetInputAvailability(false);
		
	}
	
	private void PickFromList()
	{
		var randIndex = GetNewNumber(randNumbers1, _currentEntries.Count);
		Debug.LogError("out : "+randIndex+"_cont : "+_currentEntries.Count);
		_outEntry = _currentEntries[randIndex];
		if (_outEntry != null)
		{
//			_outEntry.HasTriggered = true;
//			_outEntry.DecreaseCurrentEntryCount();
		}
		Debug.LogError("index : "+randIndex+ " cur :: "+_currentIndex);
		RewardSequence(_currentEntries[randIndex]);
	}

	private int GetNewNumber(List<int> randNumList, int availableCount)
	{
		int countMax = -1;
		int a = -1;
		if (availableCount > countMax)
		{
			countMax = availableCount;
			Debug.LogError("cMax : "+countMax);
		}
		while (a == -1)
		{
			a = Random.Range(0, availableCount);
			if (!randNumList.Contains(a))
				randNumList.Add(a);
			else
			{
				if (randNumList.Count == countMax)
				{
//					ClearTriggerFromCurrentEntries();
					randNumList.Clear();
				}
				a = -1;
			}
		}
		return a;
	}

	private void ClearTriggerFromCurrentEntries()
	{
		for (int i = 0; i < _currentEntries.Count; i++)
		{
			_currentEntries[i].HasTriggered = false;
		}
	}
	

	public void RewardSequence(ImageEntry rewardEntry)
	{
		_wheelController.StartWheelRotationAnimation(rewardEntry);
		PopulateEntryList();
	}

	public void RewardSequenceFinished()
	{
		_spinButton.SetButtonState(true);
		_gameplayPanel.SetInputAvailability(true);
	}
	
	
	public void PopulateEntryList()
	{
		_currentEntries = null; 
		_currentEntries = new List<ImageEntry>(10);
		var remainingAddingIndex = 10; 
		while (remainingAddingIndex > 0)
		{
			if(!Manager.instance.EntryManager.ImageEntry[_currentIndex].HasTriggered && Manager.instance.EntryManager.ImageEntry[_currentIndex].GetCurrentEntryCount() > 0)
				_currentEntries.Add(Manager.instance.EntryManager.ImageEntry[_currentIndex]);
//			else 
//				_currentEntries.Add(null);
			if (_currentIndex < Manager.instance.EntryManager.ImageEntry.Count - 1)
				_currentIndex++;
			else
				_currentIndex = 0;
			remainingAddingIndex--;
		}
		Debug.LogError("_current Index : "+_currentIndex);

	}

	public List<ImageEntry> GetCurrentEntries()
	{
		Debug.LogError("GetCurrentEntries");
		return _currentEntries;
	}
}
