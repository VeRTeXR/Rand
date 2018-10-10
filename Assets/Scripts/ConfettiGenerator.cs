using EZObjectPools;
using UnityEngine;

public class ConfettiGenerator : MonoBehaviour
{
    private Vector3 _randPos;
    public EZObjectPool ConfettiPiecePool;

    public void ThrowConf()
    {
        Debug.LogError("throwCCofff");
        for (var i = 0; i < 30; i++)
        {
            var randPos = new Vector2(transform.position.x,transform.position.y) + (Random.insideUnitCircle * 400f);
            GameObject c;
            if(ConfettiPiecePool.TryGetNextObject(randPos, Quaternion.identity, out c))
            {
                c.gameObject.SetActive(true);
                c.transform.parent = transform;
                c.transform.eulerAngles = new Vector3(0,0,Random.Range(-10,10));
                c.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(Random.Range(-0.25f,0.25f),Random.Range(0,2))*1000, ForceMode2D.Impulse);
            }
        }
    }
}
