using UnityEngine;
using System.Collections;

using UnityEngine.SceneManagement;

public class RockThrowingManagerController : MonoBehaviour
{
    private string sceneName
    {
        get
        {
            if (GameObject.Find("RockTroll") != null)
            {
                return "RockTroll";
            }
            else
            {
                return "RockThrowing";
            }
        }
    }

    public void Quit()
    {
        SceneManager.UnloadSceneAsync(sceneName);

        Scene mt = SceneManager.GetActiveScene();
        // Scene mt = SceneManager.GetSceneByName("Level1");
        foreach (GameObject obj in mt.GetRootGameObjects())
        {
            if (obj.name != "PauseMenu")
            {
                obj.SetActive(true);
            }
        }
    }
}
