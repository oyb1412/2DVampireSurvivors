using UnityEngine;

/// <summary>
/// 애너미 스폰 관리
/// </summary>
public class Spawner : MonoBehaviour
{
    #region Variable
    //스폰 포인트를 랜덤으로 하기 위해 자식 오브젝트로 여러개 설정. 저장하기 위한 배열 변수
    [SerializeField]private Transform[] spawnerPoint;
    [SerializeField]private SpawnDate[] spawnDate;
    [SerializeField] private float eleteTimer;

    private float eleteSpawn = 5;
    //애너미의 스폰율을 조정하기 위한 타이머
    private float timer;
    private int gameLevel = 0;

    #endregion

    void Start()
    {
        //자식 오브젝트로 초기화
        spawnerPoint = GetComponentsInChildren<Transform>();
    }

    void Update()
    {
        if (!GameManager.instance.IsLive)
            return;

        gameLevel = Mathf.Min(GameManager.instance.MinTime , spawnDate.Length - 2);
        Spawn();
    }
    
    /// <summary>
    /// 애너미 스폰
    /// </summary>
    void Spawn()
    {
        timer += Time.deltaTime;
        eleteTimer += Time.deltaTime;
        if(eleteTimer > eleteSpawn)
        {
            //사용할 프리펩을 파라매터로 입력
            GameObject enemy = GameManager.instance.pool.Get(0);

            //스폰된 애너미의 위치는 여러개의 스폰 포인트중 랜덤하게 지정
            enemy.transform.position = spawnerPoint[UnityEngine.Random.Range(1, spawnerPoint.Length)].position;
            enemy.GetComponent<Enemy>().Init(spawnDate[5]);
            eleteTimer = 0;
        }
        if (timer > spawnDate[gameLevel].spawnTime)
        {
            //사용할 프리펩을 파라매터로 입력
            GameObject enemy = GameManager.instance.pool.Get(0);
            //스폰된 애너미의 위치는 여러개의 스폰 포인트중 랜덤하게 지정
            enemy.transform.position = spawnerPoint[UnityEngine.Random.Range(1, spawnerPoint.Length)].position;
            enemy.GetComponent<Enemy>().Init(spawnDate[gameLevel]);
            timer = 0;
        }
    }
}

/// <summary>
/// 스폰 애너미 데이터
/// </summary>
[System.Serializable]
public class SpawnDate
{
    public float spawnTime;
    public int SpriteType;
    public int health;
    public float speed;
}