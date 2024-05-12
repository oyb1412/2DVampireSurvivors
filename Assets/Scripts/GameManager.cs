using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ��ü���� ���� �뷱�� ����
/// </summary>
public class GameManager : MonoBehaviour
{
    #region Variable
    public static GameManager instance;
    [Header("Object")]
    //�÷��̾�
    public Player player;
    //Ǯ�� ������Ʈ �Ŵ���
    public PoolManager pool;
    //������ �� ȣ��UI
    public LevelUp levelUp;
    //�¸� �� �й�� ȣ��UI
    public Restart restartUi;
    //�ֺ� �ֳʹ� óġ ������Ʈ 
    public GameObject EnemyCleaner;
    //�ı� ������ ���� Ŭ����
    public AssignBox assingBox;

    //���� �ð�(��)
    public float CurrentTime { get; private set; }
    //���� �ð�(��)
    public int MinTime { get; private set; }
    //ų
    public int KillNumber { get; private set; } 

    //������ �ʿ��� ����ġ
    public float[] MaxExp { get; private set; } = { 5, 10, 50, 100, 200, 400, 600, 1000 };
    //����
    public int Level { get; private set; }
    [Header("Status")]
    //ü��
    public float hp;
    //����ġ
    public float exp;
    //�ִ� ü��
    [field:SerializeField]public float MaxHp { get; private set; }
    //���� ����
    public bool IsLive { get; private set; }
    #endregion

    private void Awake()
    {
        //�̱���
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
    /// ���� �ð� ���� �� �÷��̾� ����ġ ���
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
    /// ų �� ����
    /// </summary>
    public void plusKill()
    {
        if (!IsLive)
            return;

        KillNumber++;
    }

    #region GameState

    /// <summary>
    /// ���� �¸� �� ȣ��
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
    /// ���� �й� �� ȣ��
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
    /// ���� ����
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
    /// ���� �¸�
    /// </summary>
    public void GameVictory() {
        StartCoroutine(Co_GameVictory());
    }

    /// <summary>
    /// ���� ����
    /// </summary>
    public void GameOver() {
        StartCoroutine(Co_GameOver());
    }

    /// <summary>
    /// ���� �����
    /// </summary>
    public void GameRetry() {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// ���� �Ͻ�����
    /// </summary>
    public void Stop()
    {
        IsLive = false;
        Time.timeScale = 0;
    }

    /// <summary>
    /// ���� �簳
    /// </summary>
    public void ReStart()
    {
        IsLive = true;
        Time.timeScale = 1;
    }
    #endregion
}
