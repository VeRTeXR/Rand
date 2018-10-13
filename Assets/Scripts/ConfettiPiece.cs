using UnityEngine;

public class ConfettiPiece : MonoBehaviour
{
    private void Awake()
    {       
        var seq = LeanTween.sequence();
        seq.append(4f);
        seq.append(() => gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero);
        seq.append(() => gameObject.SetActive(false));
        
    }
}
