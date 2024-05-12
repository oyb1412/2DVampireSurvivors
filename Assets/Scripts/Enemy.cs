using System.Collections;
using UnityEngine;

/// <summary>
/// 적 데이터 관리
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
    /// 생성 시 초기화
    /// </summary>
    /// <param name="data">생성 데이터</param>
    public void Init(SpawnDate data) {
        enemyType = data.SpriteType;
        animator.runtimeAnimatorController = controllers[data.SpriteType];
        speed = data.speed;
        maxHp = data.health;
        hp = data.health + GameManager.instance.Level;
    }

    /// <summary>
    /// 풀링으로 인해 재활성시 초기화
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

 
        //애너미가 죽은 상태거나 피격 상태면 함수 종료
        if (!isLive || animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            return;

        // 타겟과 애너미 사이의 벡터
        Vector2 dirVec = playerRigid.position - rigid.position;

        //애너미의 진행 방향 * 속도를 벡터로 저장
        nextVec = dirVec.normalized * speed;

        // 직접적인 애너미 이동
        rigid.MovePosition(rigid.position + nextVec * Time.fixedDeltaTime);
        // 애너미가 타겟에게 닿아도 밀려나지 않게 벨로시티 조정
        rigid.velocity = Vector3.zero;

    }

    //LateUpdate에 애너미 스프라이트의 반전상황과 관련된 코드 작성
    //애너미가 false상태면 함수 종료
    //애너미의 방향을 토대로 반전 설정
    private void LateUpdate()
    {
        if (!GameManager.instance.IsLive)
            return;

        if (!isLive) 
            return;

        
        spriter.flipX = nextVec.x > 0 ? false : true;
   
    }

    //충돌 함수 생성
    private void OnTriggerEnter2D(Collider2D collision) {
        //불렛과 충돌하지 않았거나 죽어있는 상황이면 함수 종료
        if (!collision.CompareTag("Bullet") || !isLive)
            return;

        //애너미의 체력을 불렛의 데미지만큼 감소
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
    /// 애너미 사망 시 발동
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
    /// 애너미 피격 시 발동(넉백)
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    IEnumerator Co_HitRoutine(Collider2D index)
    {
        //플레이어 포지션 보관
        Vector3 playerPos = GameManager.instance.player.transform.position;

        //애너미의 반대 방향 벡터 보관
        Vector3 backVec = transform.position - playerPos;

        hp -= index.GetComponent<Bullet>().Damage;
        animator.SetTrigger("Hit");
        //리지드에 힘을 가함(정규화 후) ForceMode2D.Impulse는 타격,폭발처럼 순간적인 힘을 나타낼때 사용
        rigid.AddForce(backVec.normalized * 0.2f, ForceMode2D.Impulse);

        //1프레임 쉬기
        yield return new WaitForSeconds(0.15f);
    }
    #endregion
}
