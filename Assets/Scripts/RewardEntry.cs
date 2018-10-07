using UnityEngine;
using UnityEngine.UI;

public class RewardEntry : MonoBehaviour
{
    public Image AppliedRewardImage;

    public void StartAnimation()
    {
        gameObject.SetActive(true);
        LeanTween.alpha(gameObject.GetComponent<RectTransform>(), 0, 0f);
        transform.localPosition = new Vector3(0, transform.localPosition.y - 1000, 0);
        
        var seq = LeanTween.sequence();
        seq.append(LeanTween.alpha(gameObject.GetComponent<RectTransform>(), 1, 3f).setEaseInBack());
        seq.append(LeanTween.moveLocalY(gameObject, 0, 1f).setEaseInCubic());
        seq.append(2f);
        seq.append(LeanTween.alpha(gameObject.GetComponent<RectTransform>(), 0, 3f).setEaseInBack());
        seq.append(() =>
            {
                gameObject.SetActive(false);
                WheelController.ResetWheelState();
            }
        );
    }

    public void StartConfettiAnimation()
    {
        
    }
    public WheelController WheelController;

    public void SetRewardSprite(Sprite img)
    {
        AppliedRewardImage.sprite = img;
    } 
}
