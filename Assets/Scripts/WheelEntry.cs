using UnityEngine;
using UnityEngine.UI;

public class WheelEntry : MonoBehaviour
{

	public Image EntryImage;

	public void SetEntryImage(Sprite img)
	{
		EntryImage.sprite = img;
	}
}
