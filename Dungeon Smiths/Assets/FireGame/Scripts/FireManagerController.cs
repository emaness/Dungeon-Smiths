using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FireManagerController : MonoBehaviour
{
    private const int MaxActiveFires = 30;
    private const float FireballInterval = 0.3f;

    public GameObject fire;
    public GameObject fireball;
    public GameObject hose;

    public Text YouWin;
    public Image FireBar;

    // represents the health of the overall fire column
    // when the user gets through all the fires they win
    private Queue<FireState> remainingFires;
    private int initialFireCount;
    private float fireballTimer;

    private void enqueueFires(int count, float health)
    {
        for (int i = 0; i != count; ++i)
        {
            FireState fs = new FireState(health);
            remainingFires.Enqueue(fs);
        }
    }

    private void Start()
    {
        YouWin.gameObject.SetActive(false);
        fireballTimer = FireballInterval;
        remainingFires = new Queue<FireState>();
        enqueueFires(200, 100);
        initialFireCount = remainingFires.Count;
        SpawnFires();
    }

    private void SpawnFires()
    {
        int numFires = findFires().Count;
        int numToCreate = Mathf.Min(MaxActiveFires - numFires, remainingFires.Count);
        for (int i = 0; i != numToCreate; ++i)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-3.25f, 3.25f), Random.Range(1.3f, 1.8f), 0.0f);
            GameObject newFire = Instantiate(fire, spawnPos, Quaternion.identity, gameObject.transform);
            FireState fs = remainingFires.Dequeue();
            newFire.GetComponent<FireController>().State = fs;
        }
    }

    private List<GameObject> findFires()
    {
        List<GameObject> fires = new List<GameObject>();
        int childCount = gameObject.transform.childCount;
        for (int i = 0; i != childCount; ++i)
        {
            GameObject child = gameObject.transform.GetChild(i).gameObject;
            if (child.CompareTag("Fire"))
            {
                fires.Add(child);
            }
        }
        return fires;
    }

    private IEnumerator DoGameOver()
    {
        yield return new WaitForSeconds(1.0f);

        SceneManager.UnloadSceneAsync("FireGame").completed += e =>
        {
            SceneManager.LoadScene("FireGame", LoadSceneMode.Additive);
        };
    }

    public void GameOver()
    {
        StartCoroutine("DoGameOver");
    }

    private IEnumerator DoWin()
    {
        yield return new WaitForSeconds(2.5f);

        SceneManager.UnloadSceneAsync("FireGame");

        Scene mt = SceneManager.GetSceneByName("Level1");
        foreach (GameObject obj in mt.GetRootGameObjects())
        {
            obj.SetActive(true);
        }
    }

    private void Update()
    {
        fireballTimer -= Time.deltaTime;
        if (fireballTimer < 0.0f)
        {
            if (Random.Range(0, 10) < 4)
            {
                var fires = findFires();
                if (fires.Count > 0)
                {
                    GameObject fromFire = fires[Random.Range(0, fires.Count)];
                    Vector3 p = fromFire.transform.position;
                    Vector3 r = new Vector3(-2.0f, Random.Range(-3.5f, 3.5f), 0.0f);
                    Vector3 hosePos = hose.transform.position;
                    Vector3 rv = p - r;
                    rv.Normalize();
                    Vector3 dir = hosePos - p;
                    Vector3 vel = dir * 0.8f + rv * 0.2f;
                    vel.Normalize();
                    vel *= 2.0f;

                    GameObject newFireball = Instantiate(fireball, p, Quaternion.identity);
                    newFireball.GetComponent<FireballController>().hose = hose;
                    var rb = newFireball.GetComponent<Rigidbody2D>();
                    rb.velocity = vel;

                    if (GetComponent<AudioSource>().isPlaying == false)
                    {
                        GetComponent<AudioSource>().volume = Random.Range(0.6f, .8f);
                        GetComponent<AudioSource>().pitch = Random.Range(0.9f, 1);
                        GetComponent<AudioSource>().Play();
                    }
                }
            }

            fireballTimer = FireballInterval;
        }

        int firesDestroyed = (initialFireCount - remainingFires.Count) - findFires().Count;
        FireBar.rectTransform.localScale = new Vector3(1.0f - firesDestroyed / (float)initialFireCount, 1.0f, 1.0f);
        if (firesDestroyed == initialFireCount)
        {
            YouWin.gameObject.SetActive(true);
            StartCoroutine("DoWin");
            hose.GetComponent<HoseController>().dead = true;
        }
        else
        {
            SpawnFires();
        }
    }
}