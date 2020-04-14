using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlatformerMovement : MonoBehaviour
{
    public Joystick moveStick;
    public Button jumpButton;

    public AudioClip bounceAudio;
    public AudioClip winAudio;
    public AudioSource audio;

    private Rigidbody2D rigid;
    private Animator anim;
    private bool inAir = false;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //1.0f is the distance from ground, 1 << 9 is a bitmask to check colliders with only platforms (horizontal)
        //1 << 8 is a bitmask to check colliders with only walls (vertical)
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 0.1f, 1 << 9);
        checkWalk(hit);
        checkWalk2(hit);
        RaycastHit2D hit1 = Physics2D.Raycast(transform.position, Vector2.right, 1.0f, 1 << 8);
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position, Vector2.left, 1.0f, 1 << 8);
        jumpButton.onClick.RemoveAllListeners();
        if (hit.collider != null)
        {
            //LEAVING A "BUG" IN HERE THAT WON'T WORK IN MOBILE:
            //IF YOU PRESS THE JUMP BUTTON AND THEN PRESS SPACE
            //YOU WILL HAVE MORE JUMP FORCE THAN USUAL.
            //THIS IS FOR PLAYTESTING LEVELS USING MORE JUMP
            //FORCE THAN INTENDED FOR THE PLAYER.
            inAir = true;
            //audio.PlayOneShot(bounceAudio);
            jumpButton.onClick.AddListener(() => checkJump2());
            checkJump();
        }
        if (hit.collider == null && inAir)
        {
            if (hit1.collider != null)
            {
                checkBounce(1); //right
                inAir = false;
                audio.PlayOneShot(bounceAudio);          
                //GetComponent<AudioSource>().Play();
            }
            if (hit2.collider != null)
            {
                checkBounce(2); //left
                inAir = false;
                audio.PlayOneShot(bounceAudio);
                //GetComponent<AudioSource>().Play();
            }
        }
        Vector3 gravityVector = new Vector3(0, -4.31f, 0);
        rigid.AddForce(gravityVector * Time.deltaTime, ForceMode2D.Impulse);
    }

    private void checkWalk(RaycastHit2D hit)
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
            if (hit.collider != null)
            {
                anim.SetTrigger("Walk");
            }
            else
            {
                anim.SetTrigger("Idle");
            }
        }
        else if (speed > 0)
        {
            Vector3 rotate = new Vector3(0, 0, 0);
            transform.eulerAngles = rotate;
            transform.Translate(speed, 0, 0);
            if (hit.collider != null)
            {
                anim.SetTrigger("Walk");
            }
            else
            {
                anim.SetTrigger("Idle");
            }
        }
    }

    private void checkWalk2(RaycastHit2D hit)
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
            if (hit.collider != null)
            {
                anim.SetTrigger("Walk");
            }
            else
            {
                anim.SetTrigger("Idle");
            }
        }
        else if (speed > 0)
        {
            Vector3 rotate = new Vector3(0, 0, 0);
            transform.eulerAngles = rotate;
            transform.Translate(speed, 0, 0);
            if (hit.collider != null)
            {
                anim.SetTrigger("Walk");
            }
            else
            {
                anim.SetTrigger("Idle");
            }
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            audio.PlayOneShot(winAudio);
            SceneManager.LoadScene("Level 3");
        }
    }
}
