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
        //�浹 ����� ���� �ƴϰų� �ҷ��� �߻����� �ƴ� ��쿣 �Լ� ����
        if (!collision.CompareTag("Enemy") || penetrate < 0)
            return;

        //�ҷ� ����� 1�� ����
        penetrate--;
        //�ҷ� ������� 0���� ����������
        if(penetrate < 0)
        {
            //�ҷ� ������ �ʱ�ȭ
            rigid.velocity = Vector3.zero;
            //�ҷ� ������Ʈ ��Ȱ��ȭ
            gameObject.SetActive(false);
        }

    }
}
