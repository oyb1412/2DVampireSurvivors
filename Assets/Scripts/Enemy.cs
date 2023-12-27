using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float hp;
    public int maxHp;
    public bool isLive;
    public int enemyType;
    private Rigidbody2D playerRigid;
    private Rigidbody2D rigid;
    Vector2 nextVec;
    public RuntimeAnimatorController[] controllers;
    Animator animator;
    SpriteRenderer spriter;
    Collider2D col;
    public DropItem dropItem;
    private void Awake()
    {
        col = GetComponent<Collider2D>();
        spriter = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (!GameManager.instance.isLive)
            return;

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
    public void Init(SpawnDate data)
    {
        enemyType = data.SpriteType;
        animator.runtimeAnimatorController = controllers[data.SpriteType];
        speed = data.speed;
        maxHp = data.health;
        hp = data.health;
    }
    //LateUpdate�� �ֳʹ� ��������Ʈ�� ������Ȳ�� ���õ� �ڵ� �ۼ�
    //�ֳʹ̰� false���¸� �Լ� ����
    //�ֳʹ��� ������ ���� ���� ����
    private void LateUpdate()
    {
        if (!GameManager.instance.isLive)
            return;

        if (!isLive) 
            return;

        spriter.flipX = nextVec.x > 0 ? false : true;
   
    }
    //Ÿ�� ���� ����
    private void OnEnable()
    {
        playerRigid = GameManager.instance.player.GetComponent<Rigidbody2D>();
        hp = maxHp;

        isLive = true;
        col.enabled = true;
        rigid.simulated = true;
        spriter.sortingOrder = 2;
        animator.SetBool("Dead", false);
    }


    //�浹 �Լ� ����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�ҷ��� �浹���� �ʾҰų� �׾��ִ� ��Ȳ�̸� �Լ� ����
        if (!collision.CompareTag("Bullet") || !isLive)
            return;

        //�ֳʹ��� ü���� �ҷ��� ��������ŭ ����
        hp -= collision.GetComponent<Bullet>().damage;
        AudioManager.instance.PlayerSfx(AudioManager.Sfx.Hit);

  
        if (hp > 0)
        {
            animator.SetTrigger("Hit");
            StartCoroutine(KnockBack());
        }
        else
        {
           dropItem.Create(enemyType,transform.position);
           StartCoroutine(EnemyDead());
        }

    }



 
    
    IEnumerator EnemyDead()
    {
        isLive = false;
        GameManager.instance.plusKill();
        spriter.sortingOrder = 1;
        animator.SetBool("Dead", true);
        yield return new WaitForSeconds(0.5f);
        transform.gameObject.SetActive(false);
        col.enabled = false;
        rigid.simulated = false;
    }

    //�˹��� ���� �ڷ�ƾ �Լ�
    IEnumerator KnockBack()
    {
        //�÷��̾� ������ ����
        Vector3 playerPos = GameManager.instance.player.transform.position;

        //�ֳʹ��� �ݴ� ���� ���� ����
        Vector3 backVec = transform.position - playerPos;

        //�����忡 ���� ����(����ȭ ��) ForceMode2D.Impulse�� Ÿ��,����ó�� �������� ���� ��Ÿ���� ���
        rigid.AddForce(backVec.normalized * 0.2f, ForceMode2D.Impulse);

        //1������ ����
        yield return new WaitForSeconds(0.3f);
    }


    void Dead()
    {
        gameObject.SetActive(false);
    }
}
