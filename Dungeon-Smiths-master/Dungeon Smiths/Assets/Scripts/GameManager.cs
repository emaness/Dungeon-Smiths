using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager :Singleton<GameManager>
{
    [SerializeField]
    private Text info;

    [SerializeField]
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void SetInfo(string ifo)
    {
        info.transform.parent.gameObject.SetActive(true);
        player.GetComponent<Player>().enabled = false;
        info.text = ifo;
    }

    public void OnResetButton()
    {
        info.transform.parent.gameObject.SetActive(false);
        player.GetComponent<Player>().enabled = false;
        SceneManager.LoadScene(0);
    }
}
