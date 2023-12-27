using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Box : MonoBehaviour
{
    int ranNumber;
    public DropItem dropItem;
    private void Awake()
    {
        ranNumber = Random.Range(0, 10);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet"))
            return;

        if(ranNumber == 8) //�ڼ� 
        {
            dropItem.Create(ranNumber, transform.position);
        }
        else if(ranNumber == 9) //�� ����
        {
            dropItem.Create(ranNumber, transform.position);
        }
        else if(ranNumber == 10) // ����
        {
            dropItem.Create(ranNumber, transform.position);
        }

        gameObject.SetActive(false);


    }
}
