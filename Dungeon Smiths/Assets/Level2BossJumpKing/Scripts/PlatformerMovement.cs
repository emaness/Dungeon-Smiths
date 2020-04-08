using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlatformerMovement : MonoBehaviour
{
    public Joystick moveStick;
    public Button jumpButton;
    private Rigidbody2D rigid;
    private Animator anim;
    private bool inAir = false;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        checkWalk();
        checkWalk2();
        //1.0f is the distance from ground, 1 << 9 is a bitmask to check colliders with only platforms (horizontal)
        //1 << 8 is a bitmask to check colliders with only walls (vertical)
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 0.1f, 1 << 9);
        RaycastHit2D hit1 = Physics2D.Raycast(transform.position, Vector2.right, 0.1f, 1 << 8);
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position, Vector2.left, 0.1f, 1 << 8);
        jumpButton.onClick.RemoveAllListeners();
        if (hit.collider != null)
        {
            //LEAVING A "BUG" IN HERE THAT WON'T WORK IN MOBILE:
            //IF YOU PRESS THE JUMP BUTTON AND THEN PRESS SPACE
            //YOU WILL HAVE MORE JUMP FORCE THAN USUAL.
            //THIS IS FOR PLAYTESTING LEVELS USING MORE JUMP
            //FORCE THAN INTENDED FOR THE PLAYER.
            inAir = true;
            jumpButton.onClick.AddListener(() => checkJump2());
            checkJump();
        }
        if (hit.collider == null && inAir)
        {
            if (hit1.collider != null)
            {
                checkBounce(1); //right
                inAir = false;
            }
            if (hit2.collider != null)
            {
                checkBounce(2); //left
                inAir = false;
            }
        }
        Vector3 gravityVector = new Vector3(0, -4.31f, 0);
        rigid.AddForce(gravityVector * Time.deltaTime, ForceMode2D.Impulse);
    }

    private void checkWalk()
    {
        float speed = Input.GetAxis("Horizontal") * 15.0f * Time.deltaTime;
        if (speed == 0)
        {
            anim.SetTrigger("Idle");
        }
        else if (speed < 0)
        {
            Vector3 rotate = new Vector3(0, 180, 0);
            transform.eulerAngles = rotate;
            transform.Translate(-speed, 0, 0);
            anim.SetTrigger("Walk");
        }
        else if (speed > 0)
        {
            Vector3 rotate = new Vector3(0, 0, 0);
            transform.eulerAngles = rotate;
            transform.Translate(speed, 0, 0);
            anim.SetTrigger("Walk");
        }
    }

    private void checkWalk2()
    {
        float speed = moveStick.Horizontal * 15.0f * Time.deltaTime;
        if (speed == 0)
        {
            anim.SetTrigger("Idle");
        }
        else if (speed < 0)
        {
            Vector3 rotate = new Vector3(0, 180, 0);
            transform.eulerAngles = rotate;
            transform.Translate(-speed, 0, 0);
            anim.SetTrigger("Walk");
        }
        else if (speed > 0)
        {
            Vector3 rotate = new Vector3(0, 0, 0);
            transform.eulerAngles = rotate;
            transform.Translate(speed, 0, 0);
            anim.SetTrigger("Walk");
        }
    }

    private void checkJump()
    {
        if(Input.GetButtonDown("Jump"))
        {
            Vector2 jump_force = new Vector2(0, 500.0f);
            rigid.AddForce(jump_force);
        }
    }

    private void checkJump2()
    {
        Vector2 jump_force = new Vector2(0, 500.0f);
        rigid.AddForce(jump_force);
    }

    private void checkBounce(int check)
    {
        if (check == 1)
        {
            Vector2 bounce_force = new Vector2(-50.0f, 300.0f);
            rigid.AddForce(bounce_force);
            Vector3 rotate = new Vector3(0, 180, 0);
            transform.eulerAngles = rotate;
        }
        if (check == 2)
        {
            Vector2 bounce_force = new Vector2(50.0f, 300.0f);
            rigid.AddForce(bounce_force);
            Vector3 rotate = new Vector3(0, 0, 0);
            transform.eulerAngles = rotate;
        }
    }
}
