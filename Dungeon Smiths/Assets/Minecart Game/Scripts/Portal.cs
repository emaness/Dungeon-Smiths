using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collider)
    {
        SceneManager.UnloadSceneAsync("Minecart Game");

        Scene mt = SceneManager.GetSceneByName("Level1");
        foreach (GameObject obj in mt.GetRootGameObjects())
        {
            obj.SetActive(true);
        }
    }
}
