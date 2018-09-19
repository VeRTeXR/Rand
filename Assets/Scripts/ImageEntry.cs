using UnityEngine;
using UnityEngine.UI;

public class ImageEntry : MonoBehaviour
{

	private int _entryIndex; 
	private int _maxCount;
	private int _currentCount;
	private string _description;
	public Button IncreaseButton;
	public Button DecreaseButton;
	public Button LoadButton;
	public Button DeleteButton;
	

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

	public void SetEntryIndex(int i)
	{
		_entryIndex = i;
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
		Manager.instance.FileLoader.LoadImage(_entryIndex);
	}

	public void DeleteEntry()
	{
		Manager.instance.FileLoader.DeleteEntry(gameObject);
	}
	
	public void IncreaseCurrentEntryCount()
	{
		_currentCount++;
	}

	public void DecreaseCurrentEntryCount()
	{
		_currentCount--;
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
