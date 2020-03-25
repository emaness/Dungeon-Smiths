﻿using System.Collections;
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

    // Start is called before the first frame update
    void Start()
    {
        board[0, 0] = "trap1";
        board[0, 1] = "trap3";
        board[0, 2] = "trap2";
        board[0, 3] = "trap3";
        board[0, 4] = "trap1";
        board[1, 0] = "trap4";
        board[1, 1] = "trap3";
        board[1, 2] = "trap5";
        board[1, 3] = "trap3";
        board[1, 4] = "trap4";
        board[2, 0] = "trap1";
        board[2, 1] = "trap3";
        board[2, 2] = "trap2";
        board[2, 3] = "trap3";
        board[2, 4] = "trap1";

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
            trap1(x, y);
        }
        else if (board[x, y] == "trap2") //Removal in all neighbors up/down/left/right
        {
            trap2(x, y);
        }
        else if (board[x, y] == "trap3") //Single removal
        {
            trap3(x, y);
        }
        else if (board[x, y] == "trap4") //Removal in same row
        {
            trap4(x, y);
        }
        else if (board[x, y] == "trap5") //Removal of everything
        {
            trap5(x, y);
        }
    }

    private void trap1(int x, int y)
    {
        trapText.text = "Traps Remaining: " + numOfTraps;

        for (int i = 0; i < 3; i++)
        {
            if (board[i, y] != "Done")
            {
                GameObject newTrap = Instantiate(trap1obj, coordinatesList[i, y], Quaternion.identity);
                newTrap.transform.localScale += new Vector3(25.0f, 25.0f, 25.0f);

                board[i, y] = "Done";
                ratChildren[i, y].gameObject.SetActive(false);
                buttonList[i, y].gameObject.SetActive(false);
            }
        }
        
        numOfTraps -= 1;
    }

    private void trap2(int x, int y)
    {
        trapText.text = "Traps Remaining: " + numOfTraps;

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (Mathf.Abs(x - i) + Mathf.Abs(y - j) <= 1)
                {
                    if (board[i, j] != "Done")
                    {
                        GameObject newTrap = Instantiate(trap2obj, coordinatesList[i, j], Quaternion.identity);
                        newTrap.transform.localScale += new Vector3(10.0f, 10.0f, 10.0f);

                        board[i, j] = "Done";
                        ratChildren[i, j].gameObject.SetActive(false);
                        buttonList[i, j].gameObject.SetActive(false);
                    }
                }
            }
        }
        
        numOfTraps -= 1;
    }

    private void trap3(int x, int y)
    {
        trapText.text = "Traps Remaining: " + numOfTraps;

        if (board[x, y] != "Done")
        {
            GameObject newTrap = Instantiate(trap3obj, coordinatesList[x, y], Quaternion.identity);
            newTrap.transform.localScale += new Vector3(10.0f, 10.0f, 10.0f);

            board[x, y] = "Done";
            ratChildren[x, y].gameObject.SetActive(false);
            buttonList[x, y].gameObject.SetActive(false);
        }
        
        numOfTraps -= 1;
    }

    private void trap4(int x, int y)
    {
        trapText.text = "Traps Remaining: " + numOfTraps;

        for (int i = 0; i < 5; i++)
        {
            if (board[x, i] != "Done")
            {
                GameObject newTrap = Instantiate(trap4obj, coordinatesList[x, i], Quaternion.identity);
                newTrap.transform.localScale += new Vector3(10.0f, 10.0f, 10.0f);

                board[x, i] = "Done";
                ratChildren[x, i].gameObject.SetActive(false);
                buttonList[x, i].gameObject.SetActive(false);
            }
        }
        
        numOfTraps -= 1;
    }

    private void trap5(int x, int y)
    {
        trapText.text = "Traps Remaining: " + numOfTraps;

        GameObject newTrap = Instantiate(trap5obj, coordinatesList[x, y], Quaternion.identity);
        newTrap.transform.localScale += new Vector3(10.0f, 10.0f, 10.0f);

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
        
        numOfTraps -= 1;
    }
}
