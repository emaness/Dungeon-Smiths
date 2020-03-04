using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageGame : MonoBehaviour
{
    public void restart()
	{

	}

    /*
    public IEnumerator advance()
	{
        yield return new WaitForSeconds(4.0f);

        SceneManager.UnloadSceneAsync("CaveIn");

        Scene mt = SceneManager.GetSceneByName("Level 2");

        foreach (GameObject obj in mt.GetRootGameObjects())
        {
            if (obj.name != "PauseMenu")
            {
                obj.SetActive(true);
            }
        }
    }
    */
}
