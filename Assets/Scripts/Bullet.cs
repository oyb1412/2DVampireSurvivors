using UnityEngine;

/// <summary>
/// 발사체 관리 클래스
/// </summary>
public class Bullet : MonoBehaviour
{
    public float Damage { get; private set; }
    private float weaponType;

    // 관통력
    private int count;
    private Rigidbody2D rigid;

    #region InitMethod
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// 발사체 생성 시 초기화
    /// </summary>
    /// <param name="damage">데미지</param>
    /// <param name="weaponType">발사체 타입</param>
    /// <param name="dirVec">날아갈 방향</param>
    /// <param name="count">관통력</param>
    public void Init(float damage, float weaponType, Vector3 dirVec, int count)
    {
        Damage = damage / 10;
        this.weaponType = weaponType;
        this.count = count;

        switch(weaponType)
        {
            case 0:
            case 1:
                rigid.velocity = dirVec * 15f;
                break;
            case 8:
                rigid.velocity = dirVec * 5f;
                break;
        }
     
    }

    /// <summary>
    /// 풀링 오브젝트로 인해 발사체 재활성화시 초기화
    /// </summary>
    private void OnEnable()
    {
        transform.position = GameManager.instance.player.transform.position;
    }
    #endregion

    private void Update()
    {
        //무기에 따른 다른 행동 반복
        switch(weaponType)
        {
            case 0:
            case 1:
                Vector3 pos = transform.position - GameManager.instance.player.transform.position;
                if (pos.magnitude > 15f)
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

    public void ExoplotionEnd() {
        gameObject.SetActive(false);
    }
}
