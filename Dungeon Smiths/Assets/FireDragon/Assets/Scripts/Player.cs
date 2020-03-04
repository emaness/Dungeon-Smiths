using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player : Singleton<Player>
{
    public float moveSpeed;
    public float jumpSpeed;

    public float distance = 0.5f;

    private CharacterController controller;


	private Animator ar;
    private Rigidbody2D rig;   //rigidbody

    private float horizontal;  
    private float vertical;  

    private float move; // horizontal speed left or right

    public bool isUp = false;
    public bool isJump = true;

    private bool isShow = true;
    public Joystick joystick;
    public Button jumpButton;

    void Start()
    {

        rig = GetComponent<Rigidbody2D>();   // get the rigidbody of player

        ar = GetComponent<Animator>();

        jumpButton = GameObject.Find("jumpButton").GetComponent<Button>();
        jumpButton.onClick.AddListener(() => jumpHandle());

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

        //if (isJump)
        //{
        //    if (Input.GetButtonDown())
        //    {

        //        rig.AddForce(new Vector2(0, jumpSpeed));   //upforce
        //    }
        //}


        horizontal = joystick.Horizontal; //Input.GetAxis("Horizontal");   // horizontal offset
  
        vertical = joystick.Vertical;//Input.GetAxis("Vertical");
       


        if (horizontal == 0&&vertical == 0)
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
            move = horizontal * moveSpeed;   // horizontal speed
            ar.SetTrigger("Walk");
            rig.velocity = new Vector2(move, rig.velocity.y);

            if (isUp)
            {
                if (vertical != 0)
                {
                    move = vertical * moveSpeed;   // vertical speed
                    rig.velocity = new Vector2(rig.velocity.x, move);

                }
            }
        }
    }

    private void jumpHandle()   // if jump button triggered
    {

        rig.AddForce(new Vector2(0, jumpSpeed));
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
        if (collision.tag == "Floor")
        {
            if (collision.GetComponent<Floor>().isAdd)
            {
                collision.GetComponent<BoxCollider2D>().isTrigger = false;
              collision.GetComponent<Floor>().isAdd = false;
            }

        }

        if (collision.tag == "1")
        {
            if (collision.GetComponent<Floor>().isAdd)
            {
                foreach (GameObject g in GameManager.Instance().floor1)
                {
                    g.GetComponent<BoxCollider2D>().isTrigger = false;
                    g.GetComponent<Floor>().isAdd = false;
                    g.GetComponent<Floor>().isShow = false;
                }


            }
           
         }

        if (collision.tag == "2")
        {
            if (collision.GetComponent<Floor>().isAdd)
           {
               foreach (GameObject g in GameManager.Instance().floor2)
                {
                    g.GetComponent<BoxCollider2D>().isTrigger = false;
                    g.GetComponent<Floor>().isAdd = false;
                    g.GetComponent<Floor>().isShow = false;
                }
           }

        }

        if (collision.tag == "3")
        {
            if (collision.GetComponent<Floor>().isAdd)
            {
                foreach (GameObject g in GameManager.Instance().floor3)
                {
                    g.GetComponent<BoxCollider2D>().isTrigger = false;
                    g.GetComponent<Floor>().isAdd = false;
                    g.GetComponent<Floor>().isShow = false;
                }
            }

        }

        if (collision.tag == "4")
        {
            if (collision.GetComponent<Floor>().isAdd)
            {
                foreach (GameObject g in GameManager.Instance().floor4)
                {
                    g.GetComponent<BoxCollider2D>().isTrigger = false;
                    g.GetComponent<Floor>().isAdd = false;
                    g.GetComponent<Floor>().isShow = false;
               }
            }

        }
    }


}
