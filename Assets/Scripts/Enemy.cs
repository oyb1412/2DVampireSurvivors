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
    float knockTimer;
    private Rigidbody2D playerRigid;
    private Rigidbody2D rigid;
    Vector2 nextVec;
    Animator animator;
    SpriteRenderer spriter;
    WaitForFixedUpdate wait;
    Collider2D col;
    private void Awake()
    {
        col = GetComponent<Collider2D>();
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
        rigid.MovePosition(rigid.position + nextVec * Time.fixedDeltaTime);
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

        spriter.flipX = nextVec.x > 0 ? false : true;
   
    }
    //Ÿ�� ���� ����
    private void OnEnable()
    {
        playerRigid = GameManager.instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        hp = maxHp;

        isLive = true;
        col.enabled = true;
        rigid.simulated = true;
        spriter.sortingOrder = 2;
        animator.SetBool("Dead", false);
    }

    private void Update()
    {
        knockTimer += Time.deltaTime;
    }

    //�浹 �Լ� ����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�ҷ��� �浹���� �ʾҰų� �׾��ִ� ��Ȳ�̸� �Լ� ����
        if (!collision.CompareTag("Bullet") || !isLive)
            return;

        //�ֳʹ��� ü���� �ҷ��� ��������ŭ ����
        hp -= collision.GetComponent<Bullet>().damage;

        if(knockTimer > 0.7f)
        {
            //�ֳʹ̰� �ǰݴ��ҽ� �˹� �ڷ�ƾ ȣ��
            StartCoroutine(KnockBack());
            knockTimer = 0;
        }
 

        if (hp > 0)
        {
            animator.SetTrigger("Hit");
        }
        else
        {
            //col.enabled = false;
            //rigid.simulated = false;
            GameManager.instance.plusExp();
            spriter.sortingOrder = 1;
            isLive = false;
            animator.SetBool("Dead",true);
        }

    }


    //�˹��� ���� �ڷ�ƾ �Լ�
    IEnumerator KnockBack()
    {
        //�÷��̾� ������ ����
        Vector3 playerPos = GameManager.instance.player.transform.position;

        //�ֳʹ��� �ݴ� ���� ���� ����
        Vector3 backVec = transform.position - playerPos;

        //�����忡 ���� ����(����ȭ ��) ForceMode2D.Impulse�� Ÿ��,����ó�� �������� ���� ��Ÿ���� ���
        rigid.AddForce(backVec.normalized * 0.3f, ForceMode2D.Impulse);

        //1������ ����
        yield return wait;
    }


    void Dead()
    {
        gameObject.SetActive(false);
    }
}
