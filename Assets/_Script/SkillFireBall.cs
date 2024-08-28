using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class SkillFireBall : MonoBehaviour
{
    public Player setDirection;
    public Collider2D effect;
    public Vector3 skillRange;
    private Animator anim;
    private bool hit;
    public float speed = 30.0f;
    

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("onHit");
        hit = true;
        //anim.SetBool("isOnHit", true);
        effect.enabled = false;
        anim.SetTrigger("onHit");
        
    }


    private void Deactivate()
    {
       Destroy(gameObject);
    }
  
    void Start()
    {
        effect = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        setDirection = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hit) return;
        skillRange = setDirection.movement;
        float skillSpeed = speed * Time.deltaTime;
        transform.Translate(skillSpeed, 0.0f, 0.0f);
        anim.SetTrigger("FireBall");

    }

}
