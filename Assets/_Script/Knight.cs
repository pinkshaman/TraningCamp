using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour
{
    public Vector2 Movement;
    public float moveSpeed;
    public float Direction;


    public Animator anim;
    public Rigidbody2D rb;
    
    public void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void Flip()
    {
       
    }
    public void SetDirection()
    {
        Direction = Input.GetAxis("Horizontal");
        if (Direction > 0) { Direction = 1; }
        else if (Direction < 0) { Direction = -1; }
        else { Direction = 0; }
        Movement = Vector2.right;
        rb.transform.localScale = new Vector2(Direction, 1);
        Debug.Log($"{Direction}");
    }
    public void Move()
    {   if (Input.GetButton("Horizontal"))
        {
            SetDirection();
            rb.transform.position = Movement * moveSpeed * Time.deltaTime;
        }
    }
}
