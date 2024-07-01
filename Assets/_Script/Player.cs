using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpPower = 15.0f;
    private Animator anim;
    private Rigidbody2D rb;
    Vector2 movement;
    private int direction;
    bool isJumping = false;
    private bool alive = true;
    float moveHorizontal;
    public GameObject Camera;

    // Start is called before the first frame update


    void onlanding()
    {
        if (!isJumping)
        {
            Vector2 jump = Vector2.zero;
        }
        
    }


    void Flip()
    {
        if (moveHorizontal < 0)
        {
            direction = -1;
            movement = Vector2.left;
            transform.localScale = new Vector2(direction, 1);
        }
        else if (moveHorizontal > 0)
        {
            direction = 1;
            movement = Vector2.right;
            transform.localScale = new Vector2(direction, 1);
        }
    }

    private void Move()
    {
        Flip();
        moveHorizontal = Input.GetAxis("Horizontal");

        if (Input.GetButton("Horizontal"))
        {
            anim.SetBool("isWalk", true);
            transform.position = new Vector2(transform.position.x, transform.position.y);
            Vector2 movement = new Vector2(moveHorizontal, transform.position.x);
            transform.Translate(movement * speed * Time.deltaTime);
        }
        else
        {
            anim.SetBool("isWalk", false);

        }

    }
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            Vector2 jump = new Vector2(transform.position.x, jumpPower);
            anim.SetBool("isJump", true);
            transform.position = jump;
            isJumping = true;
        }
        else
        {
            anim.SetBool("isJump", false);
            isJumping = false;
        }

        onlanding();

    }
    void Crounch()
    {


        if (Input.GetButton("V"))
        {
            anim.SetBool("Coungh", true);
        }
        else
        {
            anim.SetBool("Coungh", false);
        }


    }
    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            anim.SetTrigger("Attack");
        }
    }
    void Restart()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            anim.SetTrigger("idle");
            alive = true;
        }
    }
    void Hurt()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            anim.SetTrigger("hurt");

        }
    }
    void Die()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            anim.SetTrigger("die");
            alive = false;
        }
    }
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    private void FixedUpdate()
    {

        Restart();
        if (alive)
        {

            Hurt();
            Die();
            Attack();
            Jump();
            Move();

        }
    }
}
