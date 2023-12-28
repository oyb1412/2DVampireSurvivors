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
        ranNumber = Random.Range(0, 7);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet"))
            return;

        if(ranNumber  == 2)  //Έχ Α¦°Ε
        {
            dropItem.Create(8, transform.position);
        }
        GameManager.instance.hp += 3;
        gameObject.SetActive(false);
    }
}
