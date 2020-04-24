using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPhase2 : MonoBehaviour
{
    public GameObject bullet;
    public GameObject laser;
    public GameObject laserWarn;
    public GameObject laser1;
    public GameObject laserWarn1;
    public GameObject laser2;
    public GameObject laserWarn2;
    public GameObject lifeShield;

    public GameObject player;
    public int health = 200;
    public bool fireable1 = true;
    public bool fireable2 = true;
    public bool lifeShieldTriggered = false;
    public bool lifeShieldActive = false;
    private float countdown1 = 2.0f;
    private float countdown2 = 10.0f;
    private float countdown3 = 5.0f;
    private GameObject shield;

    // Start is called before the first frame update
    void Start()
    {
        //Phase 2 shoots waves of bullets every 3 seconds on top, mid, or bot
        //Also laser every 10 seconds on top, mid, or bot
        //Also dropped to 100 hp the boss will absorb damage and gain life instead
    }

    // Update is called once per frame
    void Update()
    {
        if (fireable1)
        {
            BulletWall();
        }
        else if (!(fireable1) && (countdown1 > 0.0f))
        {
            countdown1 -= Time.deltaTime;
        }
        else if (countdown1 <= 0.0f)
        {
            countdown1 = 2.0f;
            fireable1 = true;
        }
        if (fireable2)
        {
            Laser();
        }
        else if (!(fireable2) && (countdown2 > 0.0f))
        {
            countdown2 -= Time.deltaTime;
        }
        else if (countdown2 <= 0.0f)
        {
            countdown2 = 10.0f;
            fireable2 = true;
        }
        if ((!lifeShieldTriggered) && health <= 100)
        {
            LifeShield();
        }
        if ((lifeShieldActive) && (countdown3 > 0.0f))
        {
            countdown3 -= Time.deltaTime;
        }
        else if (countdown3 <= 0.0f)
        {
            lifeShieldActive = false;
            countdown3 = 100.0f;
            Destroy(shield);
            gameObject.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    private void BulletWall()
    {
        string[] arr = new string[3];
        arr[0] = "top";
        arr[1] = "mid";
        arr[2] = "bot";
        float random = Random.Range(0.0f, 3.0f);
        string pick = arr[(int)random];
        if (pick == "top")
        {
            for (int i = 0; i < 5; ++i)
            {
                for (int j = 0; j < 3; ++j)
                {
                    Vector3 target = new Vector3(transform.position.x - i, 6 - j, transform.position.z);
                    GameObject shot = Instantiate(bullet, target, Quaternion.identity);
                    Rigidbody2D proc = shot.GetComponent<Rigidbody2D>();
                    proc.velocity = new Vector2(-5.0f, 0.0f);
                }
            }
        }
        else if (pick == "mid")
        {
            for (int i = 0; i < 5; ++i)
            {
                for (int j = 0; j < 3; ++j)
                {
                    Vector3 target = new Vector3(transform.position.x - i, 1 - j, transform.position.z);
                    GameObject shot = Instantiate(bullet, target, Quaternion.identity);
                    Rigidbody2D proc = shot.GetComponent<Rigidbody2D>();
                    proc.velocity = new Vector2(-5.0f, 0.0f);
                }
            }
        }
        else if (pick == "bot")
        {
            for (int i = 0; i < 5; ++i)
            {
                for (int j = 0; j < 3; ++j)
                {
                    Vector3 target = new Vector3(transform.position.x - i, -4 - j, transform.position.z);
                    GameObject shot = Instantiate(bullet, target, Quaternion.identity);
                    Rigidbody2D proc = shot.GetComponent<Rigidbody2D>();
                    proc.velocity = new Vector2(-5.0f, 0.0f);
                }
            }
        }
        fireable1 = false;
    }

    private void Laser()
    {
        int[] arr = new int[3];
        arr[0] = 0;
        arr[1] = 1;
        arr[2] = 2;
        float random = Random.Range(0.0f, 3.0f);
        int pick = arr[(int)random];
        if (pick == 0)
        {
            laserWarn.SetActive(true);
            StartCoroutine("laserOne", 1.0f);
        }
        if (pick == 1)
        {
            laserWarn1.SetActive(true);
            StartCoroutine("laserTwo", 1.0f);
        }
        if (pick == 2)
        {
            laserWarn2.SetActive(true);
            StartCoroutine("laserThree", 1.0f);
        }
        fireable2 = false;
    }
    
    private IEnumerator laserOne(float time)
    {
        yield return new WaitForSeconds(time);
        laserWarn.SetActive(false);
        laser.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        laser.SetActive(false);
    }
    
    private IEnumerator laserTwo(float time)
    {
        yield return new WaitForSeconds(time);
        laserWarn1.SetActive(false);
        laser1.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        laser1.SetActive(false);
    }
    
    private IEnumerator laserThree(float time)
    {
        yield return new WaitForSeconds(time);
        laserWarn2.SetActive(false);
        laser2.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        laser2.SetActive(false);
    }

    private void LifeShield()
    {
        shield = Instantiate(lifeShield, transform.position, Quaternion.identity);
        shield.transform.localScale += new Vector3(3.0f, 1.0f, 1.0f);
        Vector3 tempRotate = shield.transform.eulerAngles;
        tempRotate.x = -90.0f;
        shield.transform.eulerAngles = tempRotate;
        gameObject.transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        lifeShieldTriggered = true;
        lifeShieldActive = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Rogue_weapon_001(Clone)" && !lifeShieldActive)
        {
            health -= 10;
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.name == "Rogue_weapon_001(Clone)" && lifeShieldActive)
        {
            health += 10;
            Destroy(collision.gameObject);
        }
    }
}
