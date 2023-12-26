using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    Vector2 playerVec;
    public Vector2 inputVec;
    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator animator;
    public Scanner scanner;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
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

        rigid.MovePosition(rigid.position + inputVec * speed);
    }

 

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!GameManager.instance.isLive || !collision.gameObject.CompareTag("Enemy"))
            return;

        GameManager.instance.hp -= 10 * Time.deltaTime;

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
}
