using System;
using UnityEngine;
using UnityEngine.UI;

public class ImageEntry : MonoBehaviour
{
	private int _maxCount;
	private int _currentCount;
	private string _description;
	public Button IncreaseButton;
	public Button DecreaseButton;
	public Button LoadButton;
	public Button DeleteButton;
	public bool HasTriggered;

	void Start()
	{
		if (IncreaseButton != null)
			IncreaseButton.onClick.AddListener(IncreaseCurrentEntryCount);
		if(DecreaseButton != null) 
			DecreaseButton.onClick.AddListener(DecreaseCurrentEntryCount);
		if(LoadButton != null) 
			LoadButton.onClick.AddListener(LoadImage);
		if(DeleteButton != null) 
			DeleteButton.onClick.AddListener(DeleteEntry);
	}
	
	void Update()
	{
		if (_maxCount < _currentCount)
			_maxCount = _currentCount;
	}
	
	public void SetDescription(string text)
	{
		_description = text;
	}

	public string GetDescription()
	{
		return _description;
	}

	public void LoadImage()
	{
		var entryIndex = 0;
		for (var i = 0; i < Manager.instance.EntryManager.ImageEntry.Count; i++)
			if (Manager.instance.EntryManager.ImageEntry[i] == this)
				entryIndex = i;
		
		Manager.instance.EntryManager.LoadImage(entryIndex);
	}

	public void DeleteEntry()
	{
		Manager.instance.EntryManager.DeleteEntry(gameObject);
	}
	
	public void IncreaseCurrentEntryCount()
	{
		_currentCount++;
		Mathf.Clamp(_currentCount, 0, Int32.MaxValue);
	}

	public void DecreaseCurrentEntryCount()
	{
		_currentCount--;
		Mathf.Clamp(_currentCount, 0, Int32.MaxValue);
		if (_currentCount < 0)
		{
			
		}
	}

	public int GetCurrentEntryCount()
	{
		return _currentCount;
	}

	public int GetMaxEntryCount()
	{
		return _maxCount;
	}

}
