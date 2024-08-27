using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using UnityEngine.Windows;
using static UnityEngine.GraphicsBuffer;
using Input = UnityEngine.Input;

public class Player : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpPower = 15.0f;
    public float skillSpeed = 30.0f;
    private Animator anim;
    private Rigidbody2D rb;
    public Collider2D col;
    public Transform firePoint;
    public GameObject PrefabsFireBall;
    public Transform windpPoint;
    public GameObject Wind;
    public bool isGrounded = true;
    public bool canMove = true;
    public bool isJumping = false;
    private bool alive = true;
    public bool isCrouching = false;
    public bool isMoving = false;
    public bool isDash = false;
    public Vector3 movement;
    public float inputX;
    public float dashDistance = 10.0f;
    public float skillCooldown = Mathf.Infinity;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("igrounded = true");
            isGrounded = true;
        }
        else
        {
            Debug.Log("igrounded = false");
            isGrounded = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {

    }

    public void Flip()
    {
        if (inputX != 0)
        {
            movement = inputX > 0 ? Vector3.right : Vector3.left;
            rb.transform.localScale = new Vector3(Mathf.Sign(inputX), 1, 0);
            isMoving = true;
        }
        else
        {
            movement = Vector3.zero;
            isMoving = false;
        }

    }

    public void Move()
    {
        Flip();
        if (canMove )
        {
            rb.transform.position += speed * Time.deltaTime * movement;
            anim.SetFloat("speed", Mathf.Abs(inputX));

        }
    }
    public void Jump()
    {

        if (isGrounded == true)
        {
            if (Input.GetButton("Jump"))
            {
                anim.SetTrigger("Jump");

                Vector3 jumpForce = new(0, jumpPower, 0);
                rb.AddForce(jumpForce, ForceMode2D.Impulse);
                Debug.Log("Junp");
            }
        }
    }
    public void Dash()
    {

        if (Input.GetKeyDown(KeyCode.N))
        {
            Flip();
            //anim.SetBool("isDash", true);
            Vector3 dashDirection = new(Mathf.Sign(inputX) * dashDistance, 0, 0);
            Debug.Log("Dashing");
            anim.SetTrigger("Dash");
            rb.AddForce(dashDirection, ForceMode2D.Impulse);
            isDash = true;
        }
        

    }
    public void Crounch()
    {

        if (Input.GetKey(KeyCode.V))
        {
            canMove = false;
            anim.SetFloat("speed", 0);
            Debug.Log(" Crouch is holdon");
            isCrouching = true;
            anim.SetBool("isCrouch", true);
        }
        else
        {
            isCrouching = false;
            anim.SetBool("isCrouch", false);
            canMove = true;
        }
    }
    public void Attack()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {

            anim.SetTrigger("Attack");
            anim.SetBool("isAttack", true);

            skillCooldown = 0;

        }
        else
        {
            anim.SetBool("isAttack", false);

        }
    }
    public void Restart()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            anim.SetTrigger("idle");
            alive = true;
        }
    }
    public void Hurt()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            anim.SetTrigger("hurt");
        }
    }
    public void Die()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            anim.SetTrigger("die");
            alive = false;
        }
    }
    public void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

    }

    // Update is called once per frame
    public void Update()
    {
        inputX = Input.GetAxis("Horizontal");
        skillCooldown += Time.deltaTime;
        Restart();
        if (alive)
        {

            //cameraFollow();          
            Hurt();
            Die();
            Attack();
            Dash();
            Crounch();
            Jump();
            Move();

        }
    }
}
