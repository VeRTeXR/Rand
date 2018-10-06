using UnityEngine;
using UnityEngine.UI;

public class RewardEntry : MonoBehaviour
{
    public Image AppliedRewardImage;
    private RectTransform _rectTransform;

    void Awake()
    {
        _rectTransform = gameObject.GetComponent<RectTransform>();
        LeanTween.alpha(gameObject.GetComponent<RectTransform>(), 0, 0f);
    }
    
    
    public void StartAnimation()
    {
        gameObject.SetActive(true);
//        ResetRewardEntry();
        transform.localPosition = new Vector3(0, transform.localPosition.y - 300f, 0);
        
        var seq = LeanTween.sequence();
        seq.append(LeanTween.alpha(gameObject.GetComponent<RectTransform>(), 1,2f).setEaseInBack());
        seq.append(LeanTween.moveLocalY(gameObject, 0, 3f).setEaseInCubic());
        seq.append(0.4f);
        seq.append(LeanTween.alpha(gameObject.GetComponent<RectTransform>(), 0, 0.5f).setEaseInBack());
        seq.append(()=>gameObject.SetActive(false) );

    }

    private void ResetRewardEntry()
    {
        LeanTween.alpha(gameObject.GetComponent<RectTransform>(), 1, 3f).setEaseInBack();
        
                
    }
    
    public void SetRewardSprite(Sprite img)
    {
        AppliedRewardImage.sprite = img;
    } 
}
