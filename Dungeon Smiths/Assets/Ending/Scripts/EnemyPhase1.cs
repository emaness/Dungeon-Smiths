using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPhase1 : MonoBehaviour
{
    public GameObject teleporter;
    public GameObject lightning;
    public GameObject hydro;
    public GameObject hydroWarn;
    public GameObject explosion;
    public GameObject explosionWarn;

    public GameObject player;
    public int health = 100;
    public bool fireable = true;
    private float countdown = 5.0f;
    private string[] arr = new string[5];
    private bool firstTime = true;
    private GameObject teleport;
    private GameObject electric;

    // Start is called before the first frame update
    void Start()
    {
        arr[0] = "Lightning";
        arr[1] = "Hydro";
        arr[2] = "Explosion";
        //Teleport happens every single interval
        //Basically- teleport + 1 random move of those 3
    }

    // Update is called once per frame
    void Update()
    {
        if (fireable)
        {
            float random = Random.Range(0.0f, 2.0f);
            string pick = arr[(int)random];
            Teleport();
            if (pick == "Lightning")
            {
                Lightning();
            }
            else if (pick == "Hydro")
            {
                Hydro();
            }
            else if (pick == "Explosion")
            {
                Explosion();
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
        //Disable all parts of character
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        StartCoroutine("teleportDelay", 1.0f);
    }
    
    private IEnumerator teleportDelay(float time)
    {
        yield return new WaitForSeconds(time);
        Vector3 currentPos = teleport.transform.position;
        Destroy(teleport);
        if (firstTime)
        {
            Vector3[] listOfVectors = new Vector3[3];
            listOfVectors[0] = new Vector3(9.0f, 5.0f, 0.0f); //right platform
            listOfVectors[1] = new Vector3(-2.0f, 0.0f, 0.0f); //mid platform
            listOfVectors[2] = new Vector3(-12.0f, 5.0f, 0.0f); //left platform
            float randInd = Random.Range(0.0f, 3.0f);
            Vector3 target2 = listOfVectors[(int)randInd];
            teleport = Instantiate(teleporter, target2, Quaternion.identity);
            firstTime = false;
        }
        else
        {
            Vector3[] listOfVectors = new Vector3[2];
            if (currentPos.x == 9.0f)
            {
                listOfVectors[0] = new Vector3(-2.0f, 0.0f, 0.0f);
                listOfVectors[1] = new Vector3(-12.0f, 5.0f, 0.0f);
            }
            else if (currentPos.x == -2.0f)
            {
                listOfVectors[0] = new Vector3(9.0f, 5.0f, 0.0f);
                listOfVectors[1] = new Vector3(-12.0f, 5.0f, 0.0f);
            }
            else if (currentPos.x == -12.0f)
            {
                listOfVectors[0] = new Vector3(9.0f, 5.0f, 0.0f);
                listOfVectors[1] = new Vector3(-2.0f, 0.0f, 0.0f);
            }
            float randInd = Random.Range(0.0f, 2.0f);
            Vector3 target2 = listOfVectors[(int)randInd];
            teleport = Instantiate(teleporter, target2, Quaternion.identity);
        }
        //If firstTime, pick between 3 areas, else, pick between 2 areas
        StartCoroutine("teleportDelay2", 1.0f);
    }

    private IEnumerator teleportDelay2(float time)
    {
        yield return new WaitForSeconds(time);
        //Enable all parts of character
        transform.position = teleport.transform.position;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
        Destroy(teleport);
    }

    private void Lightning()
    {
        electric = Instantiate(lightning, player.transform.position, Quaternion.identity);
        electric.transform.localScale += new Vector3(1.0f, 1.0f, 1.0f);
        fireable = false;
        StartCoroutine("electricDelay", 1.0f);
    }

    private IEnumerator electricDelay(float time)
    {
        yield return new WaitForSeconds(time);
        if (player.transform.position.x == electric.transform.position.x && player.transform.position.y == electric.transform.position.y)
        {
            player.GetComponent<PlatformerMovement2>().health -= 10;
        }
        electric = Instantiate(lightning, player.transform.position, Quaternion.identity);
        electric.transform.localScale += new Vector3(1.0f, 1.0f, 1.0f);
        StartCoroutine("electricDelay2", 1.0f);
    }

    private IEnumerator electricDelay2(float time)
    {
        yield return new WaitForSeconds(time);
        if (player.transform.position.x == electric.transform.position.x && player.transform.position.y == electric.transform.position.y)
        {
            player.GetComponent<PlatformerMovement2>().health -= 10;
        }
        electric = Instantiate(lightning, player.transform.position, Quaternion.identity);
        electric.transform.localScale += new Vector3(1.0f, 1.0f, 1.0f);
        StartCoroutine("electricDelay3", 1.0f);
    }

    private IEnumerator electricDelay3(float time)
    {
        yield return new WaitForSeconds(time);
        if (player.transform.position.x == electric.transform.position.x && player.transform.position.y == electric.transform.position.y)
        {
            player.GetComponent<PlatformerMovement2>().health -= 10;
        }
    }

    private void Hydro()
    {
        fireable = false;
    }

    private void Explosion()
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
