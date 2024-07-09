using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class Player : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpPower = 15.0f;
    public float skillSpeed = 30.0f;
    private Animator anim;
    private Rigidbody2D rb;
    Vector3 movement;
    private int direction;
    bool isJumping = false;
    private bool alive = true;
    float moveHorizontal;
    
    bool isCrouching = false; 
    public Collider2D col;
    
  
    // Start is called before the first frame update


    void onlanding()
    {
        if (col.IsTouchingLayers())
        {
            anim.SetBool("isJump", false);
            isJumping = false;
        }
        else
        {
            anim.SetBool("isJump", true);
            isJumping = true;
        }
    }
   
    void OnTriggerEnter2D(Collider2D collider)
    {


    }
   
    

    void Flip()
    {
        if (moveHorizontal < 0)
        {
            direction = -1;
            movement = Vector3.left;
            transform.localScale = new Vector3(direction, 1, 1);
        }
        else if (moveHorizontal > 0)
        {
            direction = 1;
            movement = Vector3.right;
            transform.localScale = new Vector3(direction, 1, 1);
        }
    }


    private void Move()
    {
        Flip();
        moveHorizontal = Input.GetAxis("Horizontal");

        if (Input.GetButton("Horizontal"))
        {

            if (isCrouching == false)
            {

                anim.SetBool("isWalk", true);
                transform.position += movement * speed * Time.deltaTime;
            }
            else
            {
                anim.SetBool("isWalk", false);
                float termSpeed = 0.0f;
                transform.position += movement * termSpeed * Time.deltaTime;
            }
        }
        else
        {
            anim.SetBool("isWalk", false);
        }
    }
    void Jump()
    {

        onlanding();
        if (isJumping == false)
        {
            if (Input.GetKeyDown(KeyCode.N))
            {
                rb.velocity = new Vector3(rb.velocity.x, jumpPower);
                //Vector2 jump = new Vector2(transform.position.x, jumpPower);
                anim.SetBool("isJump", true);
                transform.Translate(movement * jumpPower * Time.deltaTime);
            }
        }
    }
    void Crounch()
    {


        if (Input.GetKey(KeyCode.V))
        {
            Debug.Log(" Crouch is holdon");
            isCrouching = true;
            anim.SetBool("isCrouch", true);
        }
        else
        {
            isCrouching = false;
            anim.SetBool("isCrouch", false);
        }


    }
    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            anim.SetTrigger("Attack");
            anim.SetBool("isAttack", true);
            

        }
        else
        {
            anim.SetBool("isAttack", false);
            
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
        col = GetComponent<Collider2D>();

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
       
        Restart();
        if (alive)
        {

            //cameraFollow();          
            Hurt();
            Die();
            Attack();

            Crounch();
            Jump();
            Move();

        }
    }
}
