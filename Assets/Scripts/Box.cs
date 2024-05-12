using UnityEngine;

/// <summary>
/// �ı� ������ Box ������Ʈ
/// </summary>
public class Box : MonoBehaviour
{
    // ��� ���� ���� ����
    private int ranNumber;

    // ����� ������
    public DropItem dropItem;

    private void Awake()
    {
        // 0~7������ ���� ���� ��ȯ
        ranNumber = Random.Range(0, 7);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �߻�ü �̿��� �浹 ����
        if (!collision.CompareTag("Bullet"))
            return;

        // ����� ������ Ư�� �����Ͻ�
        if(ranNumber  == 2)  
        {
            // ������ ���
            dropItem.Create(8, transform.position);
        }
        // ü�� ȸ��
        GameManager.instance.hp += 3;

        // ��Ȱ��ȭ
        gameObject.SetActive(false);
    }
}
