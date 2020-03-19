using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floot : MonoBehaviour
{
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //float dis = Vector2.Distance(transform.localPosition, player.localPosition);

        //if(dis <= Player.Instance().distance)
        //{

        //}
        //else
        //{

        //}
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
        {
            Player.Instance().isUp = true;
            Player.Instance().isJump = false;
            GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Player.Instance().isUp = false;
        Player.Instance().isJump = true;
        GetComponent<BoxCollider2D>().isTrigger = false;
    }
}
