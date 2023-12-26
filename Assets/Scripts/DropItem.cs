using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    Rigidbody2D rigid;
    bool trigger;
    // Start is called before the first frame update
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = GameManager.instance.player.transform.position;
        if ((playerPos - transform.position).magnitude < 6f)
            trigger = true;

        Vector2 nextPos = (playerPos - transform.position).normalized * 30f;

        if(trigger)
            rigid.MovePosition(rigid.position + nextPos * Time.deltaTime);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        GameManager.instance.exp++;
        gameObject.SetActive(false);
    }
}
