using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookManager : MonoBehaviour
{
    public RopeManager ropeManager;
    private void Start()
    {
        ropeManager = GetComponentInParent<RopeManager>();
        
    }
    private void Update()
    {
        if (ropeManager.rope_state == RopeState.elongation)   // when rope is enlongating, hook must be active
        {
            transform.GetComponent<PolygonCollider2D>().enabled = true;
        }
        if (ropeManager.length <= 1f)  // when hook go back to origin
        {
            if (transform.childCount > 0)   
            {
                Destroy(transform.GetChild(0).gameObject);
            }
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "bomb")
        { 
            Debug.Log("game over");
            return;
        }
        ropeManager.rope_state = RopeState.shorten;
        collision.transform.parent = transform;  
        collision.transform.localPosition = new Vector3(0,-1,0);        
        transform.GetComponent<PolygonCollider2D>().enabled = false;
        collision.GetComponent<PolygonCollider2D>().enabled = false;  // rock collider close
        if (collision.transform.tag == "rock")
        {
            ropeManager.ScaleSpeed = 0.6f;
        }
     

    }
}
