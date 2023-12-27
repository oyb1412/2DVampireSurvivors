using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignBox : MonoBehaviour
{
    public void BoxInit(int index)
    {
        for (int i = 0; i < index; i++)
        {
            Vector3 ranPos = new Vector3(Random.Range(-50f, 50), Random.Range(-50f, 50), 0f);
            Transform box = GameManager.instance.pool.Get(7).transform;
            box.position = ranPos;
            box.parent = GameObject.Find("BoxManager").transform;

        }
    }


}
