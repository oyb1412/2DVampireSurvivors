using System.Collections;
using UnityEngine;

/// <summary>
/// �� ������ ����
/// </summary>
public class Enemy : MonoBehaviour
{
    #region Variable
    private float speed;
    private float hp =1;
    private int maxHp;
    private bool isLive;
    private int enemyType;
    private Rigidbody2D playerRigid;
    private Rigidbody2D rigid;
    private Vector2 nextVec;
    private Animator animator;
    private SpriteRenderer spriter;
    private Collider2D col;

    [SerializeField] private RuntimeAnimatorController[] controllers;
    [SerializeField] private DropItem dropItem;
    #endregion

    #region InitMethod
    private void Awake()
    {
        col = GetComponent<Collider2D>();
        spriter = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// ���� �� �ʱ�ȭ
    /// </summary>
    /// <param name="data">���� ������</param>
    public void Init(SpawnDate data) {
        enemyType = data.SpriteType;
        animator.runtimeAnimatorController = controllers[data.SpriteType];
        speed = data.speed;
        maxHp = data.health;
        hp = data.health + GameManager.instance.Level;
    }

    /// <summary>
    /// Ǯ������ ���� ��Ȱ���� �ʱ�ȭ
    /// </summary>
    private void OnEnable() {
        playerRigid = GameManager.instance.player.GetComponent<Rigidbody2D>();
        hp = maxHp;
        isLive = true;
        col.enabled = true;
        rigid.simulated = true;
        spriter.sortingOrder = 2;
        animator.SetBool("Dead", false);
    }
    #endregion

    #region UpdateMethod
    private void FixedUpdate()
    {
        if (!GameManager.instance.IsLive)
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

    //LateUpdate�� �ֳʹ� ��������Ʈ�� ������Ȳ�� ���õ� �ڵ� �ۼ�
    //�ֳʹ̰� false���¸� �Լ� ����
    //�ֳʹ��� ������ ���� ���� ����
    private void LateUpdate()
    {
        if (!GameManager.instance.IsLive)
            return;

        if (!isLive) 
            return;

        
        spriter.flipX = nextVec.x > 0 ? false : true;
   
    }

    //�浹 �Լ� ����
    private void OnTriggerEnter2D(Collider2D collision) {
        //�ҷ��� �浹���� �ʾҰų� �׾��ִ� ��Ȳ�̸� �Լ� ����
        if (!collision.CompareTag("Bullet") || !isLive)
            return;

        //�ֳʹ��� ü���� �ҷ��� ��������ŭ ����
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Hit);
        if (hp > 0) {
            StartCoroutine(Co_HitRoutine(collision));
        }
        if (hp <= 0) {
            dropItem.Create(enemyType, transform.position);
            StartCoroutine(Co_EnemyDead());
        }
    }
    #endregion

    #region HitMethod
    /// <summary>
    /// �ֳʹ� ��� �� �ߵ�
    /// </summary>
    /// <returns></returns>
    IEnumerator Co_EnemyDead()
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

    /// <summary>
    /// �ֳʹ� �ǰ� �� �ߵ�(�˹�)
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    IEnumerator Co_HitRoutine(Collider2D index)
    {
        //�÷��̾� ������ ����
        Vector3 playerPos = GameManager.instance.player.transform.position;

        //�ֳʹ��� �ݴ� ���� ���� ����
        Vector3 backVec = transform.position - playerPos;

        hp -= index.GetComponent<Bullet>().Damage;
        animator.SetTrigger("Hit");
        //�����忡 ���� ����(����ȭ ��) ForceMode2D.Impulse�� Ÿ��,����ó�� �������� ���� ��Ÿ���� ���
        rigid.AddForce(backVec.normalized * 0.2f, ForceMode2D.Impulse);

        //1������ ����
        yield return new WaitForSeconds(0.15f);
    }
    #endregion
}
