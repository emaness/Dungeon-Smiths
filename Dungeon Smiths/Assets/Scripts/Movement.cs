using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    public GameObject cam;
    public Joystick moveStick;
    public Joystick camStick;
    private Rigidbody rigid;
    //private float moveSpeed = 10.0f;
    //private float camSpeed = 100.0f;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
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
		}
		if (scene != null)
        {
            Destroy(other.gameObject);
            moveStick.Reset();
            camStick.Reset();

            foreach (GameObject obj in gameObject.scene.GetRootGameObjects())
            {
                if (obj.name != "CanvasStays" && obj.name != "MenuManager")
                {
                    obj.SetActive(false);
                }
            }
            SceneManager.LoadScene(scene, LoadSceneMode.Additive);
        }
    }
}
