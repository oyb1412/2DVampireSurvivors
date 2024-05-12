using UnityEngine;

/// <summary>
/// 파괴 가능한 Box 오브젝트
/// </summary>
public class Box : MonoBehaviour
{
    // 드롭 여부 지정 변수
    private int ranNumber;

    // 드롭할 아이템
    public DropItem dropItem;

    private void Awake()
    {
        // 0~7까지의 랜덤 정수 반환
        ranNumber = Random.Range(0, 7);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 발사체 이외의 충돌 무시
        if (!collision.CompareTag("Bullet"))
            return;

        // 저장된 정수가 특정 정수일시
        if(ranNumber  == 2)  
        {
            // 아이템 드랍
            dropItem.Create(8, transform.position);
        }
        // 체력 회복
        GameManager.instance.hp += 3;

        // 비활성화
        gameObject.SetActive(false);
    }
}
