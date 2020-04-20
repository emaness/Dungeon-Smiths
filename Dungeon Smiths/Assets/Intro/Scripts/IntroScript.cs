using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroScript : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;
    public GameObject textCanvas;
    public GameObject bubble1;
    public GameObject bubble2;
    public GameObject bubble3;
    public GameObject bubble4;
    public GameObject bubble5;
    private int scriptNum = 0;
    private Animator anim;
    private GameObject text1;
    private GameObject text2;
    private GameObject text3;
    private GameObject text4;
    private GameObject text5;


    // Start is called before the first frame update
    void Start()
    {
        float width = 16f;
        float height = 9f;
        Camera.main.aspect = width / height;
        anim = player.GetComponent<Animator>();
        text1 = textCanvas.transform.GetChild(1).gameObject;
        text2 = textCanvas.transform.GetChild(2).gameObject;
        text3 = textCanvas.transform.GetChild(3).gameObject;
        text4 = textCanvas.transform.GetChild(4).gameObject;
        text5 = textCanvas.transform.GetChild(5).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (scriptNum == 1)
        {
            Vector3 target = new Vector3(-5.7f, -7f, player.transform.position.z);
            player.transform.position = Vector3.MoveTowards(player.transform.position, target, 3.0f * Time.deltaTime);
            if (player.transform.position == target)
            {
                anim.SetTrigger("Idle");
            }
        }
        if ((Input.GetMouseButtonDown(0) || Input.touchCount > 0))
        {
            if (scriptNum == 0)
            {
                anim.SetTrigger("Walk");
                scriptNum++;
            }
            else if (scriptNum == 1)
            {
                anim.SetTrigger("Idle");
                Vector3 target = new Vector3(-5.7f, -7f, player.transform.position.z);
                player.transform.position = target;
                bubble1.SetActive(true);
                text1.SetActive(true);
                bubble2.SetActive(true);
                text2.SetActive(true);
                text1.GetComponent<Text>().text = "Your Highness.";
                text2.GetComponent<Text>().text = "Ah, young dungeon smith. Now is the time that we need you.";
                scriptNum++;
            }
            else if (scriptNum == 2)
            {
                bubble2.SetActive(false);
                text2.SetActive(false);
                text1.GetComponent<Text>().text = "I would like to pursue the venture into digging into the dungeon.";
                scriptNum++;
            }
            else if (scriptNum == 3)
            {
                bubble1.SetActive(false);
                text1.SetActive(false);
                bubble2.SetActive(true);
                text2.SetActive(true);
                text2.GetComponent<Text>().text = "Now is the time we need you young one. The world is under serious threat.";
                scriptNum++;
            }
            else if (scriptNum == 4)
            {
                bubble1.SetActive(true);
                text1.SetActive(true);
                text1.GetComponent<Text>().text = "How So?";
                text2.GetComponent<Text>().text = "Let me show you some images of war crimes. The Aliens are coming, and they won't stop now.";
                scriptNum++;
            }
            else if (scriptNum == 5)
            {
                bubble1.SetActive(false);
                text1.SetActive(false);
                bubble2.SetActive(false);
                text2.SetActive(false);
                scriptNum++;
            }
            else if (scriptNum == 6)
            {
                scriptNum++;
            }
            else if (scriptNum == 7)
            {
                scriptNum++;
            }
            else if (scriptNum == 8)
            {
                scriptNum++;
            }
            else if (scriptNum == 9)
            {
                scriptNum++;
            }
        }
    }
}
