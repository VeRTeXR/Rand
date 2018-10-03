using UnityEngine;
using UnityEngine.UI;

public class RewardEntry : MonoBehaviour
{
    public Image AppliedRewardImage;

    public void StartAnimation()
    {
        var seq = LeanTween.sequence();
//        seq.append( () =>  LeanTween.move(gameObject, new Vector3( ), ));


    }
    
    
    public void SetRewardSprite(Sprite img)
    {
        AppliedRewardImage.sprite = img;
    } 
}
