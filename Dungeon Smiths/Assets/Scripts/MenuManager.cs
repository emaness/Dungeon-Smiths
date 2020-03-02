using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Joystick moveStick;
    public Joystick camStick;

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
}
