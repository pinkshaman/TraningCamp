using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour
{
    public Vector2 Movement;
    public float moveSpeed;
    public float inputX;
    public bool facingRight = true;

    public Animator anim;
    public Rigidbody2D rb;
    
    public void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void Flip()
    {
        facingRight = !facingRight;  // Đảo ngược giá trị của facingRight

        Vector3 scale = rb.transform.localScale;
        scale.x *= -1;  // Đảo ngược giá trị trục X để lật nhân vật
        rb.transform.localScale = scale;
    }
    public void SetDirection()
    {
        Debug.Log("Script is running");
        inputX = Input.GetAxis("Horizontal");
        Debug.Log("Horizontal Input: " + inputX);
        if (inputX > 0)
        {
            inputX = 1;
            Movement = Vector2.right;

            if (!facingRight) 
            {
                Flip();
            }
        }
        else if (inputX < 0)
        {
            inputX = -1;
            Movement = Vector2.left;

            if (facingRight) 
            {
                Flip();
            }
        }
        else
        {
            inputX = 0;
            Movement = Vector2.zero;
        }
    }

    public void Move()
    {
        SetDirection();
        if (inputX != 0)
        {
            anim.SetBool("isWalk", true);
            rb.velocity = new Vector2(Movement.x * moveSpeed, rb.velocity.y);
        }
        else
        {
            anim.SetBool("isWalk", false);
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }
}
