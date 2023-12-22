using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player;
    public PoolManager pool;
    public static GameManager instance;

    //게임 시간(초)
    public float curTime = 0;
    //게임 시간(분)
    public float minTime = 0;
    //킬
    public int kill;
    //경험치
    public float exp;
    //렙업에 필요한 경험치
    public float[] maxExp  = { 5,10,50,100,200,400,600,1000};
    //레벨
    public int level = 0;
    //체력
    public float hp = 30;
    //최대 체력
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
    //경험치 획득 함수
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
