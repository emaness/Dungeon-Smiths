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
            float random = Random.Range(0.0f, 3.0f);
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
        StartCoroutine("electricDelay", 0.5f);
    }

    private IEnumerator electricDelay(float time)
    {
        yield return new WaitForSeconds(time);
        if (((player.transform.position.x <= electric.transform.position.x + 3) && (player.transform.position.x >= electric.transform.position.x - 3)) && ((player.transform.position.y <= electric.transform.position.y + 3) && (player.transform.position.y >= electric.transform.position.y - 3)))
        {
            player.GetComponent<PlatformerMovement2>().health -= 10;
        }
        yield return new WaitForSeconds(time);
        electric = Instantiate(lightning, player.transform.position, Quaternion.identity);
        electric.transform.localScale += new Vector3(1.0f, 1.0f, 1.0f);
        StartCoroutine("electricDelay2", 0.5f);
    }

    private IEnumerator electricDelay2(float time)
    {
        yield return new WaitForSeconds(time);
        if (((player.transform.position.x <= electric.transform.position.x + 3) && (player.transform.position.x >= electric.transform.position.x - 3)) && ((player.transform.position.y <= electric.transform.position.y + 3) && (player.transform.position.y >= electric.transform.position.y - 3)))
        {
            player.GetComponent<PlatformerMovement2>().health -= 10;
        }
        yield return new WaitForSeconds(time);
        electric = Instantiate(lightning, player.transform.position, Quaternion.identity);
        electric.transform.localScale += new Vector3(1.0f, 1.0f, 1.0f);
        StartCoroutine("electricDelay3", 0.5f);
    }

    private IEnumerator electricDelay3(float time)
    {
        yield return new WaitForSeconds(time);
        if (((player.transform.position.x <= electric.transform.position.x + 3) && (player.transform.position.x >= electric.transform.position.x - 3)) && ((player.transform.position.y <= electric.transform.position.y + 3) && (player.transform.position.y >= electric.transform.position.y - 3)))
        {
            player.GetComponent<PlatformerMovement2>().health -= 10;
        }
    }

    private void Hydro()
    {
        //Mid
        Vector3 target = new Vector3(-4.5f, -2.0f, 0.0f);
        GameObject waterWarn = Instantiate(hydroWarn, target, Quaternion.identity);
        waterWarn.transform.localScale += new Vector3(1.0f, 1.0f, 1.0f);
        Vector3 target2 = new Vector3(-3.5f, -2.0f, 0.0f);
        GameObject waterWarn2 = Instantiate(hydroWarn, target2, Quaternion.identity);
        waterWarn2.transform.localScale += new Vector3(1.0f, 1.0f, 1.0f);
        Vector3 target3 = new Vector3(-2.5f, -2.0f, 0.0f);
        GameObject waterWarn3 = Instantiate(hydroWarn, target3, Quaternion.identity);
        waterWarn3.transform.localScale += new Vector3(1.0f, 1.0f, 1.0f);
        Vector3 target4 = new Vector3(-1.5f, -2.0f, 0.0f);
        GameObject waterWarn4 = Instantiate(hydroWarn, target4, Quaternion.identity);
        waterWarn4.transform.localScale += new Vector3(1.0f, 1.0f, 1.0f);
        Vector3 target5 = new Vector3(-0.5f, -2.0f, 0.0f);
        GameObject waterWarn5 = Instantiate(hydroWarn, target5, Quaternion.identity);
        waterWarn5.transform.localScale += new Vector3(1.0f, 1.0f, 1.0f);
        Vector3 target6 = new Vector3(0.5f, -2.0f, 0.0f);
        GameObject waterWarn6 = Instantiate(hydroWarn, target6, Quaternion.identity);
        waterWarn6.transform.localScale += new Vector3(1.0f, 1.0f, 1.0f);

        //Left
        Vector3 target7 = new Vector3(-14.5f, 3.0f, 0.0f);
        GameObject waterWarn7 = Instantiate(hydroWarn, target7, Quaternion.identity);
        waterWarn7.transform.localScale += new Vector3(1.0f, 1.0f, 1.0f);
        Vector3 target8 = new Vector3(-13.5f, 3.0f, 0.0f);
        GameObject waterWarn8 = Instantiate(hydroWarn, target8, Quaternion.identity);
        waterWarn8.transform.localScale += new Vector3(1.0f, 1.0f, 1.0f);
        Vector3 target9 = new Vector3(-12.5f, 3.0f, 0.0f);
        GameObject waterWarn9 = Instantiate(hydroWarn, target9, Quaternion.identity);
        waterWarn9.transform.localScale += new Vector3(1.0f, 1.0f, 1.0f);
        Vector3 target10 = new Vector3(-11.5f, 3.0f, 0.0f);
        GameObject waterWarn10 = Instantiate(hydroWarn, target10, Quaternion.identity);
        waterWarn10.transform.localScale += new Vector3(1.0f, 1.0f, 1.0f);
        Vector3 target11 = new Vector3(-10.5f, 3.0f, 0.0f);
        GameObject waterWarn11 = Instantiate(hydroWarn, target11, Quaternion.identity);
        waterWarn11.transform.localScale += new Vector3(1.0f, 1.0f, 1.0f);
        Vector3 target12 = new Vector3(-9.5f, 3.0f, 0.0f);
        GameObject waterWarn12 = Instantiate(hydroWarn, target12, Quaternion.identity);
        waterWarn12.transform.localScale += new Vector3(1.0f, 1.0f, 1.0f);

        //Right
        Vector3 target13 = new Vector3(6.5f, 3.0f, 0.0f);
        GameObject waterWarn13 = Instantiate(hydroWarn, target13, Quaternion.identity);
        waterWarn13.transform.localScale += new Vector3(1.0f, 1.0f, 1.0f);
        Vector3 target14 = new Vector3(7.5f, 3.0f, 0.0f);
        GameObject waterWarn14 = Instantiate(hydroWarn, target14, Quaternion.identity);
        waterWarn14.transform.localScale += new Vector3(1.0f, 1.0f, 1.0f);
        Vector3 target15 = new Vector3(8.5f, 3.0f, 0.0f);
        GameObject waterWarn15 = Instantiate(hydroWarn, target15, Quaternion.identity);
        waterWarn15.transform.localScale += new Vector3(1.0f, 1.0f, 1.0f);
        Vector3 target16 = new Vector3(9.5f, 3.0f, 0.0f);
        GameObject waterWarn16 = Instantiate(hydroWarn, target16, Quaternion.identity);
        waterWarn16.transform.localScale += new Vector3(1.0f, 1.0f, 1.0f);
        Vector3 target17 = new Vector3(10.5f, 3.0f, 0.0f);
        GameObject waterWarn17 = Instantiate(hydroWarn, target17, Quaternion.identity);
        waterWarn17.transform.localScale += new Vector3(1.0f, 1.0f, 1.0f);
        Vector3 target18 = new Vector3(11.5f, 3.0f, 0.0f);
        GameObject waterWarn18 = Instantiate(hydroWarn, target18, Quaternion.identity);
        waterWarn18.transform.localScale += new Vector3(1.0f, 1.0f, 1.0f);

        fireable = false;
        StartCoroutine("HydroDelay", 1.0f);
    }

    private IEnumerator HydroDelay(float time)
    {
        yield return new WaitForSeconds(time);

        //Mid
        Vector3 target2 = new Vector3(-3.5f, -2.0f, 0.0f);
        GameObject waterWarn2 = Instantiate(hydro, target2, Quaternion.identity);
        Vector3 target3 = new Vector3(-2.5f, -2.0f, 0.0f);
        GameObject waterWarn3 = Instantiate(hydro, target3, Quaternion.identity);
        Vector3 target4 = new Vector3(-1.5f, -2.0f, 0.0f);
        GameObject waterWarn4 = Instantiate(hydro, target4, Quaternion.identity);
        Vector3 target5 = new Vector3(-0.5f, -2.0f, 0.0f);
        GameObject waterWarn5 = Instantiate(hydro, target5, Quaternion.identity);
        Vector3 target6 = new Vector3(0.5f, -2.0f, 0.0f);
        GameObject waterWarn6 = Instantiate(hydro, target6, Quaternion.identity);

        //Left
        Vector3 target8 = new Vector3(-13.5f, 3.0f, 0.0f);
        GameObject waterWarn8 = Instantiate(hydro, target8, Quaternion.identity);
        Vector3 target9 = new Vector3(-12.5f, 3.0f, 0.0f);
        GameObject waterWarn9 = Instantiate(hydro, target9, Quaternion.identity);
        Vector3 target10 = new Vector3(-11.5f, 3.0f, 0.0f);
        GameObject waterWarn10 = Instantiate(hydro, target10, Quaternion.identity);
        Vector3 target11 = new Vector3(-10.5f, 3.0f, 0.0f);
        GameObject waterWarn11 = Instantiate(hydro, target11, Quaternion.identity);
        Vector3 target12 = new Vector3(-9.5f, 3.0f, 0.0f);
        GameObject waterWarn12 = Instantiate(hydro, target12, Quaternion.identity);

        //Right
        Vector3 target14 = new Vector3(7.5f, 3.0f, 0.0f);
        GameObject waterWarn14 = Instantiate(hydro, target14, Quaternion.identity);
        Vector3 target15 = new Vector3(8.5f, 3.0f, 0.0f);
        GameObject waterWarn15 = Instantiate(hydro, target15, Quaternion.identity);
        Vector3 target16 = new Vector3(9.5f, 3.0f, 0.0f);
        GameObject waterWarn16 = Instantiate(hydro, target16, Quaternion.identity);
        Vector3 target17 = new Vector3(10.5f, 3.0f, 0.0f);
        GameObject waterWarn17 = Instantiate(hydro, target17, Quaternion.identity);
        Vector3 target18 = new Vector3(11.5f, 3.0f, 0.0f);
        GameObject waterWarn18 = Instantiate(hydro, target18, Quaternion.identity);
        
        yield return new WaitForSeconds(0.5f);
        waterWarn2.GetComponent<BoxCollider2D>().enabled = false;
        waterWarn3.GetComponent<BoxCollider2D>().enabled = false;
        waterWarn4.GetComponent<BoxCollider2D>().enabled = false;
        waterWarn5.GetComponent<BoxCollider2D>().enabled = false;
        waterWarn6.GetComponent<BoxCollider2D>().enabled = false;
        waterWarn8.GetComponent<BoxCollider2D>().enabled = false;
        waterWarn9.GetComponent<BoxCollider2D>().enabled = false;
        waterWarn10.GetComponent<BoxCollider2D>().enabled = false;
        waterWarn11.GetComponent<BoxCollider2D>().enabled = false;
        waterWarn12.GetComponent<BoxCollider2D>().enabled = false;
        waterWarn14.GetComponent<BoxCollider2D>().enabled = false;
        waterWarn15.GetComponent<BoxCollider2D>().enabled = false;
        waterWarn16.GetComponent<BoxCollider2D>().enabled = false;
        waterWarn17.GetComponent<BoxCollider2D>().enabled = false;
        waterWarn18.GetComponent<BoxCollider2D>().enabled = false;
    }

    private void Explosion()
    {
        Vector3 target = new Vector3(0.0f, -5.0f, 0.0f);
        GameObject explodeWarn = Instantiate(explosionWarn, target, Quaternion.identity);
        explodeWarn.transform.localScale += new Vector3(1.0f, 1.0f, 1.0f);
        fireable = false;
        StartCoroutine("ExplosionDelay", 1.0f);
    }
    
    private IEnumerator ExplosionDelay(float time)
    {
        yield return new WaitForSeconds(time);
        Vector3 target = new Vector3(-15.0f, -7.0f, 0.0f);
        GameObject explode = Instantiate(explosion, target, Quaternion.identity);
        explode.transform.localScale += new Vector3(1.0f, 1.0f, 1.0f);
        Vector3 target1 = new Vector3(-13.0f, -7.0f, 0.0f);
        GameObject explode1 = Instantiate(explosion, target1, Quaternion.identity);
        explode1.transform.localScale += new Vector3(1.0f, 1.0f, 1.0f);
        Vector3 target2 = new Vector3(-11.0f, -7.0f, 0.0f);
        GameObject explode2 = Instantiate(explosion, target2, Quaternion.identity);
        explode2.transform.localScale += new Vector3(1.0f, 1.0f, 1.0f);
        Vector3 target3 = new Vector3(-9.0f, -7.0f, 0.0f);
        GameObject explode3 = Instantiate(explosion, target3, Quaternion.identity);
        explode3.transform.localScale += new Vector3(1.0f, 1.0f, 1.0f);
        Vector3 target4 = new Vector3(-7.0f, -7.0f, 0.0f);
        GameObject explode4 = Instantiate(explosion, target4, Quaternion.identity);
        explode4.transform.localScale += new Vector3(1.0f, 1.0f, 1.0f);
        Vector3 target5 = new Vector3(-5.0f, -7.0f, 0.0f);
        GameObject explode5 = Instantiate(explosion, target5, Quaternion.identity);
        explode5.transform.localScale += new Vector3(1.0f, 1.0f, 1.0f);
        Vector3 target6 = new Vector3(-3.0f, -7.0f, 0.0f);
        GameObject explode6 = Instantiate(explosion, target6, Quaternion.identity);
        explode6.transform.localScale += new Vector3(1.0f, 1.0f, 1.0f);
        Vector3 target7 = new Vector3(-1.0f, -7.0f, 0.0f);
        GameObject explode7 = Instantiate(explosion, target7, Quaternion.identity);
        explode7.transform.localScale += new Vector3(1.0f, 1.0f, 1.0f);
        Vector3 target8 = new Vector3(1.0f, -7.0f, 0.0f);
        GameObject explode8 = Instantiate(explosion, target8, Quaternion.identity);
        explode8.transform.localScale += new Vector3(1.0f, 1.0f, 1.0f);
        Vector3 target9 = new Vector3(3.0f, -7.0f, 0.0f);
        GameObject explode9 = Instantiate(explosion, target9, Quaternion.identity);
        explode9.transform.localScale += new Vector3(1.0f, 1.0f, 1.0f);
        Vector3 target10 = new Vector3(5.0f, -7.0f, 0.0f);
        GameObject explode10 = Instantiate(explosion, target10, Quaternion.identity);
        explode10.transform.localScale += new Vector3(1.0f, 1.0f, 1.0f);
        Vector3 target11 = new Vector3(7.0f, -7.0f, 0.0f);
        GameObject explode11 = Instantiate(explosion, target11, Quaternion.identity);
        explode11.transform.localScale += new Vector3(1.0f, 1.0f, 1.0f);
        Vector3 target12 = new Vector3(9.0f, -7.0f, 0.0f);
        GameObject explode12 = Instantiate(explosion, target12, Quaternion.identity);
        explode12.transform.localScale += new Vector3(1.0f, 1.0f, 1.0f);
        Vector3 target13 = new Vector3(11.0f, -7.0f, 0.0f);
        GameObject explode13 = Instantiate(explosion, target13, Quaternion.identity);
        explode13.transform.localScale += new Vector3(1.0f, 1.0f, 1.0f);
        Vector3 target14 = new Vector3(13.0f, -7.0f, 0.0f);
        GameObject explode14 = Instantiate(explosion, target14, Quaternion.identity);
        explode14.transform.localScale += new Vector3(1.0f, 1.0f, 1.0f);
        Vector3 target15 = new Vector3(15.0f, -7.0f, 0.0f);
        GameObject explode15 = Instantiate(explosion, target15, Quaternion.identity);
        explode15.transform.localScale += new Vector3(1.0f, 1.0f, 1.0f);

        yield return new WaitForSeconds(0.5f);
        explode.GetComponent<BoxCollider2D>().enabled = false;
        explode1.GetComponent<BoxCollider2D>().enabled = false;
        explode2.GetComponent<BoxCollider2D>().enabled = false;
        explode3.GetComponent<BoxCollider2D>().enabled = false;
        explode4.GetComponent<BoxCollider2D>().enabled = false;
        explode5.GetComponent<BoxCollider2D>().enabled = false;
        explode6.GetComponent<BoxCollider2D>().enabled = false;
        explode7.GetComponent<BoxCollider2D>().enabled = false;
        explode8.GetComponent<BoxCollider2D>().enabled = false;
        explode9.GetComponent<BoxCollider2D>().enabled = false;
        explode10.GetComponent<BoxCollider2D>().enabled = false;
        explode11.GetComponent<BoxCollider2D>().enabled = false;
        explode12.GetComponent<BoxCollider2D>().enabled = false;
        explode13.GetComponent<BoxCollider2D>().enabled = false;
        explode14.GetComponent<BoxCollider2D>().enabled = false;
        explode15.GetComponent<BoxCollider2D>().enabled = false;
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
