using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RopeState
{
    idle,  //normal rotate
    elongation, 
    shorten
}
public class RopeManager : MonoBehaviour
{

    private Vector3 direction=new Vector3(0,0,1); //rotate direction (z axis)
    public float length = 1f;   // length of rope
    [HideInInspector]
    public RopeState rope_state; // state of rope
    public GameObject hook;
    public float ScaleSpeed = 1;
    public void Update()
    {
      
        if (rope_state == RopeState.idle)
        {
            ScaleSpeed = 1;
            RopeRotate();
            if (Input.GetMouseButtonDown(0)) 
            {
                rope_state = RopeState.elongation;
            }
        }
        else if (rope_state == RopeState.elongation)
        {
            RopeElongation();
        }
        else if (rope_state == RopeState.shorten)
        {
            RopeShorten();
        }
        
     

    }
    public void RopeRotate(){

        if (transform.localRotation.z <= -0.5f)
        {
            direction = Vector3.forward;
        }
        else if (transform.localRotation.z >= 0.5f)
        {
            direction = Vector3.back;  
        }
        transform.Rotate(direction);
    }
    
    public void RopeElongation()   // direction (y axis)
    {
      
        if (length >= 6f)
        {
            rope_state = RopeState.shorten;
            return;
        }
        length += Time.deltaTime*3*ScaleSpeed;
        transform.localScale = new Vector3(transform.localScale.x, length, transform.localScale.z);
        hook.transform.localScale= new Vector3(hook.transform.localScale.x, 1/length,hook.transform.localScale.z); //fix hook when rope elongates

    }
    public void RopeShorten()
    {
        if (length <= 1f)
        {
            rope_state = RopeState.idle;
            return;
        }
        length -= Time.deltaTime*3*ScaleSpeed;
        transform.localScale = new Vector3(transform.localScale.x, length, transform.localScale.z);
        hook.transform.localScale = new Vector3(hook.transform.localScale.x, 1 / length, hook.transform.localScale.z);

    }
}
