using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skill : MonoBehaviour
{
    public string skillName;
    public string skillDescription;
    public string skillType;
    public string skillLevel;
    
    public Collider2D effect;
    public Vector3 skillRange;    
    
    private Animator anim; 
    private bool hit;
    public float speed = 30.0f;
    float direction;

  

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;       
        anim.SetBool("isOnHit",true);
        anim.SetTrigger("explosion");
        effect.enabled = false;

    }


    public void setDirection(float _direction)
    {
        effect.enabled = true;
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;

        float localSccaleX = transform.localScale.x;
        if (Mathf.Sign(localSccaleX) != _direction)
            localSccaleX = -localSccaleX;
        transform.localScale = new Vector3(localSccaleX, transform.localScale.y, transform.localScale.z);
    }


    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        effect = GetComponent<CustomCollider2D>();
        anim = GetComponent<Animator>();


    }

    // Update is called once per frame
    void Update()
    {
        OnTriggerEnter2D(effect);
        if (hit) return;
        float skillSpeed = speed * Time.deltaTime * direction;
        transform.Translate(skillSpeed, 0.0f, 0.0f);
        anim.SetTrigger("FireBall");
       
    }

}
