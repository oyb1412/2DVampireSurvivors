using UnityEngine;

/// <summary>
/// �÷��̾� ������ ����
/// </summary>
public class Player : MonoBehaviour
{
    #region Variable
    public float speed;
    private float audioTimer = 0;
    private Rigidbody2D rigid;
    private SpriteRenderer spriter;
    private Animator animator;
    public float BaseSpeed { get; private set; }
    public Vector2 InputVec { get; private set; }
    public Scanner Scanner { get; private set; }

    #endregion

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        Scanner = GetComponent<Scanner>();
        BaseSpeed = speed;
    }

    #region UpdateMethod
    void Update()
    {
        if (!GameManager.instance.IsLive)
            return;

        InputVec = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if(InputVec.x != 0)
            spriter.flipX = InputVec.x > 0;
    }

    private void FixedUpdate()
    {
        if (!GameManager.instance.IsLive)
            return;

        audioTimer += Time.deltaTime;

        //������ٵ� �̵�
        rigid.MovePosition(rigid.position + InputVec * (Time.fixedDeltaTime * (speed / 10)) );
    }

    /// <summary>
    /// �ֳʹ̿��� �浹 ���
    /// </summary>
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!GameManager.instance.IsLive || !collision.gameObject.CompareTag("Enemy"))
            return;

        if(audioTimer >0.5f)
        {
            //�ǰ�ȿ���� ����
            AudioManager.instance.PlaySfx(AudioManager.Sfx.PlayerHit);
            audioTimer = 0;
        }

        GameManager.instance.hp -= 10f * Time.deltaTime;

        if (GameManager.instance.hp < 0)
        {
            for (int i = 1; i < transform.childCount; i++)
            {
                transform.GetChild(1).gameObject.SetActive(false);
            }
            animator.SetTrigger("Dead");
            GameManager.instance.GameOver();
        }
    }
    #endregion

    /// <summary>
    /// �ִϸ��̼� ����
    /// </summary>
    public void ChargeaAnimation(bool index)
    {
        animator.SetBool("Charge", index);
    }
}
