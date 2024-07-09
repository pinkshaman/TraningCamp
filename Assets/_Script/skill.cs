using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skill : MonoBehaviour
{
    public string skillName;
    public string skillDescription;
    public string skillType;
    public string skillLevel;
    public float skillspeed;
    public Collider2D effect;
    public Vector3 skillRange;
    public Transform posittionChar;
    public Vector3 skillPosition;
    private Animator anim;

    void skillRan()
    {
        skillRange = new Vector3 (skillPosition.x,skillPosition.y+30.0f,skillPosition.z);
       

    }
    // Start is called before the first frame update
    void Start()
    {

        Vector3 skillPosition = posittionChar.transform.position;
        anim= GetComponent<Animator>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("C"))
        {
            gameObject.SetActive(true);
            anim.SetTrigger("FireBall");
            skillRan();
        }
        else
        {
            Vector3 skillPosition = posittionChar.transform.position;
        }
    }
}
