using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPhase2 : MonoBehaviour
{
    public int health = 200;
    public bool fireable = true;
    private float countdown = 5.0f;
    private string[] arr = new string[5];

    // Start is called before the first frame update
    void Start()
    {
        arr[0] = "Laser";
        arr[1] = "Virus";
        arr[2] = "Explosion";
        arr[3] = "LifeShield";
        arr[4] = "BulletWall";
    }

    // Update is called once per frame
    void Update()
    {
        if (fireable)
        {
            float random = Random.Range(0.0f, 5.0f);
            string pick = arr[(int)random];
            if (pick == "Laser")
            {
                Laser();
            }
            else if (pick == "Virus")
            {
                Virus();
            }
            else if (pick == "Explosion")
            {
                Explosion();
            }
            else if (pick == "LifeShield")
            {
                LifeShield();
            }
            else if (pick == "BulletWall")
            {
                BulletWall();
            }
        }
        else if (!(fireable) && (countdown > 0.0f))
        {
            countdown -= Time.deltaTime;
        }
        else if (countdown <= 0.0f)
        {
            countdown = 5.0f;
            fireable = true;
        }
    }

    private void Laser()
    {
        fireable = false;
    }

    private void Virus()
    {
        fireable = false;
    }

    private void Explosion()
    {
        fireable = false;
    }

    private void LifeShield()
    {
        fireable = false;
    }

    private void BulletWall()
    {
        fireable = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Rogue_weapon_001(Clone)")
        {
            health -= 10;
            Destroy(collision.gameObject);
        }
    }
}
