using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject[] prefabs;
    List<GameObject>[] poolList;

    void Start()
    {
        //����Ʈ �迭 ũ��, ���� �ʱ�ȭ
        poolList = new List<GameObject>[prefabs.Length];
        
        for(int i = 0;i < poolList.Length; i++) 
        {
            poolList[i] = new List<GameObject>();
        }
    }

    //�������� Ǯ���� ����� �Լ�
    public GameObject Get(int index)
    {
        GameObject obj = null;
        //������Ʈ Ǯ������ ������ ��� ������Ʈ�� �˻�
        foreach (GameObject item in poolList[index])
        {
            // ��Ȱ��ȭ ��(��Ȱ�� ������)������Ʈ�� �ִٸ�
            if (!item.activeSelf)
            {
                obj = item;// ��Ȱ��
                obj.SetActive(true); // Ȱ��ȭ
                break;// �Լ� ����
            }
        }

        //��Ȱ�� ������ ������Ʈ�� ���ٸ�
        if (!obj)
        {
            //������Ʈ�� ���Ӱ� ���� ��
            obj = Instantiate(prefabs[index],transform);

            // Ǯ ����Ʈ�� �߰�
            poolList[index].Add(obj);
        }

        return obj;
    }
}

