using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPhase1 : MonoBehaviour
{
    public GameObject teleporter;
    public GameObject lightning;
    public GameObject hydro;
    public GameObject fireShield;
    public GameObject illusion;
    public GameObject illuKnife;

    public int health = 100;
    public bool fireable = true;
    private float countdown = 5.0f;
    private string[] arr = new string[5];
    private bool firstTime = true;
    private GameObject teleport;

    // Start is called before the first frame update
    void Start()
    {
        arr[0] = "Teleport";
        arr[1] = "Lightning";
        arr[2] = "Hydro";
        arr[3] = "FireShield";
        arr[4] = "Illusion";
    }

    // Update is called once per frame
    void Update()
    {
        if (fireable)
        {
            float random = Random.Range(0.0f, 5.0f);
            string pick = arr[(int)random];
            if (firstTime)
            {
                pick = "Teleport";
                firstTime = false;
            }
            if (pick == "Teleport")
            {
                Teleport();
            }
            else if (pick == "Lightning")
            {
                Lightning();
            }
            else if (pick == "Hydro")
            {
                Hydro();
            }
            else if (pick == "FireShield")
            {
                FireShield();
            }
            else if (pick == "Illusion")
            {
                Illusion();
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

    private void Teleport()
    {
        teleport = Instantiate(teleporter, transform.position, Quaternion.identity);
        StartCoroutine("teleportDelay", 1.0f);
        fireable = false;
    }
    
    private IEnumerator teleportDelay(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(teleport);
        Vector3 target2 = new Vector3(12.6f, -5f, 5f);
        teleport = Instantiate(teleporter, target2, Quaternion.identity);
        StartCoroutine("teleportDelay2", 1.0f);
    }

    private IEnumerator teleportDelay2(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(teleport);
    }

    private void Lightning()
    {
        fireable = false;
    }

    private void Hydro()
    {
        fireable = false;
    }

    private void FireShield()
    {
        fireable = false;
    }

    private void Illusion()
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
