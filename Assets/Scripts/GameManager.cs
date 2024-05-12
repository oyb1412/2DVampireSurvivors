using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 전체적인 게임 밸런스 관리
/// </summary>
public class GameManager : MonoBehaviour
{
    #region Variable
    public static GameManager instance;
    [Header("Object")]
    //플레이어
    public Player player;
    //풀링 오브젝트 매니저
    public PoolManager pool;
    //레벨업 시 호출UI
    public LevelUp levelUp;
    //승리 및 패배시 호출UI
    public Restart restartUi;
    //주변 애너미 처치 오브젝트 
    public GameObject EnemyCleaner;
    //파괴 가능한 상자 클래스
    public AssignBox assingBox;

    //게임 시간(초)
    public float CurrentTime { get; private set; }
    //게임 시간(분)
    public int MinTime { get; private set; }
    //킬
    public int KillNumber { get; private set; } 

    //렙업에 필요한 경험치
    public float[] MaxExp { get; private set; } = { 5, 10, 50, 100, 200, 400, 600, 1000 };
    //레벨
    public int Level { get; private set; }
    [Header("Status")]
    //체력
    public float hp;
    //경험치
    public float exp;
    //최대 체력
    [field:SerializeField]public float MaxHp { get; private set; }
    //생존 여부
    public bool IsLive { get; private set; }
    #endregion

    private void Awake()
    {
        //싱글턴
        if(instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }


    private void Update()
    {
        PlayGame();
    }

    /// <summary>
    /// 게임 시간 진행 및 플레이어 경험치 계산
    /// </summary>
    private void PlayGame() {
        if (!IsLive)
            return;

        CurrentTime += Time.deltaTime;

        if (CurrentTime > 59f) {
            MinTime++;
            CurrentTime = 0;
        }

        if (exp >= MaxExp[Mathf.Min(Level, MaxExp.Length - 1)]) {
            Level++;
            exp = 0;
            AudioManager.instance.PlaySfx(AudioManager.Sfx.LevelUp);

            levelUp.Show();
        }
    }

    /// <summary>
    /// 킬 수 증가
    /// </summary>
    public void plusKill()
    {
        if (!IsLive)
            return;

        KillNumber++;
    }

    #region GameState

    /// <summary>
    /// 게임 승리 시 호출
    /// </summary>
    IEnumerator Co_GameVictory() {
        IsLive = false;
        EnemyCleaner.SetActive(true);
        AudioManager.instance.PlayBgm(false);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Victory);
        yield return new WaitForSeconds(1.0f);
        restartUi.Win();
        Stop();
    }

    /// <summary>
    /// 게임 패배 시 호출
    /// </summary>
    IEnumerator Co_GameOver() {
        IsLive = false;
        AudioManager.instance.PlayBgm(false);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Lose);
        yield return new WaitForSeconds(0.2f);
        restartUi.gameObject.SetActive(true);
        restartUi.Lose();
        Stop();

    }

    /// <summary>
    /// 게임 시작
    /// </summary>
    public void GameStart() {
        hp = MaxHp;
        IsLive = true;
        levelUp.Select(0);

        AudioManager.instance.PlayBgm(true);
        ReStart();
        assingBox.BoxInit(60);

    }

    /// <summary>
    /// 게임 승리
    /// </summary>
    public void GameVictory() {
        StartCoroutine(Co_GameVictory());
    }

    /// <summary>
    /// 게임 오버
    /// </summary>
    public void GameOver() {
        StartCoroutine(Co_GameOver());
    }

    /// <summary>
    /// 게임 재시작
    /// </summary>
    public void GameRetry() {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// 게임 일시정지
    /// </summary>
    public void Stop()
    {
        IsLive = false;
        Time.timeScale = 0;
    }

    /// <summary>
    /// 게임 재개
    /// </summary>
    public void ReStart()
    {
        IsLive = true;
        Time.timeScale = 1;
    }
    #endregion
}
