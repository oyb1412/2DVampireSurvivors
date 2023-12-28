using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;
    public float coolTime;
    public int count =1;
    public int weaponType;
    float timer;
    float attackTimer;
    public float range;
    public float baseRange;
    public float baseCoolTime;
    public float baseDamage;
    public ItemData data;
    private void Start()
    {

    }
    private void Update()
    {
        if (!GameManager.instance.isLive)
            return;

        switch (id)
        {
            case 0:
                attackTimer += Time.deltaTime;
                if(attackTimer > coolTime)
                {
                    attackTimer = 0;
                    FireMoonSlash();
                }
                break;
            case 1:
                timer += Time.deltaTime;
                if(timer > coolTime)
                {
                    timer = 0;
                    FireDagger();
                }
                break;
            case 7:
                AutoRotate();
                break;
            case 8:
                timer += Time.deltaTime;
                if(timer > coolTime)
                {
                    timer = 0;
                    FireCross();
                }
                break;
        }
    }

    void FireMoonSlash()
    {
        Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 dir = new Vector2(mouse.x - transform.position.y, mouse.y - transform.position.y);
        Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
        bullet.parent = GameObject.Find("Weapon0").transform;
        bullet.transform.position = GameManager.instance.player.transform.position + (dir.normalized * 1.5f);
        bullet.transform.localScale = Vector3.one * range;
        //bullet.transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.GetComponent<Bullet>().Init(damage, id, dir.normalized, count);
    }

    void FireDagger()
    {
        //스캐너에 걸린 애너미가 없으면 함수 종료
        if (!GameManager.instance.player.scanner.target)
            return;
        //플레이어 포지션
        Vector3 playerPos = GameManager.instance.player.transform.position;
        //스캐너에 걸린 애너미 위치 
        Vector3 targetPos = GameManager.instance.player.scanner.target.position;
        //플레이어->애너미 벡터 저장후 정규화
        Vector3 vecDir = targetPos - playerPos;
        vecDir = vecDir.normalized;
        //풀 매니저에 새롭게 자식오브젝트로 생성
        Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
        //불렛 오브젝트를 웨폰1 오브젝트의 하위로 이동
        bullet.parent = GameObject.Find("Weapon1").transform;
        //불렛 위치 초기화
        bullet.position = playerPos;
        //불렛의 쿼터니온 회전
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, vecDir);

        bullet.localScale = Vector3.one * range;
        //데미지와 관통력, 발사 방향 지정
        bullet.GetComponent<Bullet>().Init(damage, weaponType, vecDir, count);
    }

    void FireCross()
    {
        //스캐너에 걸린 애너미가 없으면 함수 종료
        if (!GameManager.instance.player.scanner.target)
            return;
        //플레이어 포지션
        Vector3 playerPos = GameManager.instance.player.transform.position;
        //스캐너에 걸린 애너미 위치 
        Vector3 targetPos = GameManager.instance.player.scanner.target.position;
        //플레이어->애너미 벡터 저장후 정규화
        Vector3 vecDir = targetPos - playerPos;
        vecDir = vecDir.normalized;
        //풀 매니저에 새롭게 자식오브젝트로 생성
        Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
        //불렛 오브젝트를 웨폰1 오브젝트의 하위로 이동
        bullet.parent = GameObject.Find("Weapon8").transform;
        //불렛 위치 초기화
        bullet.position = playerPos;
        bullet.localScale = Vector3.one * range;
        bullet.GetComponent<Bullet>().Init(damage, id, vecDir, count);

    }
    public void LevelUp(float damage, float range)
    {
        this.damage = damage;
        this.range = range;
        count++;

        if (id == 7)
            Assign();

        GameManager.instance.player.BroadcastMessage("ApplyPassive", SendMessageOptions.DontRequireReceiver);
    }

    public void Init(ItemData data)
    {
        name = "Weapon" + data.itemId;
        transform.parent = GameManager.instance.player.transform;
        transform.localPosition = Vector3.zero;

        id = data.itemId;
        damage = data.damage;
        count = data.count;
        range = data.range / 100;
        coolTime = data.CT / 100;

        baseRange = range;
        baseCoolTime = coolTime;
        baseDamage = damage;
        for (int i = 0; i<GameManager.instance.pool.prefabs.Length; i++)
        {
            if(data.weaponObject == GameManager.instance.pool.prefabs[i])
            {
                prefabId = i;
                break;
            }
        }


        weaponType = (int)data.itemType;
        switch (id)
        {
            case 7:
                Assign();
                break;
        }

        GameManager.instance.player.BroadcastMessage("ApplyPassive", SendMessageOptions.DontRequireReceiver);
    }

    //무기 생성, 배치
    void Assign()
    {
        //무기의 갯수만큼 반복 실행
        for(int i = 0; i< count; i++)
        {
            Transform bullet;
            if (i < transform.childCount)
            {
                //그 자식 오브젝트를 그대로 사용
                bullet = transform.GetChild(i);
            }
            //레벨업으로 인해 새롭게 배치를 진행할 경우
            else
            {
                bullet = GameManager.instance.pool.Get(prefabId).transform;
                bullet.parent = transform;
            }
            //생성직후 위치 0,0,0으로 초기화
            bullet.transform.position = GameManager.instance.player.transform.position;

            //생성직후 회전량 초기화
            bullet.transform.rotation = Quaternion.identity;

            //z축을 기준으로 360 * index * count로 각도 조정
            bullet.transform.Rotate(Vector3.forward * 360 * i / count);

            //각도 조정 후 y축으로 위치이동
            bullet.transform.Translate(0, 1.5f, 0f);

            bullet.localScale = Vector3.one * range;

            //불렛의 초기화 진행
            bullet.GetComponent<Bullet>().Init(damage, id, Vector3.zero, count);
        }


    }



    //무기 자동회전 함수
    void AutoRotate()
    {
           //z축을 기준으로 속도만큼 자동회전
           transform.Rotate(Vector3.forward , (300f - (100 - (coolTime * 10))) * Time.deltaTime);
    }

}
