using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndingScript : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;
    public GameObject enemy2;
    public GameObject textCanvas;
    public GameObject canvas;
    public GameObject bubble1;
    public GameObject bubble2;
    public GameObject teleporter;
    private int scriptNum = 0;
    private Animator anim;
    private PlatformerMovement2 moveScript;
    private GameObject text1;
    private GameObject text2;
    private GameObject teleport;

    // Start is called before the first frame update
    void Start()
    {
        float width = 16f;
        float height = 9f;
        Camera.main.aspect = width / height;
        anim = player.GetComponent<Animator>();
        moveScript = player.GetComponent<PlatformerMovement2>();
        text1 = textCanvas.transform.GetChild(1).gameObject;
        text2 = textCanvas.transform.GetChild(2).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (scriptNum == 0)
        {
            canvas.SetActive(false);
            moveScript.enabled = false;
            anim.SetTrigger("Walk");
        }
        if (scriptNum == 1)
        {
            Vector3 target = new Vector3(7f, -7f, player.transform.position.z);
            player.transform.position = Vector3.MoveTowards(player.transform.position, target, 3.0f * Time.deltaTime);
            if (player.transform.position == target)
            {
                anim.SetTrigger("Idle");
            }
        }
        else if (scriptNum == 8)
        {
            Vector3 target = new Vector3(enemy.transform.position.x, -9f, enemy.transform.position.z);
            enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, target, 0.1f * Time.deltaTime);
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
                Vector3 target = new Vector3(7f, -7f, player.transform.position.z);
                player.transform.position = target;
                Vector3 target2 = new Vector3(enemy.transform.position.x, enemy.transform.position.y, -5f);
                teleport = Instantiate(teleporter, target2, Quaternion.identity);
                StartCoroutine("wait2", 1.0f);
                scriptNum++;
            }
            else if (scriptNum == 2)
            {
                Destroy(teleport);
                enemy.SetActive(true);
                bubble1.SetActive(true);
                text1.SetActive(true);
                bubble2.SetActive(true);
                text2.SetActive(true);
                text1.GetComponent<Text>().text = "You again old man.";
                text2.GetComponent<Text>().text = "With such an artifact, of course I'd stick around. You think the one you picked up is real?";
                scriptNum++;
            }
            else if (scriptNum == 3)
            {
                text1.GetComponent<Text>().text = "Damn it... give me that right now.";
                text2.GetComponent<Text>().text = "Bring it on joker. You can't even touch me.";
                scriptNum++;
            }
            else if (scriptNum == 4)
            {
                bubble1.SetActive(false);
                text1.SetActive(false);
                bubble2.SetActive(false);
                text2.SetActive(false);
                canvas.SetActive(true);
                textCanvas.transform.GetChild(0).gameObject.SetActive(false);
                moveScript.enabled = true;
                canvas.SetActive(true);
                scriptNum++;
            }
            else if (scriptNum == 5 && winCheck1())
            {
                moveScript.enabled = false;
                canvas.SetActive(false);
                Vector3 target2 = new Vector3(enemy.transform.position.x, enemy.transform.position.y, enemy.transform.position.z);
                teleport = Instantiate(teleporter, target2, Quaternion.identity);
                enemy.SetActive(false);
                scriptNum++;
                StartCoroutine("wait4", 1.0f);
            }
            else if (scriptNum == 6)
            {
                Destroy(teleport);
                Vector3 target = new Vector3(13f, -6f, 5f);
                enemy.transform.position = target;
                Vector3 temp = enemy.transform.eulerAngles;
                temp.z = -90.0f;
                enemy.transform.eulerAngles = temp;
                enemy.SetActive(true);
                bubble1.SetActive(true);
                text1.SetActive(true);
                bubble2.SetActive(false);
                text2.SetActive(false);
                text1.GetComponent<Text>().text = "Now give me that artifact.";
                scriptNum++;
            }
            else if (scriptNum == 7)
            {
                enemy.transform.GetChild(2).gameObject.SetActive(false);
                enemy.transform.GetChild(3).gameObject.SetActive(false);
                text1.GetComponent<Text>().text = "Damn it... He's dead.";
                scriptNum++;
            }
            else if (scriptNum == 8)
            {
                bubble1.SetActive(false);
                text1.SetActive(false);
                Destroy(enemy);
                scriptNum++;
                //Summon enemy 2
                //Start boss 2
                //End boss 2
                //Rest of cutscene is similar to intro in scripting
            }
        }
    }
    
    private IEnumerator wait2(float time)
    {
        yield return new WaitForSeconds(time);
        anim.SetTrigger("Idle");
        Destroy(teleport);
        enemy.SetActive(true);
        bubble2.SetActive(true);
        text2.SetActive(true);
        text2.GetComponent<Text>().text = "You think you are so clever.";
    }

    private IEnumerator wait4(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(teleport);
        Vector3 target2 = new Vector3(12.6f, -5f, 5f);
        teleport = Instantiate(teleporter, target2, Quaternion.identity);
        if (scriptNum == 6)
        {
            StartCoroutine("wait5", 1.0f);
        }
        else
        {
            Destroy(teleport);
        }
    }

    private IEnumerator wait5(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(teleport);
        Vector3 target = new Vector3(13f, -6f, 5f);
        enemy.transform.position = target;
        Vector3 temp = enemy.transform.eulerAngles;
        temp.z = -90.0f;
        enemy.transform.eulerAngles = temp;
        enemy.SetActive(true);
        bubble1.SetActive(true);
        text1.SetActive(true);
        bubble2.SetActive(true);
        text2.SetActive(true);
        text1.GetComponent<Text>().text = "Pathetic.";
        text2.GetComponent<Text>().text = "Damn.. *cough* You've got me.";
    }


    private bool winCheck1()
    {
        return true;
    }

    private bool winCheck2()
    {
        return true;
    }
}
