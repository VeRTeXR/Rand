using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VR.WSA.Input;

public class EntryTextSetter : MonoBehaviour
{

	public Text MaxText;
	public Text CurrentText;
	private ImageEntry _imageEntry;
	
	// Use this for initialization
	void Start ()
	{
		MaxText.text = "0";
		CurrentText.text = "0";
		_imageEntry = GetComponentInParent<ImageEntry>();
	}
	
	// Update is called once per frame
	void Update () {
		if (int.Parse(CurrentText.text) != _imageEntry.GetCurrentEntryCount())
		{
			CurrentText.text = _imageEntry.GetCurrentEntryCount().ToString();
		}
		if (int.Parse(MaxText.text) != _imageEntry.GetMaxEntryCount())
		{
			MaxText.text = _imageEntry.GetMaxEntryCount().ToString();
		}
	}
}
