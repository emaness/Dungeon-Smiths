using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float totalSeconds = 600.0f;
    public GameObject timeAddUI;
    private float timeInScene = 0.0f;
    private Text timeText;
    private Text timeAddText;
    private int countLoaded = 1;
    private string[] loadedScenes; //This is a 2 sized array containing Level 1/2/3 and a loaded minigame (or no minigame).

    // Start is called before the first frame update
    void Start()
    {
        timeText = GetComponent<Text>();
        timeAddText = timeAddUI.GetComponent<Text>();
        loadedScenes = new string[2];
        loadedScenes[0] = SceneManager.GetActiveScene().name;
        loadedScenes[1] = "None";
    }

    // Update is called once per frame
    void Update()
    {
        totalSeconds -= Time.deltaTime;
        if (totalSeconds <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
        int minutes = Mathf.FloorToInt(totalSeconds / 60.0F);
        int seconds = Mathf.FloorToInt(totalSeconds - minutes * 60);
        string formatted = string.Format("{0:00}:{1:00}", minutes, seconds);
        timeText.text = formatted;

        countLoaded = SceneManager.sceneCount; //Check if minigame open
        if (countLoaded == 2 && loadedScenes[1] == "None")
        {
            //Minigame or pause menu is opened
            loadedScenes[1] = SceneManager.GetSceneAt(1).name;
        }
        if (countLoaded == 2)
        {
            //Time the specific minigame
            timeInScene = Time.timeSinceLevelLoad;
        }
        if (countLoaded == 1 && loadedScenes[1] != "None")
        {
            //Can set specific added amounts of time here depending
            //on both the minigame type and the amount of time taken in that game
            if (loadedScenes[1] == "FireGame")
            {
                if (timeInScene <= 20.0f)
                {
                    totalSeconds += 15.0f;
                    timeAddChange("+15 sec");
                }
                else if (timeInScene <= 35.0f)
                {
                    totalSeconds += 10.0f;
                    timeAddChange("+10 sec");
                }
                else
                {
                    totalSeconds += 5.0f;
                    timeAddChange("+5 sec");
                }
            }
            else if (loadedScenes[1] == "TileMatchingGame")
            {
                if (timeInScene <= 30.0f)
                {
                    totalSeconds += 15.0f;
                    timeAddChange("+15 sec");
                }
                else if (timeInScene <= 60.0f)
                {
                    totalSeconds += 10.0f;
                    timeAddChange("+10 sec");
                }
                else
                {
                    totalSeconds += 5.0f;
                    timeAddChange("+5 sec");
                }
            }
            else if (loadedScenes[1] == "Minecart Game")
            {
                if (timeInScene <= 30.0f)
                {
                    totalSeconds += 10.0f;
                    timeAddChange("+10 sec");
                }
                else
                {
                    totalSeconds += 5.0f;
                    timeAddChange("+5 sec");
                }
            }
            else if (loadedScenes[1] == "RockThrowing")
            {
                if (timeInScene <= 15.0f)
                {
                    totalSeconds += 10.0f;
                    timeAddChange("+10 sec");
                }
                else
                {
                    totalSeconds += 5.0f;
                    timeAddChange("+5 sec");
                }
            }
            else if (loadedScenes[1] == "spiderScene")
            {
                if (timeInScene <= 20.0f)
                {
                    totalSeconds += 10.0f;
                    timeAddChange("+10 sec");
                }
                else
                {
                    totalSeconds += 5.0f;
                    timeAddChange("+5 sec");
                }
            }
            else if (loadedScenes[1] == "FireDragon")
            {
                if (timeInScene <= 40.0f)
                {
                    totalSeconds += 10.0f;
                    timeAddChange("+10 sec");
                }
                else
                {
                    totalSeconds += 5.0f;
                    timeAddChange("+5 sec");
                }
            }
            else if (loadedScenes[1] == "KnightScene")
            {
                if (timeInScene <= 15.0f)
                {
                    totalSeconds += 10.0f;
                    timeAddChange("+10 sec");
                }
                else
                {
                    totalSeconds += 5.0f;
                    timeAddChange("+5 sec");
                }
            }
            else if (loadedScenes[1] == "Snake")
            {
                if (timeInScene <= 30.0f)
                {
                    totalSeconds += 10.0f;
                    timeAddChange("+10 sec");
                }
                else
                {
                    totalSeconds += 5.0f;
                    timeAddChange("+5 sec");
                }
            }
            else if (loadedScenes[1] == "Rat Game")
            {
                if (timeInScene <= 15.0f)
                {
                    totalSeconds += 15.0f;
                    timeAddChange("+15 sec");
                }
                else if (timeInScene <= 30.0f)
                {
                    totalSeconds += 10.0f;
                    timeAddChange("+10 sec");
                }
                else
                {
                    totalSeconds += 5.0f;
                    timeAddChange("+5 sec");
                }
            }

            //if (loadedScenes[1] == "whatever minigame you want")
            //Add x amount of time, or add time based on time spent (timeInScene)
            //if it's pause menu, ignore- do not affect game with pausing
            timeInScene = 0.0f;
            loadedScenes[1] = "None";
        }
    }

    void timeAddChange(string timeAdd)
    {
        timeAddUI.SetActive(true);
        print(timeAdd);
        timeAddText.text = timeAdd;
        Invoke("DisableText", 3f);
    }

    void DisableText()
    {
        timeAddUI.SetActive(false);
    }
}
