using UnityEngine;
using System.Collections;

using UnityEngine.SceneManagement;

public class AngryRocksManagerController : MonoBehaviour
{
    public void Quit()
    {
        SceneManager.UnloadSceneAsync("AngryRocks");

        Scene mt = SceneManager.GetSceneByName("Level1");
        foreach (GameObject obj in mt.GetRootGameObjects())
        {
            obj.SetActive(true);
        }
    }
}
