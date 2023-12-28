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
    float chargeTimer;
    bool chargeTrigger;
    public float range;
    public float baseRange;
    public float baseCoolTime;
    public float baseDamage;
    public ItemData data;
    
    private void Update()
    {
        if (!GameManager.instance.isLive)
            return;

        switch (id)
        {
            case 0:
                if (Input.GetMouseButton(0) && timer > coolTime)
                {
                    if (chargeTimer < 10)
                        chargeTimer += Time.deltaTime * 5;

                    if(chargeTimer > 0.5f)
                        GameManager.instance.player.ChargeaAnimation(true);


                    chargeTrigger = true;
                }
                if (Input.GetMouseButtonUp(0) && timer > coolTime)
                {
                    GameManager.instance.player.ChargeaAnimation(false);
                    chargeTrigger = false;

                    timer = 0;
                    FireMoonSlash();
                    AudioManager.instance.PlayerSfx(AudioManager.Sfx.Sword);

                }
                if (!chargeTrigger)
                    timer += Time.deltaTime;

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
        Vector2 playerPos = GameManager.instance.player.transform.position;
        Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 dir = mouse - playerPos;
        dir = dir.normalized;
        Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
        bullet.parent = GameObject.Find("Weapon0").transform;
        bullet.transform.position = GameManager.instance.player.transform.position;
        bullet.transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.transform.localScale = Vector3.one * (range + chargeTimer);
        bullet.GetComponent<Bullet>().Init(damage + chargeTimer * 15, id, dir, count);
        chargeTimer = 1;

    }

    void FireDagger()
    {
        //��ĳ�ʿ� �ɸ� �ֳʹ̰� ������ �Լ� ����
        if (!GameManager.instance.player.scanner.target)
            return;
        //�÷��̾� ������
        Vector3 playerPos = GameManager.instance.player.transform.position;
        //��ĳ�ʿ� �ɸ� �ֳʹ� ��ġ 
        Vector3 targetPos = GameManager.instance.player.scanner.target.position;
        //�÷��̾�->�ֳʹ� ���� ������ ����ȭ
        Vector3 vecDir = targetPos - playerPos;
        vecDir = vecDir.normalized;
        //Ǯ �Ŵ����� ���Ӱ� �ڽĿ�����Ʈ�� ����
        Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
        //�ҷ� ������Ʈ�� ����1 ������Ʈ�� ������ �̵�
        bullet.parent = GameObject.Find("Weapon1").transform;
        //�ҷ� ��ġ �ʱ�ȭ
        bullet.position = playerPos;
        //�ҷ��� ���ʹϿ� ȸ��
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, vecDir);

        bullet.localScale = Vector3.one * range;
        //�������� �����, �߻� ���� ����
        bullet.GetComponent<Bullet>().Init(damage, weaponType, vecDir, count);
    }

    void FireCross()
    {
        //��ĳ�ʿ� �ɸ� �ֳʹ̰� ������ �Լ� ����
        if (!GameManager.instance.player.scanner.target)
            return;
        //�÷��̾� ������
        Vector3 playerPos = GameManager.instance.player.transform.position;
        //��ĳ�ʿ� �ɸ� �ֳʹ� ��ġ 
        Vector3 targetPos = GameManager.instance.player.scanner.target.position;
        //�÷��̾�->�ֳʹ� ���� ������ ����ȭ
        Vector3 vecDir = targetPos - playerPos;
        vecDir = vecDir.normalized;
        //Ǯ �Ŵ����� ���Ӱ� �ڽĿ�����Ʈ�� ����
        Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
        //�ҷ� ������Ʈ�� ����1 ������Ʈ�� ������ �̵�
        bullet.parent = GameObject.Find("Weapon8").transform;
        //�ҷ� ��ġ �ʱ�ȭ
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
        damage = data.damage / 10;
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

    //���� ����, ��ġ
    void Assign()
    {
        //������ ������ŭ �ݺ� ����
        for(int i = 0; i< count; i++)
        {
            Transform bullet;
            if (i < transform.childCount)
            {
                //�� �ڽ� ������Ʈ�� �״�� ���
                bullet = transform.GetChild(i);
            }
            //���������� ���� ���Ӱ� ��ġ�� ������ ���
            else
            {
                bullet = GameManager.instance.pool.Get(prefabId).transform;
                bullet.parent = transform;
            }
            //�������� ��ġ 0,0,0���� �ʱ�ȭ
            bullet.transform.position = GameManager.instance.player.transform.position;

            //�������� ȸ���� �ʱ�ȭ
            bullet.transform.rotation = Quaternion.identity;

            //z���� �������� 360 * index * count�� ���� ����
            bullet.transform.Rotate(Vector3.forward * 360 * i / count);

            //���� ���� �� y������ ��ġ�̵�
            bullet.transform.Translate(0, 1.5f, 0f);

            bullet.localScale = Vector3.one * range;

            //�ҷ��� �ʱ�ȭ ����
            bullet.GetComponent<Bullet>().Init(damage, id, Vector3.zero, count);
        }


    }



    //���� �ڵ�ȸ�� �Լ�
    void AutoRotate()
    {
           //z���� �������� �ӵ���ŭ �ڵ�ȸ��
           transform.Rotate(Vector3.forward , (300f - (100 - (coolTime * 10))) * Time.deltaTime);
    }

}
