using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    //��ĵ ����
    public float scanRange;
    //��ĵ�� ����� ���̾��ũ
    public LayerMask layer;
    //����ĳ��Ʈ�� ��Ʈ�� ���(��ĵ�� ���)�� ������ �迭
    public RaycastHit2D[] targets;
    //���� ����� ���
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void FixedUpdate()
    {
        //������ ĳ��Ʈ�� ��� ��� ����� ��ȯ�ϴ� �Լ� (1.ĳ���� ���� ��ġ 2.���� ������ 3.ĳ���� ���� 4.��� ������ ���� 5.��� ���̾� )
        targets = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0f, layer);
        target = GetTarget();
    }

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
