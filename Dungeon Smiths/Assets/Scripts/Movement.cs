using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    public GameObject cam;
    public Joystick moveStick;
    public Joystick camStick;
    private Rigidbody rigid;
	public GameObject keyDoors;

    public bool isCollapsingLevel = false;

    public Text keyText = null;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();

        if(isCollapsingLevel)
        {
            StartCoroutine("CollapsingRoutine");
        }
    }

    IEnumerator CollapsingRoutine()
    {
        yield return new WaitForSeconds(5.0f);

        var origPos = cam.transform.position;

        for(var i = 0; i != 75; ++i)
        {
            cam.transform.position = new Vector3(
                origPos.x + Random.Range(-0.4f, 0.4f),
                origPos.y + Random.Range(-0.4f, 0.4f),
                origPos.z);

            yield return new WaitForSeconds(0.05f);
        }
        cam.transform.position = origPos;

        enterMinigame("CaveIn");
    }

    // Update is called once per frame
    void Update()
    {
        rigid.constraints = RigidbodyConstraints.FreezeAll;
        if (moveStick.Horizontal != 0 || moveStick.Vertical != 0)
        {
            transform.Translate(-moveStick.Horizontal * 0.2f, 0, -moveStick.Vertical * 0.2f);
            rigid.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;

            if (GetComponent<AudioSource>().isPlaying == false)
            {
                GetComponent<AudioSource>().volume = Random.Range(0.6f, .8f);
                GetComponent<AudioSource>().pitch = Random.Range(0.9f, 1.05f);
                GetComponent<AudioSource>().Play();
            }
        }
        if (camStick.Horizontal != 0 || camStick.Vertical != 0)
        {
            transform.Rotate(0, camStick.Horizontal * 3.0f, 0);
            cam.transform.Rotate(0, camStick.Horizontal * 3.0f, 0);
            rigid.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
        }
    }

    private void enterMinigame(string sceneName)
    {
        moveStick.Reset();
        camStick.Reset();

        foreach (GameObject obj in gameObject.scene.GetRootGameObjects())
        {
            if (obj.name != "CanvasStays" && obj.name != "MenuManager")
            {
                obj.SetActive(false);
            }
        }
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }

    private void OnTriggerEnter(Collider other)
    {
        string scene = null;

        if(other.gameObject.CompareTag("FireObstacle"))
        {
            scene = "FireGame";
        } else if(other.gameObject.CompareTag("TileMatchingCube"))
        {
            scene = "TileMatchingGame";
        } else if(other.gameObject.CompareTag("Minecart"))
        {
            scene = "Minecart Game";  
        } else if(other.gameObject.CompareTag("Skeleton"))
        {
            scene = "RockThrowing";
        } else if(other.gameObject.CompareTag("Spider"))
        {
            scene = "spiderScene";
        } else if(other.gameObject.CompareTag("EndLevel1Rock"))
        {
            scene = "CaveIn";
        } else if(other.gameObject.CompareTag("Gorilla"))
        {
            scene = "FireDragon";
        }
        else if (other.gameObject.CompareTag("KnightScene"))
        {
            scene = "KnightScene";
        } else if(other.gameObject.CompareTag("Snake"))
        {
            scene = "Snake";
        }

        if (other.gameObject.CompareTag("KeyPart"))
        {
            int count = int.Parse(keyText.text.Substring(6, 1));
            ++count;
            keyText.text = "Keys: " + count + "/5";
            Destroy(other.gameObject);
			if(count == 5)
			{
				Destroy(keyDoors.gameObject);
			}
        }

        if (scene != null)
        {
            Destroy(other.gameObject);
            enterMinigame(scene);
        }
    }
}
