using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //���� ����Ʈ�� �������� �ϱ� ���� �ڽ� ������Ʈ�� ������ ����. �����ϱ� ���� �迭 ����
    public Transform[] spawnerPoint;
    //�ֳʹ��� �������� �����ϱ� ���� Ÿ�̸�
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        //�ڽ� ������Ʈ�� �ʱ�ȭ
        spawnerPoint = GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Spawn();
    }

    void Spawn()
    {
        timer += Time.deltaTime;
        if(timer > 0.3f)
        {
            //����� �������� �Ķ���ͷ� �Է�
            GameObject enemy = GameManager.instance.pool.Get(0);

            //������ �ֳʹ��� ��ġ�� �������� ���� ����Ʈ�� �����ϰ� ����
            enemy.transform.position = spawnerPoint[Random.Range(1, spawnerPoint.Length)].position;
            timer = 0;
        }
    }
}
