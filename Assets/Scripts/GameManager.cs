using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Player player;
    public PoolManager pool;
    public static GameManager instance;
    public LevelUp levelUp;
    //게임 시간(초)
    public float curTime = 0;
    //게임 시간(분)
    public int minTime = 0;
    //킬
    public int kill;
    //경험치
    public float exp;
    //렙업에 필요한 경험치
    public float[] maxExp  = { 5,10,50,100,200,400,600,1000};
    //레벨
    public int level = 0;
    //체력
    public float hp;
    //최대 체력
    public float maxHp;

    public bool isLive = false;

    public Restart restartUi;
    public GameObject EnemyCleaner;
    public AssignBox boxManager;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
        
    }

    public void GameStart()
    {
        hp = maxHp;
        isLive = true;
        levelUp.Select(0);
        AudioManager.instance.PlayerBgm(true);
        ReStart();

    }
    public void GameVictory()
    {
        StartCoroutine(GameVictoryRoutine());

    }

    IEnumerator GameVictoryRoutine()
    {
        isLive = false;
        EnemyCleaner.SetActive(true);
        AudioManager.instance.PlayerBgm(false);
        AudioManager.instance.PlayerSfx(AudioManager.Sfx.Victory);
        yield return new WaitForSeconds(1.0f);
        restartUi.Win();
        Stop();
    }
    public void GameOver()
    {
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine()
    {
        isLive = false;
        AudioManager.instance.PlayerBgm(false);
        AudioManager.instance.PlayerSfx(AudioManager.Sfx.Lose);
        yield return new WaitForSeconds(0.2f);
        restartUi.gameObject.SetActive(true);
        restartUi.Lose();
        Stop();

    }
    public void GameRetry()
    {
        AudioManager.instance.PlayerSfx(AudioManager.Sfx.Select);
        SceneManager.LoadScene(0);
    }

    private void Update()
    {
        if (!isLive)
            return;

        curTime += Time.deltaTime;
        if(curTime > 58f)
        {
            //GameVictory();
        }

        if(curTime > 59f)
        {
            minTime++;
            curTime = 0;
        }

        if (exp >= maxExp[Mathf.Min(level, maxExp.Length - 1)])
        {
            level++;
            exp = 0;
            AudioManager.instance.PlayerSfx(AudioManager.Sfx.LevelUp);

            levelUp.Show();
        }
    }

    //경험치 획득 함수
    public void plusKill()
    {
        if (!isLive)
            return;

        kill++;

    }

    public void Stop()
    {
        isLive = false;
        Time.timeScale = 0;
    }

    public void ReStart()
    {
        isLive = true;
        Time.timeScale = 1;
    }

}
