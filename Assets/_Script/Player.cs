using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
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
    public GameObject WindPrefabs;

    public bool isGrounded = true;
    public bool canMove = true;
    public bool isJumping = false;
    private bool alive = true;
    public bool isCrouching = false;
    public bool isMoving = false;
    public bool isDash = false;
    public bool isDizzy = false;

    public Vector3 movement;
    public float inputX;
    public float dashDistance;
    public float dashSpeed;
    public float dashDuration;
    public float attackCount;
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            anim.SetBool("isGrounded", true);
            isGrounded = true;
            isJumping = false;
        }
        else
        {
            isGrounded = false;
        }
        Debug.Log($"isGrounded: {isGrounded}");
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {

    }

    public void Flip()
    {
        if (inputX != 0)
        {
            movement = inputX > 0 ? Vector3.right : Vector3.left;
            rb.transform.localScale = new Vector3(Mathf.Sign(inputX), transform.localScale.y, 0);
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
        if (canMove)
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                canMove = false;
                StartCoroutine(Dash());
            }
            rb.transform.position += speed * Time.deltaTime * movement;
            anim.SetFloat("speed", Mathf.Abs(inputX));

        }
    }
    public void Jump()
    {

        if (isGrounded == true && isJumping == false && isDash == false)
        {
            if (Input.GetKeyDown(KeyCode.N))
            {
                anim.SetTrigger("Jump");
                anim.SetBool("isGrounded", false);
                isGrounded = false;
                Vector3 jumpForce = new(0, jumpPower, 0);
                rb.AddForce(jumpForce, ForceMode2D.Impulse);
                Debug.Log("Junp");
            }
        }
    }
    public IEnumerator AttackCount()
    {
        attackCount++;
        Debug.Log($"AttackCount: {attackCount}");
        yield return new WaitForSeconds(2.0f);
        attackCount = 0;
    }


    public void Cast()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            anim.SetTrigger("Cast");
            Debug.Log("Cast");
            Instantiate(PrefabsFireBall, firePoint);

        }
    }
    public void Block()
    {
        if (Input.GetKey(KeyCode.F))
        {
            anim.SetBool("isBlock", true);
            Debug.Log("Block");
        }
        else
        { anim.SetBool("isBlock", false); }
    }
    public void Dizzy()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            anim.SetTrigger("dizzy");
            Debug.Log("Dizzy");
            isDizzy = true;
        }
        else { isDizzy = false; }

    }


    private IEnumerator Dash()
    {
        {
            if (isJumping == false)
            {
                isDash = true;
                canMove = false;
                float dashDirection = transform.localScale.x; // Xác định hướng dash theo chiều của nhân vật
                Vector3 dashForce = new Vector3(dashDirection * dashSpeed, 0, 0); // Tạo lực dash dựa trên hướng
                rb.velocity = Vector3.zero; // Đặt lại vận tốc để không bị ảnh hưởng bởi các lực khác
                rb.AddForce(dashForce, ForceMode2D.Impulse); // Áp dụng lực dash với chế độ Impulse

                if (anim != null)
                {
                    anim.SetTrigger("Dash");
                    anim.SetBool("isDash", true);
                }
                if (WindPrefabs != null)
                {
                    GameObject effect = Instantiate(WindPrefabs, transform.position, Quaternion.identity);
                    effect.transform.localScale = transform.localScale; // Flip hiệu ứng theo hướng nhân vật
                    effect.transform.position = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);

                    // Xóa hiệu ứng sau khi hoạt ảnh kết thúc
                    Animator effectAnimator = effect.GetComponent<Animator>();
                    float animationLength = effectAnimator ? effectAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.length : dashDuration;
                    Destroy(effect, animationLength);
                }

                yield return new WaitForSeconds(dashDuration); // Chờ thời gian Dash

                isDash = false; // Đặt lại trạng thái Dash
                canMove = true; // Cho phép di chuyển trở lại
                anim.SetBool("isDash", false); // Tắt trạng thái Dash
                anim.ResetTrigger("Dash"); // Reset trigger để ngăn ngừa Dash liên tục
            }
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
            StartCoroutine(AttackCount());
            canMove = false;           
            anim.SetTrigger("Attack");
            anim.SetBool("isAttack", true);

            if (attackCount == 3)
            {
                anim.SetTrigger("Strike");
            }
            else
            {
                anim.SetTrigger("Attack");              
            }
        }
        else
        {
            anim.SetBool("isAttack", false);
            canMove = true;
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
        Restart();
        if (alive)
        {
            Hurt();
            Die();
            Attack();
            Crounch();
            Jump();
            Move();
            Cast();
            Block();
            Dizzy();
            
        }
    }
}
