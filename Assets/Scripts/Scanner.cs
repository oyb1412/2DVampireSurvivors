using UnityEngine;

public class Scanner : MonoBehaviour
{
    //��ĵ ����
    [SerializeField]private float scanRange;
    //��ĵ�� ����� ���̾��ũ
    [SerializeField] private LayerMask layer;
    //����ĳ��Ʈ�� ��Ʈ�� ���(��ĵ�� ���)�� ������ �迭
    private RaycastHit2D[] targets;
    //���� ����� ���
    public Transform Target { get; private set; }

    private void FixedUpdate()
    {
        if (!GameManager.instance.IsLive)
            return;

        //������ ĳ��Ʈ�� ��� ��� ����� ��ȯ�ϴ� �Լ� (1.ĳ���� ���� ��ġ 2.���� ������ 3.ĳ���� ���� 4.��� ������ ���� 5.��� ���̾� )
        targets = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0f, layer);
        Target = GetTarget();
    }

    /// <summary>
    /// ���� ����� ���� ��ȯ
    /// </summary>
    /// <returns>���� ����� ��</returns>
    Transform GetTarget()
    {
        Transform result = null;
        //��ĵ ���� ����
        float diff = 100f;
        //��ĵ�� ��� ���� ����ŭ �ݺ��� ����
        for(int i = 0; i<targets.Length; i++)
        {
            //�÷��̾� ������
            Vector2 playerPos = transform.position;
            //�ֳʹ��� ������
            Vector2 enemyPos = targets[i].transform.position;
            //���� a,b�� �Ÿ��� ��ȯ�ϴ� �Լ�
            float curDiff = Vector2.Distance(playerPos, enemyPos);
            //������ �����ȿ� �ֳʹ̰� �����ҽ�
            if (curDiff < diff)
            {
                //�� �ֳʹ̿��� �Ÿ��� ���Ӱ� ����
                diff = curDiff;
                //�� �ֳʹ̸� ���� ����� Ÿ������ ����
                result = targets[i].transform;
            }

        }

        return result;
    }    
}
