using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager :Singleton<GameManager>
{
    [SerializeField]
    private Text info;
	public Text instructions;
    [SerializeField]
    private GameObject player;

    public List<GameObject> floor1 = new List<GameObject>();
    public List<GameObject> floor2 = new List<GameObject>();
    public List<GameObject> floor3 = new List<GameObject>();
    public List<GameObject> floor4 = new List<GameObject>();

    private void OnInit()
    {
        GameObject[] go1 = GameObject.FindGameObjectsWithTag("1");
        
        foreach(GameObject g1 in go1)
        {
            floor1.Add(g1);
        }

        GameObject[] go2 = GameObject.FindGameObjectsWithTag("2");

        foreach (GameObject g2 in go2)
        {
            floor2.Add(g2);
        }

        GameObject[] go3 = GameObject.FindGameObjectsWithTag("3");

        foreach (GameObject g3 in go3)
        {
            floor3.Add(g3);
        }

        GameObject[] go4 = GameObject.FindGameObjectsWithTag("4");

        foreach (GameObject g4 in go4)
        {
            floor4.Add(g4);
        }
    }
	private IEnumerator DoInstruction()
	{
		float pauseEndTime = Time.realtimeSinceStartup + 5;
		while (Time.realtimeSinceStartup < pauseEndTime)
		{
			yield return 0;
		}
		Time.timeScale = 1f;
		instructions.gameObject.SetActive(false);
	}

	// Start is called before the first frame update
	void Start()
    {
		instructions.gameObject.SetActive(false);
		//PlayerPrefs.SetInt("isFirstTime", 1);
		if (PlayerPrefs.GetInt("isFirstTime") == 1)
		{
			print("entered isfirst time");
			instructions.gameObject.SetActive(true);
			Time.timeScale = 0.0f;
			StartCoroutine("DoInstruction");
			PlayerPrefs.SetInt("isFirstTime", 0);
		}
		OnInit();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public IEnumerator DoGameOver()
    {
        Destroy(GameObject.Find("Rogue_01"));
        yield return new WaitForSeconds(0.5f);
        SceneManager.UnloadSceneAsync("FireDragon").completed += e =>
        {
            SceneManager.LoadScene("FireDragon", LoadSceneMode.Additive);
        };
    }

    public IEnumerator DoWin()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.UnloadSceneAsync("FireDragon");
        Scene mt = SceneManager.GetActiveScene();
        // Scene mt = SceneManager.GetSceneByName("Level1");
        foreach(GameObject obj in mt.GetRootGameObjects()) {
            if (obj.name != "PauseMenu")
            {
                obj.SetActive(true);
            }
        }
    }

    public void SetInfo(string ifo)
    {
        /*
        info.transform.parent.gameObject.SetActive(true);
        player.GetComponent<Player>().enabled = false;
        info.text = ifo;*/

        if(ifo.Equals("YOU WIN"))
        {
            StartCoroutine("DoWin");
        } else
        {
            StartCoroutine("DoGameOver");
        }
    }

    public void OnResetButton()
    {
        info.transform.parent.gameObject.SetActive(false);
        player.GetComponent<Player>().enabled = false;
        SceneManager.LoadScene(0);
    }
}
