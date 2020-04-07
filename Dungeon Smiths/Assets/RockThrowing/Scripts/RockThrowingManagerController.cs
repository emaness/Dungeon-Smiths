using UnityEngine;
using System.Collections;

using UnityEngine.SceneManagement;

public class RockThrowingManagerController : MonoBehaviour
{
    public void Quit()
    {
        SceneManager.UnloadSceneAsync("RockThrowing");

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
