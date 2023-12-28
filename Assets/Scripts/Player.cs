using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float baseSpeed;
    Vector2 playerVec;
    public Vector2 inputVec;
    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator animator;
    public Scanner scanner;

    float audioTimer = 0;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
        baseSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.isLive)
            return;

        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");

        if(inputVec.x != 0)
            spriter.flipX = inputVec.x > 0;
    }

    private void FixedUpdate()
    {
        if (!GameManager.instance.isLive)
            return;

        audioTimer += Time.deltaTime;

        rigid.MovePosition(rigid.position + inputVec * (Time.deltaTime * (speed / 10)) );
    }

 

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!GameManager.instance.isLive || !collision.gameObject.CompareTag("Enemy"))
            return;


        if(audioTimer >0.5f)
        {
            //피격효과음 실행
            AudioManager.instance.PlayerSfx(AudioManager.Sfx.PlayerHit);
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

    public void ChargeaAnimation(bool index)
    {
        animator.SetBool("Charge", index);
    }
}
