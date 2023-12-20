using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    Vector2 playerVec;
    public Vector2 inputVec;
    Rigidbody2D rigid;
    SpriteRenderer spriter;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");

        if(inputVec.x != 0)
            spriter.flipX = inputVec.x < 0;
   
    }

    private void FixedUpdate()
    {
        rigid.MovePosition(rigid.position + inputVec * speed);
    }
}
