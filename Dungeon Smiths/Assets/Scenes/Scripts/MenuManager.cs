using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Joystick moveStick;
    public Joystick camStick;
    public GameObject play;
    public GameObject levels;
    public GameObject exit;
    public GameObject level1;
    public GameObject level2;
    public GameObject level3;
    public GameObject back;

    public void sceneChange(string scene)
    {
        Time.timeScale = 1f;
        if (scene == "Exit")
        {
            Application.Quit();
        }
        SceneManager.LoadScene(scene);
    }

    public void CallPauseButton()
    {
        moveStick.Reset();
        camStick.Reset();
        foreach (GameObject obj in gameObject.scene.GetRootGameObjects())
        {
            if (obj.name != "MenuManager")
            {
                obj.SetActive(false);
            }
        }
        SceneManager.LoadScene("PauseMenu", LoadSceneMode.Additive);
        Time.timeScale = 0f;
    }

    public void Resume()
    {

        SceneManager.UnloadSceneAsync("PauseMenu");
        Scene mainLevel = SceneManager.GetSceneByName(SceneManager.GetActiveScene().name);
        foreach (GameObject obj in mainLevel.GetRootGameObjects())
        {
            obj.SetActive(true);
        }
        Time.timeScale = 1f;
    }

    public void LevelButton()
    {
        play.SetActive(false);
        levels.SetActive(false);
        exit.SetActive(false);
        level1.SetActive(true);
        level2.SetActive(true);
        level3.SetActive(true);
        back.SetActive(true);
    }

    public void backButton()
    {
        play.SetActive(true);
        levels.SetActive(true);
        exit.SetActive(true);
        level1.SetActive(false);
        level2.SetActive(false);
        level3.SetActive(false);
        back.SetActive(false);
    }
}
