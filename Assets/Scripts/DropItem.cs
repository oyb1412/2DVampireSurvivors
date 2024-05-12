using UnityEngine;

/// <summary>
/// 아이템 드롭 데이터 관리
/// </summary>
public class DropItem : MonoBehaviour
{
    public enum EnemyType {
        Normal,
        Elete,
        Destroy,
        Invincibilit,
        Pull
    }

    [SerializeField]private EnemyType enemyType;

    private Rigidbody2D rigid;

    //플레이어 자석효과 발동가능 여부
    private bool trigger;

    #region InitMethod
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        trigger = false;
    }
    #endregion


    private void FixedUpdate()
    {
        Magnetic();
    }

    /// <summary>
    /// 플레이어와 가까워질시 플레이어에 빨려들어가는 기능
    /// </summary>
    private void Magnetic() {
        Vector3 playerPos = GameManager.instance.player.transform.position;
        if ((playerPos - transform.position).magnitude < 2f)
            trigger = true;

        Vector2 nextPos = (playerPos - transform.position).normalized;

        if (trigger)
            rigid.MovePosition(rigid.position + nextPos * 10f * Time.fixedDeltaTime);

        if ((playerPos - transform.position).magnitude < 0.1f) {
            switch (enemyType) {
                case EnemyType.Normal:
                    GameManager.instance.exp++;
                    gameObject.SetActive(false);
                    break;

                case EnemyType.Elete:
                    GameManager.instance.exp += 15;
                    gameObject.SetActive(false);
                    break;

                case EnemyType.Destroy:
                    GameManager.instance.pool.Get(11);
                    gameObject.SetActive(false);
                    break;
            }
        }
    }

    /// <summary>
    /// 아이템 드랍 메소드
    /// </summary>
    /// <param name="index"></param>
    /// <param name="pos"></param>
    public void Create(int index, Vector3 pos)
    {
        switch (index)
        {
            case 3:
                GameObject eleteItem = GameManager.instance.pool.Get(1);
                eleteItem.transform.position = pos;
                break;
            case 8:
                GameObject destroyItem = GameManager.instance.pool.Get(8);
                destroyItem.transform.position = pos;
                break;
            default:
                GameObject normalItem = GameManager.instance.pool.Get(5);
                normalItem.transform.position = pos;
                break;
        }
    }
}
