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
        //애너미가 죽은 상태거나 피격 상태면 함수 종료
        if (!isLive || animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            return;

        // 타겟과 애너미 사이의 벡터
        Vector2 dirVec = playerRigid.position - rigid.position;

        //애너미의 진행 방향 * 속도를 벡터로 저장
        nextVec = dirVec.normalized * speed;

        // 직접적인 애너미 이동
        rigid.MovePosition(rigid.position + nextVec * Time.deltaTime);
        // 애너미가 타겟에게 닿아도 밀려나지 않게 벨로시티 조정
        rigid.velocity = Vector3.zero;
    }

    //LateUpdate에 애너미 스프라이트의 반전상황과 관련된 코드 작성
    //애너미가 false상태면 함수 종료
    //애너미의 방향을 토대로 반전 설정
    private void LateUpdate()
    {
        if (!isLive) 
            return;

        animator.SetFloat( "Speed",nextVec.magnitude);
        spriter.flipX = nextVec.x > 0 ? false : true;
   
    }
    //타겟 정보 저장
    private void OnEnable()
    {
        playerRigid = GameManager.instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        hp = maxHp;
    }



    //충돌 함수 생성
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //불렛과 충돌하지 않았거나 죽어있는 상황이면 함수 종료
        if (!collision.CompareTag("Bullet") || !isLive || animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            return;

        //애너미의 체력을 불렛의 데미지만큼 감소
        hp -= collision.GetComponent<Bullet>().damage;

        //애너미가 피격당할시 넉백 코루틴 호출
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


    //넉백을 위한 코루틴 함수
    IEnumerator KnockBack()
    {
        //1프레임 쉬기
        yield return wait;

        //플레이어 포지션 보관
        Vector3 playerPos = GameManager.instance.player.transform.position;

        //애너미의 반대 방향 벡터 보관
        Vector3 backVec = transform.position - playerPos;

        //리지드에 힘을 가함(정규화 후) ForceMode2D.Impulse는 타격,폭발처럼 순간적인 힘을 나타낼때 사용
        rigid.AddForce(backVec.normalized * 5f, ForceMode2D.Impulse);
    }


    void Dead()
    {
        isLive = false;
        gameObject.SetActive(false);
    }
}
