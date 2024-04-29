using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    Rigidbody2D rigid;
    public enum EnemyType
    {
        Normal,
        Elete,
        Destroy,
        Invincibilit,
        Pull
    }
    bool trigger;
    public EnemyType enemyType;
    // Start is called before the first frame update
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        trigger = false;
    }
    private void FixedUpdate()
    {
        Vector3 playerPos = GameManager.instance.player.transform.position;
        if ((playerPos - transform.position).magnitude < 2f)
            trigger = true;


        Vector2 nextPos = (playerPos - transform.position).normalized;

        if (trigger)
            rigid.MovePosition(rigid.position + nextPos * 10f * Time.fixedDeltaTime);



        if ((playerPos - transform.position).magnitude < 0.1f)
        {
            switch (enemyType)
            {
                case EnemyType.Normal:
                    GameManager.instance.exp++;
                    gameObject.SetActive(false);
                    break;

                case EnemyType.Elete:
                    GameManager.instance.exp += 15;
                    gameObject.SetActive(false);
                    break;

                case EnemyType.Destroy:
                    GameManager.instance.pool.Get(11);
                    gameObject.SetActive(false);
                    break;
            }
        }
    }


    public void Create(int index, Vector3 pos)
    {
        switch (index)
        {
            case 3:
                GameObject eleteItem = GameManager.instance.pool.Get(1);
                eleteItem.transform.position = pos;
                break;
            case 8:
                GameObject destroyItem = GameManager.instance.pool.Get(8);
                destroyItem.transform.position = pos;
                break;
            default:
                GameObject normalItem = GameManager.instance.pool.Get(5);
                normalItem.transform.position = pos;
                break;
        }
    }
}
