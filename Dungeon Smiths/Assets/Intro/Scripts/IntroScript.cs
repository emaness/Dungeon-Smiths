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
    public GameObject teleporter;
    private int scriptNum = 0;
    private Animator anim;
    private GameObject text1;
    private GameObject text2;
    private GameObject text3;
    private GameObject text4;
    private GameObject text5;
    private GameObject teleport;


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
        else if (scriptNum == 15)
        {
            Vector3 target = new Vector3(-30f, -7f, player.transform.position.z);
            player.transform.position = Vector3.MoveTowards(player.transform.position, target, 3.0f * Time.deltaTime);
        }
        else if (scriptNum == 21)
        {
            Vector3 target = new Vector3(90f, -5f, player.transform.position.z);
            player.transform.position = Vector3.MoveTowards(player.transform.position, target, 3.0f * Time.deltaTime);
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
                bubble4.SetActive(true);
                text4.SetActive(true);
                text4.GetComponent<Text>().text = "On my phone, my military forces have shown me this.";
                Vector3 camMove = transform.position;
                camMove.x = 121;
                transform.position = camMove;
                textCanvas.transform.GetChild(0).gameObject.SetActive(false);
                scriptNum++;
            }
            else if (scriptNum == 6)
            {
                bubble3.SetActive(true);
                text3.SetActive(true);
                bubble4.SetActive(true);
                text4.SetActive(true);
                text3.GetComponent<Text>().text = "What is this antique phone you have here, a Samsung Galaxy? I should donate my holophone.";
                text4.GetComponent<Text>().text = "It is old but gold.";
                scriptNum++;
            }
            else if (scriptNum == 7)
            {
                text3.GetComponent<Text>().text = "That is true, but a holophone has access to-";
                text4.GetComponent<Text>().text = "We're not here to talk about phones. Let's move on.";
                scriptNum++;
            }
            else if (scriptNum == 8)
            {
                bubble3.SetActive(false);
                text3.SetActive(false);
                text4.GetComponent<Text>().text = "Now you see, civilians are dying to these aliens. Our technology isn't enough to stop them. We need you to recover the artifact, you will be paid well.";
                scriptNum++;
            }
            else if (scriptNum == 9)
            {
                bubble3.SetActive(true);
                text3.SetActive(true);
                bubble4.SetActive(false);
                text4.SetActive(false);
                text3.GetComponent<Text>().text = "So, payment. I need resources to access this dungeon.";
                scriptNum++;
            }
            else if (scriptNum == 10)
            {
                bubble3.SetActive(false);
                text3.SetActive(false);
                bubble4.SetActive(true);
                text4.SetActive(true);
                text4.GetComponent<Text>().text = "You'll have the resources to do it. Alone.";
                scriptNum++;
            }
            else if (scriptNum == 11)
            {
                bubble1.SetActive(true);
                text1.SetActive(true);
                bubble2.SetActive(true);
                text2.SetActive(true);
                bubble4.SetActive(false);
                text4.SetActive(false);
                Vector3 camMove = transform.position;
                camMove.x = 0.05f;
                transform.position = camMove;
                text1.GetComponent<Text>().text = "Alone?";
                text2.GetComponent<Text>().text = "Alone.";
                scriptNum++;
            }
            else if (scriptNum == 12)
            {
                bubble1.SetActive(false);
                text1.SetActive(false);
                bubble2.SetActive(true);
                text2.SetActive(true);
                text2.GetComponent<Text>().text = "My troops and resources need to go save our city. Our city is under constant flak with the aliens.";
                scriptNum++;
            }
            else if (scriptNum == 13)
            {
                bubble1.SetActive(true);
                text1.SetActive(true);
                bubble2.SetActive(false);
                text2.SetActive(false);
                text1.GetComponent<Text>().text = "Very well your highness. The artifact will be in your hands.";
                scriptNum++;
            }
            else if (scriptNum == 14)
            {
                bubble1.SetActive(false);
                text1.SetActive(false);
                anim.SetTrigger("Walk");
                Vector3 rotate = new Vector3(0, 180, 0);
                player.transform.eulerAngles = rotate;
                scriptNum++;
            }
            else if (scriptNum == 15)
            {
                anim.SetTrigger("Idle");
                Vector3 target = new Vector3(-30f, -7f, player.transform.position.z);
                player.transform.position = target;
                Vector3 target2 = new Vector3(enemy.transform.position.x, enemy.transform.position.y, -5f);
                teleport = Instantiate(teleporter, target2, Quaternion.identity);
                StartCoroutine("wait15", 1.0f);
                scriptNum++;
            }
            else if (scriptNum == 16)
            {
                Destroy(teleport);
                bubble1.SetActive(false);
                text1.SetActive(false);
                bubble2.SetActive(true);
                text2.SetActive(true);
                text2.GetComponent<Text>().text = "There is a reason, you old wizard. He has potential, and a history of skill in dungeons. You are too old now, you need to be on the frontlines.";
                scriptNum++;
            }
            else if (scriptNum == 17)
            {
                bubble1.SetActive(true);
                text1.SetActive(true);
                bubble2.SetActive(false);
                text2.SetActive(false);
                text1.GetComponent<Text>().text = "Very well, we will see how your joker does.";
                scriptNum++;
            }
            else if (scriptNum == 18)
            {
                Vector3 target2 = new Vector3(enemy.transform.position.x, enemy.transform.position.y, -5f);
                teleport = Instantiate(teleporter, target2, Quaternion.identity);
                StartCoroutine("wait18", 1.0f);
                scriptNum++;
            }
            else if (scriptNum == 19)
            {
                Destroy(teleport);
                Vector3 camMove = transform.position;
                camMove.x = 60.0f;
                transform.position = camMove;
                Vector3 target = new Vector3(70.95f, -5f, player.transform.position.z);
                player.transform.position = target;
                Vector3 rotate = new Vector3(0, 0, 0);
                player.transform.eulerAngles = rotate;
                bubble5.SetActive(true);
                text5.SetActive(true);
                text5.GetComponent<Text>().text = "So this is the dungeon. Let's start hunting for artifacts, shall we?";
                scriptNum++;
            }
            else if (scriptNum == 20)
            {
                bubble5.SetActive(false);
                text5.SetActive(false);
                anim.SetTrigger("Walk");
                scriptNum++;
            }
            else if (scriptNum == 21)
            {
                anim.SetTrigger("Idle");
                Vector3 target2 = new Vector3(90f, -5f, player.transform.position.z);
                player.transform.position = target2;
                Vector3 target = new Vector3(70.95f, -3f, enemy.transform.position.z);
                enemy.transform.position = target;
                Vector3 target3 = new Vector3(enemy.transform.position.x, enemy.transform.position.y, -5f);
                teleport = Instantiate(teleporter, target3, Quaternion.identity);
                StartCoroutine("wait21", 1.0f);
                scriptNum++;
            }
            else if (scriptNum == 22)
            {
                SceneManager.LoadScene("Level1");
            }
        }
    }

    private IEnumerator wait15(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(teleport);
        enemy.SetActive(true);
        bubble1.SetActive(true);
        text1.SetActive(true);
        text1.GetComponent<Text>().text = "You hired an amateur like him over me?";
    }

    private IEnumerator wait18(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(teleport);
        bubble1.SetActive(false);
        text1.SetActive(false);
        enemy.SetActive(false);
    }

    private IEnumerator wait21(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(teleport);
        bubble5.SetActive(true);
        text5.SetActive(true);
        enemy.SetActive(true);
        text5.GetComponent<Text>().text = "So it begins. We shall see, young one.";
    }
}
