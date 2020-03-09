using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FallIntoPit : MonoBehaviour
{
    public GameObject gameObj;
    private bool gameIsSolo = false;

    // Start is called before the first frame update
    void Start()
    {
        int countLoaded = SceneManager.sceneCount;
        Scene[] loadedScenes = new Scene[countLoaded];

        for (int i = 0; i < countLoaded; i++)
        {
            loadedScenes[i] = SceneManager.GetSceneAt(i);
        }

        if (loadedScenes[0].name == "Minecart Game")
        {
            gameIsSolo = true;
        }
        print(gameIsSolo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!gameIsSolo)
        {
            SceneManager.UnloadSceneAsync("Minecart Game").completed += e =>
            {
                SceneManager.LoadScene("Minecart Game", LoadSceneMode.Additive);
            };
        }
        else
        {
            SceneManager.LoadScene("Minecart Game");
        }
    }
}
