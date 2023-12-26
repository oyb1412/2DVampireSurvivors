using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //���� ����Ʈ�� �������� �ϱ� ���� �ڽ� ������Ʈ�� ������ ����. �����ϱ� ���� �迭 ����
    public Transform[] spawnerPoint;
    public SpawnDate[] spawnDate;
    public float eleteSpawn = 5;
    //�ֳʹ��� �������� �����ϱ� ���� Ÿ�̸�
    float timer;
    float eleteTimer;
    public int gameLevel = 0;
    // Start is called before the first frame update
    void Start()
    {
        //�ڽ� ������Ʈ�� �ʱ�ȭ
        spawnerPoint = GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.isLive)
            return;

        gameLevel = Mathf.Min(GameManager.instance.minTime, spawnDate.Length - 1);
        Debug.Log(gameLevel);
        Spawn();
    }
    //���ӷ����� �� ��ȯ
    //
    void Spawn()
    {
        timer += Time.deltaTime;
        eleteTimer += Time.deltaTime;
        if(eleteTimer > eleteSpawn)
        {
            //����� �������� �Ķ���ͷ� �Է�
            GameObject enemy = GameManager.instance.pool.Get(0);

            //������ �ֳʹ��� ��ġ�� �������� ���� ����Ʈ�� �����ϰ� ����
            enemy.transform.position = spawnerPoint[UnityEngine.Random.Range(1, spawnerPoint.Length)].position;
            enemy.GetComponent<Enemy>().Init(spawnDate[2]);
            eleteTimer = 0;
        }
        if(timer > spawnDate[gameLevel].spawnTime)
        {
            //����� �������� �Ķ���ͷ� �Է�
            GameObject enemy = GameManager.instance.pool.Get(0);

            //������ �ֳʹ��� ��ġ�� �������� ���� ����Ʈ�� �����ϰ� ����
            enemy.transform.position = spawnerPoint[UnityEngine.Random.Range(1,spawnerPoint.Length)].position;
            enemy.GetComponent<Enemy>().Init(spawnDate[gameLevel]);
            timer = 0;
        }
    }
}

[System.Serializable]
public class SpawnDate
{
    public float spawnTime;
    public int SpriteType;
    public int health;
    public float speed;
}