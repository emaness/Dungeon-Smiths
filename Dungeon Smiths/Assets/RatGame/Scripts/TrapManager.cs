using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TrapManager : MonoBehaviour
{
    public GameObject ratsList;
    public GameObject canvas;
    public Text trapText;
    public int numOfTraps = 5;
    public Vector2[] coordinates = new Vector2[15];

    public AudioSource audio;
    public AudioClip fireStartSound;
    public AudioClip sparksSound;
	public AudioClip bombSound;
	public AudioClip gasSound;
	public AudioClip windSound;

    public GameObject trap1obj; //fire
    public GameObject trap2obj; //rock
    public GameObject trap3obj; //pit
    public GameObject trap4obj; //water
    public GameObject trap5obj; //bomb

    private string[,] board = new string[3, 5];
    private Transform[,] ratChildren = new Transform[3, 5];
    private Transform[,] buttonList = new Transform[3, 5];
    private Vector3[,] coordinatesList = new Vector3[3, 5];
    private bool gameIsSolo = false;

    private string[] randomization = new string[5] { "trap1", "trap2", "trap3", "trap4", "trap5" };
    private int[] randomizationCount = new int[5] { 0, 0, 0, 0, 0 }; //4, 2, 6, 2, 1
    private bool unaccepted = true;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                int index = 0;
                while (unaccepted)
                {
                    index = Random.Range(0, 5);
                    if (randomizationCount[index] == 4 && index == 0)
                    {
                        unaccepted = true;
                    }
                    else if (randomizationCount[index] == 2 && index == 1)
                    {
                        unaccepted = true;
                    }
                    else if (randomizationCount[index] == 6 && index == 2)
                    {
                        unaccepted = true;
                    }
                    else if (randomizationCount[index] == 2 && index == 3)
                    {
                        unaccepted = true;
                    }
                    else if (randomizationCount[index] == 1 && index == 4)
                    {
                        unaccepted = true;
                    }
                    else
                    {
                        unaccepted = false;
                    }
                }
                board[i, j] = randomization[index];
                randomizationCount[index]++;
                unaccepted = true;
            }
        }

        int countLoaded = SceneManager.sceneCount;
        Scene[] loadedScenes = new Scene[countLoaded];

        audio = GetComponent<AudioSource>();

        for (int i = 0; i < countLoaded; i++)
        {
            loadedScenes[i] = SceneManager.GetSceneAt(i);
        }
        if (loadedScenes[0].name == "RatGame")
        {
            gameIsSolo = true;
        }

        foreach (Transform child in ratsList.transform)
        {
            string sectionName = child.name;
            int x = (int)char.GetNumericValue(sectionName[9]);
            int y = (int)char.GetNumericValue(sectionName[11]);
            ratChildren[x, y] = child;
        }

        foreach (Transform button in canvas.transform)
        {
            string buttonName = button.name;
            int x = (int)char.GetNumericValue(buttonName[8]);
            int y = (int)char.GetNumericValue(buttonName[10]);
            buttonList[x, y] = button;
        }

        int k = 0;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                Vector2 coord = coordinates[k];
                coordinatesList[i, j] = new Vector3(coord.x, coord.y, 1.4f);
                k++;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        winLose();
    }

    private void winLose()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if ((board[i, j] != "Done") && (numOfTraps == 0))
                {
                    //Lose- restart
                    if (!gameIsSolo)
                    {
                        SceneManager.UnloadSceneAsync("RatGame").completed += e =>
                        {
                            SceneManager.LoadScene("RatGame", LoadSceneMode.Additive);
                        };
                    }
                    else
                    {
                        SceneManager.LoadScene("RatGame");
                    }
                }
            }
        }
        //Win- go back to level 2
        Scene mt = SceneManager.GetActiveScene();
        // Scene mt = SceneManager.GetSceneByName("Level1");
        foreach (GameObject obj in mt.GetRootGameObjects())
        {
            obj.SetActive(true);
        }
    }

    public void clickedTrap()
    {
        string buttonName = EventSystem.current.currentSelectedGameObject.name;
        int x = (int)char.GetNumericValue(buttonName[8]);
        int y = (int)char.GetNumericValue(buttonName[10]);
        if (board[x, y] == "trap1") //Removal in all in same column
        {
            StartCoroutine(trap1(x, y));
        }
        else if (board[x, y] == "trap2") //Removal in all neighbors up/down/left/right
        {
            StartCoroutine(trap2(x, y));
        }
        else if (board[x, y] == "trap3") //Single removal
        {
            StartCoroutine(trap3(x, y));
        }
        else if (board[x, y] == "trap4") //Removal in same row
        {
            StartCoroutine(trap4(x, y));
        }
        else if (board[x, y] == "trap5") //Removal of everything
        {
            StartCoroutine(trap5(x, y));
        }
    }

    private IEnumerator trap1(int x, int y)
    {
        for (int i = 0; i < 3; i++)
        {
            if (board[i, y] != "Done")
            {
                audio.PlayOneShot(fireStartSound);
            
                GameObject newTrap = Instantiate(trap1obj, coordinatesList[i, y], Quaternion.identity);
                newTrap.transform.localScale += new Vector3(25.0f, 25.0f, 25.0f);
            }
        }

        yield return new WaitForSeconds(1.0f);

        numOfTraps -= 1;
        trapText.text = "Traps Remaining: " + numOfTraps;

        for (int i = 0; i < 3; i++)
        {
            if (board[i, y] != "Done")
            {
                board[i, y] = "Done";
                ratChildren[i, y].gameObject.SetActive(false);
                buttonList[i, y].gameObject.SetActive(false);
            }
        }
    }

    private IEnumerator trap2(int x, int y)
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (Mathf.Abs(x - i) + Mathf.Abs(y - j) <= 1)
                {
                    if (board[i, j] != "Done")
                    {
                        audio.PlayOneShot(sparksSound);
                        GameObject newTrap = Instantiate(trap2obj, coordinatesList[i, j], Quaternion.identity);
                        newTrap.transform.localScale += new Vector3(10.0f, 10.0f, 10.0f);
                    }
                }
            }
        }
        
        yield return new WaitForSeconds(0.25f);

        numOfTraps -= 1;
        trapText.text = "Traps Remaining: " + numOfTraps;

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (Mathf.Abs(x - i) + Mathf.Abs(y - j) <= 1)
                {
                    if (board[i, j] != "Done")
                    {
                        board[i, j] = "Done";
                        ratChildren[i, j].gameObject.SetActive(false);
                        buttonList[i, j].gameObject.SetActive(false);
                    }
                }
            }
        }
    }

    private IEnumerator trap3(int x, int y)
    {
        if (board[x, y] != "Done")
        {
            audio.PlayOneShot(windSound);
            GameObject newTrap = Instantiate(trap3obj, coordinatesList[x, y], Quaternion.identity);
            newTrap.transform.localScale += new Vector3(10.0f, 10.0f, 10.0f);
        }

        yield return new WaitForSeconds(1.0f);

        numOfTraps -= 1;
        trapText.text = "Traps Remaining: " + numOfTraps;
        audio.Stop();

        if (board[x, y] != "Done")
        {
            board[x, y] = "Done";
            ratChildren[x, y].gameObject.SetActive(false);
            buttonList[x, y].gameObject.SetActive(false);
        }
    }

    private IEnumerator trap4(int x, int y)
    {
        for (int i = 0; i < 5; i++)
        {
            if (board[x, i] != "Done")
            {
                audio.PlayOneShot(gasSound);
                GameObject newTrap = Instantiate(trap4obj, coordinatesList[x, i], Quaternion.identity);
                newTrap.transform.localScale += new Vector3(25.0f, 25.0f, 25.0f);
            }
        }

        yield return new WaitForSeconds(2.0f);

        numOfTraps -= 1;
        trapText.text = "Traps Remaining: " + numOfTraps;
        audio.Stop();

        for (int i = 0; i < 5; i++)
        {
            if (board[x, i] != "Done")
            {
                board[x, i] = "Done";
                ratChildren[x, i].gameObject.SetActive(false);
                buttonList[x, i].gameObject.SetActive(false);
            }
        }
    }

    private IEnumerator trap5(int x, int y)
    {
        audio.PlayOneShot(bombSound);
        GameObject newTrap = Instantiate(trap5obj, coordinatesList[1, 2], Quaternion.identity);
        newTrap.transform.localScale += new Vector3(20.0f, 20.0f, 20.0f);

        yield return new WaitForSeconds(0.25f);

        numOfTraps -= 1;
        trapText.text = "Traps Remaining: " + numOfTraps;

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (board[i, j] != "Done")
                {
                    board[i, j] = "Done";
                    ratChildren[i, j].gameObject.SetActive(false);
                    buttonList[i, j].gameObject.SetActive(false);
                }
            }
        }
    }
}
