using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private bool paused = false;
    public Joystick moveStick;
    public Joystick camStick;
    public GameObject pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void sceneChange(string scene)
    {
        if (paused)
        {
            Time.timeScale = 1f;
            paused = false;
        }
        if (scene == "Exit")
        {
            Application.Quit();
        }
        SceneManager.LoadScene(scene);
    }

    public void CallPauseButton()
    {
        if (!paused)
        {
            foreach (GameObject obj in gameObject.scene.GetRootGameObjects())
            {
                if (obj.name != "Main Camera" && obj.name != "EventSystem")
                {
                    obj.SetActive(false);
                }
            }
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
            paused = true;
        }
        else
        {
            foreach (GameObject obj in gameObject.scene.GetRootGameObjects())
            {
                if (obj.name != "Main Camera" && obj.name != "EventSystem")
                {
                    obj.SetActive(true);
                }
            }
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
            paused = false;
            moveStick.Reset();
            camStick.Reset();
        }
    }
}
