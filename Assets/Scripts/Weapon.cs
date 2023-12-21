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
            bullet.transform.position = GameManager.instance.player.transform.position;

            //�������� ȸ���� �ʱ�ȭ
            bullet.transform.rotation = Quaternion.identity;

            //z���� �������� 360 * index * count�� ���� ����
            bullet.transform.Rotate(Vector3.forward * 360 * i / count);

            //���� ���� �� y������ ��ġ�̵�
            bullet.transform.Translate(0, 1.0f, 0f);

            //�ҷ��� �ʱ�ȭ ����
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

    //���� �ڵ�ȸ�� �Լ�
    void AutoRotate()
    {
        switch (id)
        {
            case 0:
                //z���� �������� �ӵ���ŭ �ڵ�ȸ��
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
