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
    public GameObject healthCanvas;
    public GameObject bubble1;
    public GameObject bubble2;
    public GameObject teleporter;
    public GameObject border1;
    public GameObject border2;
    public GameObject border3;
    private int scriptNum = 0;
    private Animator anim;
    private PlatformerMovement2 moveScript;
    private EnemyPhase1 enemyScript1;
    private EnemyPhase2 enemyScript2;
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
        enemyScript1 = enemy.GetComponent<EnemyPhase1>();
        enemyScript2 = enemy2.GetComponent<EnemyPhase2>();
        text1 = textCanvas.transform.GetChild(1).gameObject;
        text2 = textCanvas.transform.GetChild(2).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (scriptNum == 0)
        {
            canvas.SetActive(false);
            healthCanvas.SetActive(false);
            moveScript.enabled = false;
            enemyScript1.enabled = false;
            enemyScript2.enabled = false;
            anim.SetTrigger("Walk");
        }
        if (scriptNum == 1 || (scriptNum == 6 && winCheck1()) || (scriptNum == 16 && winCheck2()))
        {
            Vector3 temp = player.transform.eulerAngles;
            temp.y = 0.0f;
            player.transform.eulerAngles = temp;
            Vector3 target = new Vector3(7f, -7f, player.transform.position.z);
            player.transform.position = Vector3.MoveTowards(player.transform.position, target, 3.0f * Time.deltaTime);
            if (player.transform.position == target)
            {
                anim.SetTrigger("Idle");
            }
            else
            {
                anim.SetTrigger("Walk");
            }
        }
        if (scriptNum == 5 && !winCheck1())
        {
            if (loseCheck())
            {
                SceneManager.LoadScene("GameOver2");
            }
            bubble1.SetActive(false);
            text1.SetActive(false);
            bubble2.SetActive(false);
            text2.SetActive(false);
            textCanvas.transform.GetChild(0).gameObject.SetActive(false);
            moveScript.enabled = true;
            healthCanvas.SetActive(true);
            enemyScript1.enabled = true;
            canvas.SetActive(true);
            border1.SetActive(true);
            border2.SetActive(true);
            border3.SetActive(true);
        }
        if (scriptNum == 5 && winCheck1())
        {
            moveScript.health = 100;
            moveScript.enabled = false;
            enemyScript1.enabled = false;
            canvas.SetActive(false);
            healthCanvas.SetActive(false);
            anim.SetTrigger("Walk");
            Vector3 target2 = new Vector3(enemy.transform.position.x, enemy.transform.position.y, enemy.transform.position.z);
            teleport = Instantiate(teleporter, target2, Quaternion.identity);
            enemy.SetActive(false);
            scriptNum++;
            StartCoroutine("wait4", 1.0f);
        }
        if (scriptNum == 8)
        {
            Vector3 target = new Vector3(enemy.transform.position.x, -9f, enemy.transform.position.z);
            enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, target, 0.5f * Time.deltaTime);
        }
        if (scriptNum == 10)
        {
            Vector3 target = new Vector3(12.6f, -5f, 0);
            enemy2.transform.position = Vector3.MoveTowards(enemy2.transform.position, target, 0.5f * Time.deltaTime);
        }
        if (scriptNum == 13)
        {
            Vector3 target = new Vector3(12.6f, 7f, 0);
            enemy2.transform.position = Vector3.MoveTowards(enemy2.transform.position, target, 2.0f * Time.deltaTime);
        }
        if (scriptNum == 14)
        {
            Vector3 checkScale = new Vector3(10.0f, 15.0f, 0.0f);
            if ((enemy2.transform.localScale.x != checkScale.x) && (enemy2.transform.localScale.y != checkScale.y))
            {
                Vector3 target = new Vector3(1.0f, 1.5f, 0.0f);
                enemy2.transform.localScale += target;
            }
        }
        if (scriptNum == 15 && !winCheck2())
        {
            if (loseCheck())
            {
                SceneManager.LoadScene("GameOver2");
            }
            bubble1.SetActive(false);
            text1.SetActive(false);
            bubble2.SetActive(false);
            text2.SetActive(false);
            moveScript.enabled = true;
            enemyScript2.enabled = true;
            healthCanvas.SetActive(true);
            canvas.SetActive(true);
        }
        if (scriptNum == 15 && winCheck2())
        {
            moveScript.enabled = false;
            enemyScript2.enabled = false;
            healthCanvas.SetActive(false);
            canvas.SetActive(false);
            anim.SetTrigger("Walk");
            scriptNum++;
        }
        if (scriptNum == 16)
        {
            Vector3 checkScale = new Vector3(2.0f, 2.0f, 0.0f);
            if ((enemy2.transform.localScale.x != checkScale.x) && (enemy2.transform.localScale.y != checkScale.y))
            {
                Vector3 target = new Vector3(1.0f, 1.5f, 0.0f);
                enemy2.transform.localScale -= target;
            }
        }
        if (scriptNum == 17)
        {
            Vector3 target = new Vector3(12.6f, -5f, 0);
            enemy2.transform.position = Vector3.MoveTowards(enemy2.transform.position, target, 0.5f * Time.deltaTime);
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
                textCanvas.transform.GetChild(0).gameObject.SetActive(false);
                moveScript.enabled = true;
                enemyScript1.enabled = true;
                healthCanvas.SetActive(true);
                canvas.SetActive(true);
                scriptNum++;
            }
            else if (scriptNum == 6)
            {
                Destroy(teleport);
                Vector3 target = new Vector3(13f, -6f, 5f);
                enemy.transform.position = target;
                Vector3 temp = enemy.transform.eulerAngles;
                temp.z = -90.0f;
                enemy.transform.eulerAngles = temp;
                anim.SetTrigger("Idle");
                Vector3 target2 = new Vector3(7f, -7f, player.transform.position.z);
                player.transform.position = target2;
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
                anim.SetTrigger("Idle");
                bubble2.SetActive(false);
                text2.SetActive(false);
                enemy.transform.GetChild(2).gameObject.SetActive(false);
                enemy.transform.GetChild(3).gameObject.SetActive(false);
                text1.GetComponent<Text>().text = "Damn it... He's dead.";
                scriptNum++;
            }
            else if (scriptNum == 8)
            {
                anim.SetTrigger("Idle");
                bubble1.SetActive(false);
                text1.SetActive(false);
                bubble2.SetActive(false);
                text2.SetActive(false);
                Destroy(enemy);
                scriptNum++;
            }
            else if (scriptNum == 9)
            {
                anim.SetTrigger("Idle");
                bubble1.SetActive(false);
                text1.SetActive(false);
                bubble2.SetActive(false);
                text2.SetActive(false);
                enemy2.SetActive(true);
                scriptNum++;
            }
            else if (scriptNum == 10)
            {
                anim.SetTrigger("Idle");
                Vector3 target = new Vector3(12.6f, -5f, 0);
                enemy2.transform.position = target;
                bubble1.SetActive(true);
                text1.SetActive(true);
                bubble2.SetActive(true);
                text2.SetActive(true);
                text1.GetComponent<Text>().text = "Damn it...";
                text2.GetComponent<Text>().text = "You really thought that I was done?";
                scriptNum++;
            }
            else if (scriptNum == 11)
            {
                anim.SetTrigger("Idle");
                text1.GetComponent<Text>().text = "Just give me the artifact man.";
                text2.GetComponent<Text>().text = "You're too naive little dungeon smith. Embrace my power!";
                scriptNum++;
            }
            else if (scriptNum == 12)
            {
                anim.SetTrigger("Idle");
                bubble2.SetActive(false);
                text2.SetActive(false);
                text1.GetComponent<Text>().text = "What the...";
                scriptNum++;
            }
            else if (scriptNum == 13)
            {
                anim.SetTrigger("Idle");
                Vector3 target = new Vector3(12.6f, 7f, 0);
                enemy2.transform.position = target;
                bubble2.SetActive(false);
                text2.SetActive(false);
                text1.GetComponent<Text>().text = "Fight me!";
                scriptNum++;
            }
            else if (scriptNum == 14)
            {
                bubble1.SetActive(false);
                text1.SetActive(false);
                bubble2.SetActive(false);
                text2.SetActive(false);
                Vector3 target = new Vector3(10.0f, 14.0f, 0.0f);
                enemy2.transform.localScale = target;
                moveScript.enabled = true;
                enemyScript2.enabled = true;
                healthCanvas.SetActive(true);
                canvas.SetActive(true);
                scriptNum++;
            }
            else if (scriptNum == 16)
            {
                Vector3 target2 = new Vector3(7f, -7f, player.transform.position.z);
                player.transform.position = target2;
                Vector3 target = new Vector3(2.0f, 2.0f, 0.0f);
                enemy2.transform.localScale = target;
                scriptNum++;
            }
            else if (scriptNum == 17)
            {
                anim.SetTrigger("Idle");
                Vector3 target = new Vector3(12.6f, -5f, 0);
                enemy2.transform.position = target;
                bubble1.SetActive(true);
                text1.SetActive(true);
                text1.GetComponent<Text>().text = "Pathetic.";
                scriptNum++;
            }
            else if (scriptNum == 18)
            {
                anim.SetTrigger("Idle");
                bubble2.SetActive(true);
                text2.SetActive(true);
                text1.GetComponent<Text>().text = "Poor choice for last words.";
                text2.GetComponent<Text>().text = "I will have my revenge. It's not over you meddling kid.";
                scriptNum++;
            }
            else if (scriptNum == 19)
            {
                anim.SetTrigger("Idle");
                bubble2.SetActive(false);
                text2.SetActive(false);
                enemy2.transform.GetChild(2).gameObject.SetActive(false);
                enemy2.transform.GetChild(3).gameObject.SetActive(false);
                text1.GetComponent<Text>().text = "Guess I should go back to the king and give him the artifact.";
                scriptNum++;
            }
            else if (scriptNum == 20)
            {
                anim.SetTrigger("Idle");
                bubble1.SetActive(false);
                text1.SetActive(false);
                bubble2.SetActive(false);
                text2.SetActive(false);
                Destroy(enemy2);
                Destroy(player);
                Vector3 camMove = transform.position;
                camMove.x = 100.0f;
                transform.position = camMove;
                textCanvas.transform.GetChild(0).gameObject.SetActive(true);
                textCanvas.transform.GetChild(0).GetComponent<Text>().color = Color.black;
                textCanvas.transform.GetChild(0).GetComponent<Text>().text = "But little did the humans know...";
                scriptNum++;
            }
            else if (scriptNum == 21)
            {
                textCanvas.transform.GetChild(0).gameObject.SetActive(true);
                textCanvas.transform.GetChild(0).GetComponent<Text>().text = "...that this is just the beginning.";
                scriptNum++;
            }
            else if (scriptNum == 22)
            {
                SceneManager.LoadScene("YouWin");
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
        bubble2.SetActive(true);
        text2.SetActive(true);
        text2.GetComponent<Text>().text = "Damn.. *cough* You've got me.";
    }

    private bool winCheck1()
    {
        if (enemyScript1.health <= 0)
        {
            return true;
        }
        return false;
    }

    private bool winCheck2()
    {
        if (enemyScript2.health <= 0)
        {
            return true;
        }
        return false;
    }

    private bool loseCheck()
    {
        if (moveScript.health <= 0)
        {
            return true;
        }
        return false;
    }
}
