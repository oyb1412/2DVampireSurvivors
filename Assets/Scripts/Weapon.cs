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
    float timer;
    private void Start()
    {
        Init();
    }

    private void Update()
    {
        switch (id)
        {
            case 0:
                AutoRotate();
                break;
            case 1:
                timer += Time.deltaTime;
                if(timer > speed)
                {
                    timer = 0;
                    Fire();
                }
                break;
        }
        if(Input.GetButtonDown("Jump"))
        {
            LevelUp(2,1);
        }
    }

    void Fire()
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
        bullet.parent = GameObject.Find("Weapon 1").transform;
        //�ҷ� ��ġ �ʱ�ȭ
        bullet.position = playerPos;
        //�ҷ��� ���ʹϿ� ȸ��
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, vecDir);
        //�������� �����, �߻� ���� ����
        bullet.GetComponent<Bullet>().Init(damage, count, vecDir);

    }
    void LevelUp(int damage, int count)
    {
        this.damage = damage;
        this.count += count;

        if (id == 0)
            Assign();
    }

    public void Init()
    {
        switch (id)
        {
            case 0:
                speed = -200f;
                Assign();
                break;
            case 1:
                break;
        }
    }

    //���� ����, ��ġ
    void Assign()
    {
        //������ ������ŭ �ݺ� ����
        for(int i = 0; i< count; i++)
        {
            Transform bullet;
            //�ε����� ���� ������ ������ �ڽĿ�����Ʈ���� �������(�̹� ������ ���Ⱑ �ִ� ���)
            if(i < transform.childCount)
            {
                //�� �ڽ� ������Ʈ�� �״�� ���
                bullet = transform.GetChild(i);
            }
            else     //���������� ���� ���Ӱ� ��ġ�� ������ ���
            {
                //Ǯ �Ŵ����� ���Ӱ� �ڽĿ�����Ʈ�� ����
                bullet = GameManager.instance.pool.Get(prefabId).transform;
                //Ǯ �Ŵ����� �ִ� �ڽ� ������Ʈ�� weapon���� �̵�
                bullet.parent = transform;
            }

            //�������� ��ġ 0,0,0���� �ʱ�ȭ
            bullet.transform.position = new Vector3(0f,0f,0f);

            //�������� ȸ���� �ʱ�ȭ
            bullet.transform.rotation = Quaternion.identity;

            //z���� �������� 360 * index * count�� ���� ����
            bullet.transform.Rotate(Vector3.forward * 360 * i / count);

            //���� ���� �� y������ ��ġ�̵�
            bullet.transform.Translate(0, 1.5f, 0f);

            //��ġ ���� �� ������ �ٽ� ������� ����
            bullet.transform.Rotate(-Vector3.forward * 360 * i / count);


            //�ҷ��� �ʱ�ȭ ����
            bullet.GetComponent<Bullet>().Init(damage, penetrate, Vector3.zero);
        }


    }



    //���� �ڵ�ȸ�� �Լ�
    void AutoRotate()
    {
           //z���� �������� �ӵ���ŭ �ڵ�ȸ��
           transform.Rotate(Vector3.forward , speed * Time.deltaTime);
    }

}
