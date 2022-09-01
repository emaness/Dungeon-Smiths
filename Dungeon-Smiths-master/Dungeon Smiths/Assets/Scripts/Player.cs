using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    public float moveSpeed;
    public float jumpSpeed;

    public float distance = 0.5f;

    private CharacterController controller;


	private Animator ar;
    private Rigidbody2D rig;  

    private float horizontal;  
    private float vertical;  

    private float move; // left or right speed

    public bool isUp = false;
    public bool isJump = true;
 

    void Start()
    {

        rig = GetComponent<Rigidbody2D>();   //get rigidbody for player

        ar = GetComponent<Animator>();

    }

    void Update()
    {
        if(transform.localPosition.x >= 6.0f)
        {
            transform.localPosition =new Vector3(6.0f, transform.position.y, transform.position.z);
        }

        if (transform.localPosition.x <=  -6.0f)
        {
            transform.localPosition = new Vector3(-6.0f, transform.position.y, transform.position.z);
        }

        if (isJump)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {  

                rig.AddForce(new Vector2(0, jumpSpeed));   //Upward force
            }
        }
        

        horizontal = Input.GetAxis("Horizontal");   //horizontal offset
        vertical = Input.GetAxis("Vertical");

          
        if(horizontal == 0&&vertical == 0)
        {
            ar.SetTrigger("Idle");
        }
        else
        {
            if(horizontal > 0)
            {
                transform.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
            }

            if (horizontal < 0)
            {
                transform.localEulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
            }

            move = horizontal * moveSpeed;   //specific speed
            ar.SetTrigger("Walk");
            rig.velocity = new Vector2(move, rig.velocity.y);

            if (isUp)
            {
                if (vertical != 0)
                {
                    move = vertical * moveSpeed;  
                    rig.velocity = new Vector2(rig.velocity.x, move);

                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Enemy")
        {
            GameManager.Instance().SetInfo("GAME OVER");
        }

        if (collision.collider.tag == "Door")
        {
            GameManager.Instance().SetInfo("YOU WIN");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Floor")
        {
            collision.GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }
}
