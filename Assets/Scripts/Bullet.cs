using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    public int penetrate;
    Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public void Init(int damage, int penetrate, Vector3 dirVec)
    {
        this.damage = damage;
        this.penetrate = penetrate;

        if(penetrate > -1)
        {
            rigid.velocity = dirVec * 15f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //충돌 대상이 적이 아니거나 불렛이 발사형이 아닌 경우엔 함수 종료
        if (!collision.CompareTag("Enemy") || penetrate < 0)
            return;

        //불렛 관통력 1씩 감소
        penetrate--;
        //불렛 관통력이 0보다 낮아졌을때
        if(penetrate < 0)
        {
            //불렛 물리력 초기화
            rigid.velocity = Vector3.zero;
            //불렛 오브젝트 비활성화
            gameObject.SetActive(false);
        }

    }
}
