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
    //public AudioClip bounceAudio;
    public AudioClip knifeThrow;
    public AudioClip damageSound;

    public int health = 100;
    public GameObject knife;
    public bool fireable = true;
    private float countdown = 1.0f;
    

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
        if (fireable)
        {
            checkFire();
        }
        else if (!(fireable) && (countdown > 0.0f))
        {
            countdown -= Time.deltaTime;
        }
        else if (countdown <= 0.0f)
        {
            countdown = 1.0f;
            fireable = true;
        }
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
            audio.PlayOneShot(knifeThrow);
            anim.SetTrigger("Attack3");
            fireable = false;
            StartCoroutine("waitAnimation", 0.3f);
        }
    }

    private void checkFire2()
    {
        if (fireable)
        {
            anim.SetTrigger("Attack3");
            fireable = false;
            StartCoroutine("waitAnimation", 0.3f);
        }
    }

    private IEnumerator waitAnimation(float time)
    {
        yield return new WaitForSeconds(time);
        Vector3 target = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
        GameObject shot = Instantiate(knife, target, transform.rotation);
        Vector3 rotate = shot.transform.eulerAngles;
        rotate.z = -90.0f;
        shot.transform.eulerAngles = rotate;
        Rigidbody2D proc = shot.GetComponent<Rigidbody2D>();
        if (transform.eulerAngles.y == 180.0f)
        {
            proc.velocity = new Vector2(-50.0f, 0.0f);
        }
        else
        {
            proc.velocity = new Vector2(50.0f, 0.0f);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "WaterAttack")
        {
            health -= 2;
        }
        else if (collision.gameObject.tag == "ExplosionAttack")
        {
            health -= 10;
        }
        else if (collision.gameObject.name == "shot-1(Clone)")
        {
            audio.PlayOneShot(damageSound);
            health -= 5;
            Destroy(collision.gameObject);
        }
    }
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "laser")
        {
            health -= 1;
        }
    }
}
