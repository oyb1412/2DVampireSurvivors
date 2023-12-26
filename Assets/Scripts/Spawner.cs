using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //스폰 포인트를 랜덤으로 하기 위해 자식 오브젝트로 여러개 설정. 저장하기 위한 배열 변수
    public Transform[] spawnerPoint;
    public SpawnDate[] spawnDate;
    public float eleteSpawn = 5;
    //애너미의 스폰율을 조정하기 위한 타이머
    float timer;
    float eleteTimer;
    public int gameLevel = 0;
    // Start is called before the first frame update
    void Start()
    {
        //자식 오브젝트로 초기화
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
    //게임레벨로 몹 소환
    //
    void Spawn()
    {
        timer += Time.deltaTime;
        eleteTimer += Time.deltaTime;
        if(eleteTimer > eleteSpawn)
        {
            //사용할 프리펩을 파라매터로 입력
            GameObject enemy = GameManager.instance.pool.Get(0);

            //스폰된 애너미의 위치는 여러개의 스폰 포인트중 랜덤하게 지정
            enemy.transform.position = spawnerPoint[UnityEngine.Random.Range(1, spawnerPoint.Length)].position;
            enemy.GetComponent<Enemy>().Init(spawnDate[2]);
            eleteTimer = 0;
        }
        if(timer > spawnDate[gameLevel].spawnTime)
        {
            //사용할 프리펩을 파라매터로 입력
            GameObject enemy = GameManager.instance.pool.Get(0);

            //스폰된 애너미의 위치는 여러개의 스폰 포인트중 랜덤하게 지정
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