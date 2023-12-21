using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public int hp;
    public int maxHp;
    public bool isLive;
    private Rigidbody2D playerRigid;
    private Rigidbody2D rigid;
    Vector2 nextVec;
    Animator animator;
    SpriteRenderer spriter;
    WaitForFixedUpdate wait;
    private void Start()
    {
        spriter = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        wait = new WaitForFixedUpdate();
    }

    private void FixedUpdate()
    {
        //�ֳʹ̰� ���� ���°ų� �ǰ� ���¸� �Լ� ����
        if (!isLive || animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            return;

        // Ÿ�ٰ� �ֳʹ� ������ ����
        Vector2 dirVec = playerRigid.position - rigid.position;

        //�ֳʹ��� ���� ���� * �ӵ��� ���ͷ� ����
        nextVec = dirVec.normalized * speed;

        // �������� �ֳʹ� �̵�
        rigid.MovePosition(rigid.position + nextVec * Time.deltaTime);
        // �ֳʹ̰� Ÿ�ٿ��� ��Ƶ� �з����� �ʰ� ���ν�Ƽ ����
        rigid.velocity = Vector3.zero;
    }

    //LateUpdate�� �ֳʹ� ��������Ʈ�� ������Ȳ�� ���õ� �ڵ� �ۼ�
    //�ֳʹ̰� false���¸� �Լ� ����
    //�ֳʹ��� ������ ���� ���� ����
    private void LateUpdate()
    {
        if (!isLive) 
            return;

        animator.SetFloat( "Speed",nextVec.magnitude);
        spriter.flipX = nextVec.x > 0 ? false : true;
   
    }
    //Ÿ�� ���� ����
    private void OnEnable()
    {
        playerRigid = GameManager.instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        hp = maxHp;
    }



    //�浹 �Լ� ����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�ҷ��� �浹���� �ʾҰų� �׾��ִ� ��Ȳ�̸� �Լ� ����
        if (!collision.CompareTag("Bullet") || !isLive || animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            return;

        //�ֳʹ��� ü���� �ҷ��� ��������ŭ ����
        hp -= collision.GetComponent<Bullet>().damage;

        //�ֳʹ̰� �ǰݴ��ҽ� �˹� �ڷ�ƾ ȣ��
        StartCoroutine(KnockBack());

        if (hp > 0)
        {
            animator.SetTrigger("Hit");
        }

        if (hp < 0)
        {
            Dead();
        }
        else
        {

        }
    }


    //�˹��� ���� �ڷ�ƾ �Լ�
    IEnumerator KnockBack()
    {
        //1������ ����
        yield return wait;

        //�÷��̾� ������ ����
        Vector3 playerPos = GameManager.instance.player.transform.position;

        //�ֳʹ��� �ݴ� ���� ���� ����
        Vector3 backVec = transform.position - playerPos;

        //�����忡 ���� ����(����ȭ ��) ForceMode2D.Impulse�� Ÿ��,����ó�� �������� ���� ��Ÿ���� ���
        rigid.AddForce(backVec.normalized * 5f, ForceMode2D.Impulse);
    }


    void Dead()
    {
        isLive = false;
        gameObject.SetActive(false);
    }
}
