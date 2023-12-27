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

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = GameManager.instance.player.transform.position;
        if ((playerPos - transform.position).magnitude < 1f)
            trigger = true;

        Vector2 nextPos = (playerPos - transform.position).normalized * 20f;

        if(trigger)
            rigid.MovePosition(rigid.position + nextPos * Time.deltaTime);

        if ((playerPos - transform.position).magnitude < 0.05f)
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
                    gameObject.SetActive(false);
                    break;

                case EnemyType.Pull:
                    gameObject.SetActive(false);
                    break;

                case EnemyType.Invincibilit:
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
          
            default:
                GameObject normalItem = GameManager.instance.pool.Get(5);
                normalItem.transform.position = pos;
                break;
        }
    }
}
