using UnityEngine;
using UnityEngine.UI;

public class WheelEntry : MonoBehaviour
{

	public Image EntryImage;

	public void SetEntryImage(Sprite img= null)
	{
		if (img == null) return;
		var Image = GetComponent<Image>();
		Image.sprite = img;
	}
}
