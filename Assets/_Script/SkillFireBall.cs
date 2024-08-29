using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.Windows;

public class SkillFireBall : MonoBehaviour
{
    public Player setDirection;
    public Collider2D effect;
    private Animator anim;
    private bool hit;
    public float speed = 30.0f;
    public float timeDestroy = 0.25f;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("onHit");
        hit = true;
        effect.enabled = false;
        anim.SetTrigger("onHit");
        Destroy(gameObject, timeDestroy);
        //StartCoroutine(Effect());

    }
    //public IEnumerator Effect()
    //{
    //    OnHit();
    //    yield return new WaitForSeconds(0.25f);
    //    Deactivate();
    //}
    //public void OnHit()
    //{
    //    anim.SetTrigger("onHit");
    //}

    private void Deactivate()
    {
        Destroy(gameObject, timeDestroy);
    }
   
    void Start()
    {
        effect = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hit) return;      
        float skillDistance = speed * Time.deltaTime ;
        transform.Translate(skillDistance, 0.0f, 0.0f);
        anim.SetTrigger("FireBall");

    }

}
