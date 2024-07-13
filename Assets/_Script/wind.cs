using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class wind : MonoBehaviour
{
    private Animator anim;
    private float direction;
    

    public void setDirectionDS(float _direction)
    {
        direction = _direction;
        gameObject.SetActive(true);


        float localSccaleX = transform.localScale.x;
        if (Mathf.Sign(localSccaleX) != _direction)
            localSccaleX = -localSccaleX;
        transform.localScale = new Vector3(localSccaleX, transform.localScale.y, transform.localScale.z);
       
    }
    void dasWindEffect()
    {
        
        if (Input.GetButton("Horizontal"))
        {
            gameObject.SetActive(true);
            anim.SetBool("isDashWind",true);
        }
        else
        {
            anim.SetBool("isDashWind",false);
            gameObject.SetActive(false);
        }
    }
   

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

    }



    // Update is called once per frame
    void Update()
    {
        dasWindEffect();
       
    }
}
