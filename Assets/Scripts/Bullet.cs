using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public float weaponType;
    public int count;
    float deleteTimer;
    Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public void Init(float damage, float weaponType, Vector3 dirVec, int count)
    {
        this.damage = damage;
        this.weaponType = weaponType;
        this.count = count;
        if(this.weaponType == 1 )
        {
            rigid.velocity = dirVec * 15f;
        }

        if(this.weaponType == 8 ) 
        {
            rigid.velocity = dirVec * 7f;
        }
    }

    private void Update()
    {
        switch(weaponType)
        {
            case 0:

                deleteTimer += Time.deltaTime;
                if (deleteTimer > 0.25f)
                {
                    gameObject.SetActive(false);
                    deleteTimer = 0;
                }
                transform.position = GameManager.instance.player.transform.position;
                break;
            case 1:
                Vector3 pos = transform.position - GameManager.instance.player.transform.position;
                if (pos.magnitude > 25f)
                    gameObject.SetActive(false);
                break;
            case 8:
                transform.Rotate(Vector3.forward, 800f * Time.deltaTime);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        //충돌 대상이 적이 아니거나 불렛이 발사형이 아닌 경우엔 함수 종료
        if (!collision.CompareTag("Enemy") || weaponType != 1 && weaponType != 8)
            return;
        //불렛 관통력 1씩 감소
        count--;
        //불렛 관통력이 0보다 낮아졌을때
        if(count < 0)
        {
            //불렛 물리력 초기화
            rigid.velocity = Vector3.zero;
            //불렛 오브젝트 비활성화
            gameObject.SetActive(false);
        }

    }


}
