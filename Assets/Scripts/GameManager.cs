using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player;
    public PoolManager pool;
    public static GameManager instance;

    //���� �ð�(��)
    public float curTime = 0;
    //���� �ð�(��)
    public float minTime = 0;
    //ų
    public int kill;
    //����ġ
    public float exp;
    //������ �ʿ��� ����ġ
    public float[] maxExp  = { 5,10,50,100,200,400,600,1000};
    //����
    public int level = 0;
    //ü��
    public float hp = 30;
    //�ִ� ü��
    public float maxHp = 100;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
        
    }


    private void Update()
    {
        curTime += Time.deltaTime;

        if(curTime > 59f)
        {
            minTime++;
            curTime = 0;
        }
    }
    //����ġ ȹ�� �Լ�
    public void plusExp()
    {
        exp++;
        kill++;
        if (maxExp[level] <= exp)
        {
            level++;
            exp = 0;
        }
    }
}
