using UnityEngine;
using UnityEngine.UI;

public class WheelEntry : MonoBehaviour
{

	public Image EntryImage;
	public ImageEntry LinkedEntry;

	public void SetEntryImage(Sprite img= null)
	{
		if (img == null) return;
		var Image = GetComponent<Image>();
		Image.sprite = img;
	}

	public void SetLinkEntry(ImageEntry linkEntry)
	{
		if (linkEntry != null)
			LinkedEntry = linkEntry;
	}

	public ImageEntry GetLinkEntry()
	{
		return LinkedEntry;
	}
}
