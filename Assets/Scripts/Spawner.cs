using UnityEngine;

/// <summary>
/// �ֳʹ� ���� ����
/// </summary>
public class Spawner : MonoBehaviour
{
    #region Variable
    //���� ����Ʈ�� �������� �ϱ� ���� �ڽ� ������Ʈ�� ������ ����. �����ϱ� ���� �迭 ����
    [SerializeField]private Transform[] spawnerPoint;
    [SerializeField]private SpawnDate[] spawnDate;
    [SerializeField] private float eleteTimer;

    private float eleteSpawn = 5;
    //�ֳʹ��� �������� �����ϱ� ���� Ÿ�̸�
    private float timer;
    private int gameLevel = 0;

    #endregion

    void Start()
    {
        //�ڽ� ������Ʈ�� �ʱ�ȭ
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
    /// �ֳʹ� ����
    /// </summary>
    void Spawn()
    {
        timer += Time.deltaTime;
        eleteTimer += Time.deltaTime;
        if(eleteTimer > eleteSpawn)
        {
            //����� �������� �Ķ���ͷ� �Է�
            GameObject enemy = GameManager.instance.pool.Get(0);

            //������ �ֳʹ��� ��ġ�� �������� ���� ����Ʈ�� �����ϰ� ����
            enemy.transform.position = spawnerPoint[UnityEngine.Random.Range(1, spawnerPoint.Length)].position;
            enemy.GetComponent<Enemy>().Init(spawnDate[5]);
            eleteTimer = 0;
        }
        if (timer > spawnDate[gameLevel].spawnTime)
        {
            //����� �������� �Ķ���ͷ� �Է�
            GameObject enemy = GameManager.instance.pool.Get(0);
            //������ �ֳʹ��� ��ġ�� �������� ���� ����Ʈ�� �����ϰ� ����
            enemy.transform.position = spawnerPoint[UnityEngine.Random.Range(1, spawnerPoint.Length)].position;
            enemy.GetComponent<Enemy>().Init(spawnDate[gameLevel]);
            timer = 0;
        }
    }
}

/// <summary>
/// ���� �ֳʹ� ������
/// </summary>
[System.Serializable]
public class SpawnDate
{
    public float spawnTime;
    public int SpriteType;
    public int health;
    public float speed;
}