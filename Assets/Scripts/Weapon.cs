using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public int damage;
    public float speed;
    public int count;
    public int penetrate;



    private void Update()
    {
        switch (id)
        {
            case 0:
                AutoRotate();
                break;
        }
        if(Input.GetButtonDown("Jump"))
        {
            LevelUp();
        }
    }

    //무기 생성, 배치
    void Assign()
    {
        //무기의 갯수만큼 반복 실행
        for(int i = 0; i< count; i++)
        {
            Transform bullet;
            //인덱스가 현재 생성된 웨폰의 자식오브젝트보다 적을경우(이미 생성된 무기가 있는 경우)
            if(i < transform.childCount)
            {
                //그 자식 오브젝트를 그대로 사용
                bullet = transform.GetChild(i);
            }
            else     //레벨업으로 인해 새롭게 배치를 진행할 경우
            {
                //풀 매니저에 새롭게 자식오브젝트로 생성
                bullet = GameManager.instance.pool.Get(prefabId).transform;
                //풀 매니저에 있는 자식 오브젝트를 weapon으로 이동
                bullet.parent = transform;
            }

            //생성직후 위치 0,0,0으로 초기화
            bullet.transform.position = GameManager.instance.player.transform.position;

            //생성직후 회전량 초기화
            bullet.transform.rotation = Quaternion.identity;

            //z축을 기준으로 360 * index * count로 각도 조정
            bullet.transform.Rotate(Vector3.forward * 360 * i / count);

            //각도 조정 후 y축으로 위치이동
            bullet.transform.Translate(0, 1.0f, 0f);

            //불렛의 초기화 진행
            bullet.GetComponent<Bullet>().Init(damage, penetrate);
        }


    }

    public void Init()
    {
        switch (id)
        {
            case 0:
                speed = 250f;
                Assign();
                break;
        }
    }

    //무기 자동회전 함수
    void AutoRotate()
    {
        switch (id)
        {
            case 0:
                //z축을 기준으로 속도만큼 자동회전
                transform.Rotate(Vector3.forward , speed * Time.deltaTime);
                break;
        }
    }

    void LevelUp()
    {
        damage =1;
        count++;

        if(id == 0)
        {
            Assign();
        }
    }


}
