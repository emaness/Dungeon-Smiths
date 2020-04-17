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
	private bool[] isFirstTime;

	public int numKeys;
	
	public AudioSource audio;
	public AudioClip footSteps;
	public AudioClip keySound;
	public AudioClip shakingSound;

    public bool isCollapsingLevel = false;

    public Text keyText = null;

    // Start is called before the first frame update
    void Start()
    {
		isFirstTime = new bool[12];
		for (int i = 0; i < isFirstTime.Length; i++) { isFirstTime[i] = true; }

		rigid = GetComponent<Rigidbody>();
		audio = GetComponent<AudioSource>();
    }

    IEnumerator CollapsingRoutine()
    {
		yield return new WaitForSeconds(1.0f);

        var origPos = cam.transform.position;

		audio.PlayOneShot(shakingSound);

		for (var i = 0; i != 75; ++i)
        {
            cam.transform.position = new Vector3(
                origPos.x + Random.Range(-0.4f, 0.4f),
                origPos.y + Random.Range(-0.4f, 0.4f),
                origPos.z);

            yield return new WaitForSeconds(0.05f);
        }

		audio.Stop();
        cam.transform.position = origPos;

        enterMinigame("CaveIn");
		yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        rigid.constraints = RigidbodyConstraints.FreezeAll;
        if (moveStick.Horizontal != 0 || moveStick.Vertical != 0)
        {
            transform.Translate(-moveStick.Horizontal * 0.2f, 0, -moveStick.Vertical * 0.2f);
            rigid.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;

            if (audio.isPlaying == false)
            {
                audio.volume = Random.Range(0.6f, .8f);
                audio.pitch = Random.Range(0.9f, 1.05f);
                audio.PlayOneShot(footSteps);
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

		bool needInstruction = getFirstTime(sceneName);
		int numberOfNeed = 0;
		if (needInstruction) numberOfNeed = 1;
		PlayerPrefs.SetInt("isFirstTime", numberOfNeed);

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

		Debug.Log("trigger enter?");

		if (other.gameObject.CompareTag("FireObstacle"))
		{
			scene = "FireGame";
		}
		else if (other.gameObject.CompareTag("TileMatchingCube"))
		{
			scene = "TileMatchingGame";
		}
		else if (other.gameObject.CompareTag("Minecart"))
		{
			scene = "Minecart Game";
		}
		else if (other.gameObject.CompareTag("Skeleton"))
		{
			scene = "RockThrowing";
		}
		else if (other.gameObject.CompareTag("Spider"))
		{
			scene = "spiderScene";
		}
		else if (other.gameObject.CompareTag("EndLevel1Rock"))
		{
			scene = "CaveIn";
		}
		else if (other.gameObject.CompareTag("Gorilla"))
		{
			scene = "FireDragon";
		}
		else if (other.gameObject.CompareTag("KnightScene"))
		{
			scene = "KnightScene";
		}
		else if (other.gameObject.CompareTag("Snake"))
		{
			scene = "Snake";
		}
		else if (other.gameObject.CompareTag("Chest"))
		{
			scene = "final2Scene";
		} 
		else if (other.gameObject.CompareTag("Rat"))
		{
			scene = "RatGame";
		}
		else if (other.gameObject.CompareTag("RockTroll"))
		{
			scene = "RockTroll";
		}
		else if (other.gameObject.CompareTag("ScorpionInsect"))
		{
			scene = "ScorpionInsect";
		}
        else if(other.gameObject.CompareTag("Level2Exit"))
        {
			scene = "JumpKing";
        }

		if (other.gameObject.CompareTag("KeyPart"))
        {

			audio.PlayOneShot(keySound);
            int count = int.Parse(keyText.text.Substring(6, 1));
            ++count;
            keyText.text = "Keys: " + count + "/" + numKeys;

			if (isCollapsingLevel && count == numKeys/2)
			{
				StartCoroutine("CollapsingRoutine");
			}

			Destroy(other.gameObject);
			if(count == numKeys)
			{
				Destroy(keyDoors.gameObject);
			}
        }

        if (scene != null)
        {
            Destroy(other.gameObject);
            enterMinigame(scene);
			setFirstTime(scene);
        }
    }

	void setFirstTime(string scene)
	{
		if (scene.Equals("FireObstacle"))
		{
			isFirstTime[0] = false;
		}
		else if (scene.Equals("TileMatchingCube"))
		{
			isFirstTime[1] = false;
		}
		else if (scene.Equals("RockTroll"))
		{
			isFirstTime[2] = false;
		}
		else if (scene.Equals("Skeleton"))
		{
			isFirstTime[3] = false;
		}
		else if (scene.Equals("Spider"))
		{
			isFirstTime[4] = false;
		}
		else if (scene.Equals("EndLevel1Rock"))
		{
			
			isFirstTime[5] = false;
		}
		else if (scene.Equals("Gorilla"))
		{
			isFirstTime[6] = false;
		}
		else if (scene.Equals("KnightScene"))
		{
			isFirstTime[7] = false;
		}
		else if (scene.Equals("Snake"))
		{
			isFirstTime[8] = false;
		}
		else if (scene.Equals("chest"))
		{
			isFirstTime[9] = false;
		}
		else if (scene.Equals("RatGame"))
		{
			isFirstTime[10] = false;
		}
		else if (scene.Equals("ScorpionInsect"))
		{
			isFirstTime[11] = false;
		}
	}

	public bool getFirstTime(string scene)
	{
		if (scene.Equals("FireObstacle"))
		{
			return isFirstTime[0];
		}
		else if (scene.Equals("TileMatchingCube"))
		{
			return isFirstTime[1];
		}
		else if (scene.Equals("RockTroll"))
		{
			return isFirstTime[2];
		}
		else if (scene.Equals("Skeleton"))
		{
			return isFirstTime[3];
		}
		else if (scene.Equals("Spider"))
		{
			return isFirstTime[4];
		}
		else if (scene.Equals("EndLevel1Rock"))
		{

			return isFirstTime[5];
		}
		else if (scene.Equals("Gorilla"))
		{
			return isFirstTime[6];
		}
		else if (scene.Equals("KnightScene"))
		{
			return isFirstTime[7];
		}
		else if (scene.Equals("Snake"))
		{
			return isFirstTime[8];
		}
		else if (scene.Equals("chest"))
		{
			return isFirstTime[9];
		}
		else if (scene.Equals("RatGame"))
		{
			return isFirstTime[10];
		}
		else if (scene.Equals("ScorpionInsect"))
		{
			return isFirstTime[11];
		}
		else return false;
	}
}
