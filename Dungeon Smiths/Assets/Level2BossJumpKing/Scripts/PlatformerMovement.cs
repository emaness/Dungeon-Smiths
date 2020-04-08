using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerMovement : MonoBehaviour
{
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
        //1.0f is the distance from ground, 1 << 9 is a bitmask to check colliders with only platforms
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 1.0f, 1 << 9);
        RaycastHit2D hit1 = Physics2D.Raycast(transform.position, Vector2.right, 1.0f, 1 << 8);
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position, Vector2.left, 1.0f, 1 << 8);
        if (hit.collider != null)
        {
            inAir = true;
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

    void checkWalk()
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

    void checkJump()
    {
        if(Input.GetButtonDown("Jump"))
        {
            Vector2 jump_force = new Vector2(0, 500.0f);
            rigid.AddForce(jump_force);
        }
    }

    void checkBounce(int check)
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
