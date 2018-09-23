using UnityEngine;
using UnityEngine.UI;

public class WheelEntry : MonoBehaviour
{

	public Image EntryImage;

	void Start()
	{
		
	}

	public void SetEntryImage(Sprite img)
	{
		var Image = GetComponent<Image>();
		Image.sprite = img;
	}
}
