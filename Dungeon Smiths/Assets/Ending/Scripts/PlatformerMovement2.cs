using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlatformerMovement2 : MonoBehaviour
{
    public Joystick moveStick;
    public Button jumpButton;
    public Button fireButton;
    private Rigidbody2D rigid;
    private Animator anim;

    public AudioSource audio;
    public AudioClip winAudio;
    public AudioClip bounceAudio;

    public int health = 100;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        fireButton.onClick.AddListener(() => checkFire2());
    }

    // Update is called once per frame
    void Update()
    {
        //1.0f is the distance from ground, 1 << 9 is a bitmask to check colliders with only platforms (horizontal)
        //1 << 8 is a bitmask to check colliders with only walls (vertical)
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 0.1f, 1 << 9);
        checkWalk(hit);
        checkWalk2(hit);
        checkFire();
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
            jumpButton.onClick.AddListener(() => checkJump2());
            checkJump();
        }
        Vector3 gravityVector = new Vector3(0, -9.81f, 0);
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
        if (Input.GetButtonDown("Jump"))
        {
            Vector2 jump_force = new Vector2(0, 750.0f);
            rigid.AddForce(jump_force);
        }
    }

    private void checkJump2()
    {
        Vector2 jump_force = new Vector2(0, 750.0f);
        rigid.AddForce(jump_force);
    }

    private void checkFire()
    {
        if (Input.GetKeyDown("z"))
        {
            print("AHADH");
        }
    }

    private void checkFire2()
    {
        print("AHADH");
    }
}
