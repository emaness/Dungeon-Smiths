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

    public GameObject fire;
    public GameObject rock;
    public GameObject pit;
    public GameObject water;
    public GameObject bomb;

    private string[,] board = new string[3, 5];
    private Transform[,] ratChildren = new Transform[3, 5];
    private Transform[,] buttonList = new Transform[3, 5];
    private bool gameIsSolo = false;

    // Start is called before the first frame update
    void Start()
    {
        board[0, 0] = "Fire";
        board[0, 1] = "Pit";
        board[0, 2] = "Rock";
        board[0, 3] = "Pit";
        board[0, 4] = "Fire";
        board[1, 0] = "Water";
        board[1, 1] = "Pit";
        board[1, 2] = "Bomb";
        board[1, 3] = "Pit";
        board[1, 4] = "Water";
        board[2, 0] = "Fire";
        board[2, 1] = "Pit";
        board[2, 2] = "Rock";
        board[2, 3] = "Pit";
        board[2, 4] = "Fire";

        int countLoaded = SceneManager.sceneCount;
        Scene[] loadedScenes = new Scene[countLoaded];

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
        if (board[x, y] == "Fire") //Removal in all in same column
        {
            trapFire(x, y);
        }
        else if (board[x, y] == "Rock") //Removal in all neighbors up/down/left/right
        {
            trapRock(x, y);
        }
        else if (board[x, y] == "Pit") //Single removal
        {
            trapPit(x, y);
        }
        else if (board[x, y] == "Water") //Removal in same row
        {
            trapWater(x, y);
        }
        else if (board[x, y] == "Bomb") //Removal of everything
        {
            trapBomb(x, y);
        }
    }

    private void trapFire(int x, int y)
    {
        numOfTraps -= 1;
        trapText.text = "Traps Remaining: " + numOfTraps;

        for (int i = 0; i < 3; i++)
        {
            if (board[i, y] != "Done")
            {
                board[i, y] = "Done";
                GameObject newFire = Instantiate(fire, ratChildren[i, y].position, Quaternion.identity);
                newFire.transform.localScale += new Vector3(100.0f, 100.0f, 100.0f);
                ratChildren[i, y].gameObject.SetActive(false);
                buttonList[i, y].gameObject.SetActive(false);
            }
        }
    }

    private void trapRock(int x, int y)
    {
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

    private void trapPit(int x, int y)
    {
        numOfTraps -= 1;
        trapText.text = "Traps Remaining: " + numOfTraps;
        if (board[x, y] != "Done")
        {
            board[x, y] = "Done";
            ratChildren[x, y].gameObject.SetActive(false);
            buttonList[x, y].gameObject.SetActive(false);
        }
    }

    private void trapWater(int x, int y)
    {
        numOfTraps -= 1;
        trapText.text = "Traps Remaining: " + numOfTraps;
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

    private void trapBomb(int x, int y)
    {
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
