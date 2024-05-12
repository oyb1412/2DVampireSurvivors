using UnityEngine;

/// <summary>
/// �߻�ü ���� Ŭ����
/// </summary>
public class Bullet : MonoBehaviour
{
    public float Damage { get; private set; }
    private float weaponType;

    // �����
    private int count;
    private Rigidbody2D rigid;

    #region InitMethod
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// �߻�ü ���� �� �ʱ�ȭ
    /// </summary>
    /// <param name="damage">������</param>
    /// <param name="weaponType">�߻�ü Ÿ��</param>
    /// <param name="dirVec">���ư� ����</param>
    /// <param name="count">�����</param>
    public void Init(float damage, float weaponType, Vector3 dirVec, int count)
    {
        Damage = damage / 10;
        this.weaponType = weaponType;
        this.count = count;

        switch(weaponType)
        {
            case 0:
            case 1:
                rigid.velocity = dirVec * 15f;
                break;
            case 8:
                rigid.velocity = dirVec * 5f;
                break;
        }
     
    }

    /// <summary>
    /// Ǯ�� ������Ʈ�� ���� �߻�ü ��Ȱ��ȭ�� �ʱ�ȭ
    /// </summary>
    private void OnEnable()
    {
        transform.position = GameManager.instance.player.transform.position;
    }
    #endregion

    private void Update()
    {
        //���⿡ ���� �ٸ� �ൿ �ݺ�
        switch(weaponType)
        {
            case 0:
            case 1:
                Vector3 pos = transform.position - GameManager.instance.player.transform.position;
                if (pos.magnitude > 15f)
                    gameObject.SetActive(false);
                break;
            case 8:
                transform.Rotate(Vector3.forward, 800f * Time.deltaTime);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�浹 ����� ���� �ƴϰų� �ҷ��� �߻����� �ƴ� ��쿣 �Լ� ����
        if (!collision.CompareTag("Enemy") || weaponType != 1 && weaponType != 8)
            return;

        //�ҷ� ����� 1�� ����
        count--;
        //�ҷ� ������� 0���� ����������
        if(count < 0)
        {
            //�ҷ� ������ �ʱ�ȭ
            rigid.velocity = Vector3.zero;
            //�ҷ� ������Ʈ ��Ȱ��ȭ
            gameObject.SetActive(false);
        }
    }

    public void ExoplotionEnd() {
        gameObject.SetActive(false);
    }
}
