using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class wind : MonoBehaviour
{
    public Animator anim;
    public Player directionDash;  
    void dasWindEffect()
    {      
        transform.localScale = directionDash.movement;
        gameObject.SetActive(true);
        anim.SetBool("isDashWind", true);
    }


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        directionDash = FindObjectOfType<Player>();
    }
   // Update is called once per frame
    void Update()
    {
        dasWindEffect();

    }
}
