using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //스폰 포인트를 랜덤으로 하기 위해 자식 오브젝트로 여러개 설정. 저장하기 위한 배열 변수
    public Transform[] spawnerPoint;
    //애너미의 스폰율을 조정하기 위한 타이머
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        //자식 오브젝트로 초기화
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
            //사용할 프리펩을 파라매터로 입력
            GameObject enemy = GameManager.instance.pool.Get(0);

            //스폰된 애너미의 위치는 여러개의 스폰 포인트중 랜덤하게 지정
            enemy.transform.position = spawnerPoint[Random.Range(1, spawnerPoint.Length)].position;
            timer = 0;
        }
    }
}
