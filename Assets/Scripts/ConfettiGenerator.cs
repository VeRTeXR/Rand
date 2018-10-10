using System.Collections.Generic;
using EZObjectPools;
using UnityEngine;
using UnityEngine.UI;

public class ConfettiGenerator : MonoBehaviour
{
    private Vector3 _randPos;
    public EZObjectPool ConfettiPiecePool;
    public List<Color> ColorList;

    public void ThrowConf()
    {
        for (var i = 0; i < 30; i++)
        {
            var randPos = new Vector2(transform.position.x,transform.position.y) + (Random.insideUnitCircle * 400f);
            GameObject c;
            if(ConfettiPiecePool.TryGetNextObject(randPos, Quaternion.identity, out c))
            {
                if(ColorList.Count > 0)
                    c.GetComponent<Image>().color = ColorList[Random.Range(0, ColorList.Count)];
                c.gameObject.SetActive(true);
                c.transform.SetParent(transform);
                c.transform.eulerAngles = new Vector3(0, 0, Random.Range(-10, 10));
                c.GetComponent<Rigidbody2D>()
                    .AddRelativeForce(new Vector2(Random.Range(-0.25f, 0.25f), Random.Range(1f, 2f)) * 1000,
                        ForceMode2D.Impulse);
            }
        }
    }
}
