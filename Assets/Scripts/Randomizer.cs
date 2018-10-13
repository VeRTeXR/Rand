using System.Collections.Generic;
using UnityEngine;

public class Randomizer : MonoBehaviour
{
	private List<ImageEntry> _currentEntries;
	private ImageEntry _outEntry;
	private int _currentIndex;
	private WheelController _wheelController;
	private GameObject _rewardEntry;
	private SpinButton _spinButton;
	private GameplayPanelController _gameplayPanel;
	private int _randIndex;
	private List<ImageEntry> _alreadyRewardedList = new List<ImageEntry>();
	
	void Awake ()
	{
		_wheelController = GetComponent<WheelController>();
		PopulateEntryList();
	}

	private void Start()
	{
		_spinButton = _wheelController.SpinButton.GetComponent<SpinButton>();
		_gameplayPanel = _wheelController.GetComponentInParent<GameplayPanelController>();
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
		PickFromList();
		_spinButton.SetButtonState(false);
		_gameplayPanel.SetInputAvailability(false);	
	}
	
	private void PickFromList()
	{
		if (_currentEntries.Count < 0)
		{
//			_spinButton.SetButtonState(false);
			Debug.LogError("randCurrentEntryisOut");
			return;
		}
		_randIndex = Random.Range(0, _currentEntries.Count-1);//GetNewNumber(randNumbers1, _currentEntries.Count);
		_outEntry = _currentEntries[_randIndex];
		var i = 10;
		while (_alreadyRewardedList.Contains(_outEntry) && _outEntry!=null)
		{
			if (i == 0)
			{
				Debug.LogError("all has been rewarded");
				_alreadyRewardedList.Clear();
			}
			_randIndex = Random.Range(0, _currentEntries.Count);
			_outEntry = _currentEntries[_randIndex];
			i--;
		}
		_alreadyRewardedList.Add(_outEntry);
		_outEntry.DecreaseCurrentEntryCount();
		RewardSequence(_outEntry);
	}

	public void RewardSequence(ImageEntry rewardEntry)
	{
		_wheelController.StartWheelRotationAnimation(rewardEntry);
		PopulateEntryList();
	}

	public void RewardSequenceFinished()
	{
		if (_currentEntries.Count <= 0)
		{
			_spinButton.SetButtonState(false);
		}

		else
		{
			_spinButton.SetButtonState(true);
		}
		_gameplayPanel.SetInputAvailability(true);
	}

	public void PopulateEntryList()
	{
		_currentEntries = null; 
		_currentEntries = new List<ImageEntry>(10);
		var remainingAddingIndex = 10; 
		while (remainingAddingIndex > 0)
		{
			if(Manager.instance.EntryManager.ImageEntry[_currentIndex].GetCurrentEntryCount() > 0)
				_currentEntries.Add(Manager.instance.EntryManager.ImageEntry[_currentIndex]);
			if (_currentIndex < Manager.instance.EntryManager.ImageEntry.Count-1)
				_currentIndex++;
			else
				_currentIndex = 0;
			remainingAddingIndex--;
		}
		Debug.LogError("randomizer Populate");
	}

	public void Reinit()
	{
		_spinButton.SetButtonState(true);
		_gameplayPanel.SetInputAvailability(true);	
	}

	public List<ImageEntry> GetCurrentEntries()
	{
		return _currentEntries;
	}
}
