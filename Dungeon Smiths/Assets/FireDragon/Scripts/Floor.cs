using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    public bool isAdd = true;

    public bool isShow = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        {
            if (isShow)
            {
                GetComponent<BoxCollider2D>().isTrigger = true;
                isShow = false;
            }
            
        }
    }
}
