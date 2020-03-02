using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageGame : MonoBehaviour
{
    public void restart()
	{

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
    public IEnumerator advance()
	{
        yield return new WaitForSeconds(2.5f);

        SceneManager.UnloadSceneAsync("CaveIn");

        Scene mt = SceneManager.GetSceneByName("Level1");

        foreach (GameObject obj in mt.GetRootGameObjects())
        {
            if (obj.name != "PauseMenu")
            {
                obj.SetActive(true);
            }
        }
    }
}
