using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;


public class Snake : MonoBehaviour{
	Vector2 dir = Vector2.right;
	List<Transform> tail = new List<Transform>();
	string butDir ;
	bool ate = false;
	bool isDied = false;
	int tailLength = 2;
	public GameObject tailPrefab;
	public Text instructions;



	public IEnumerator DoInstructions()
	{
		if (PlayerPrefs.GetInt("isFirstTime") == 1)
		{
			print("entered isfirst time");
			instructions.gameObject.SetActive(true);
			Time.timeScale = 0.0f;
			float pauseEndTime = Time.realtimeSinceStartup + 4;
			while (Time.realtimeSinceStartup < pauseEndTime)
			{
				yield return 0;
			}
			Time.timeScale = 1f;
			instructions.gameObject.SetActive(false);
			PlayerPrefs.SetInt("isFirstTime", 0);


			var prevActive = SceneManager.GetActiveScene();
			SceneManager.SetActiveScene(SceneManager.GetSceneByName("Snake"));
			for (int i = tailLength; i > 0; i--)
			{
				Vector2 v = transform.position;
				v.x = v.x - i;
				GameObject g = (GameObject)Instantiate(tailPrefab,
														  v,
														  Quaternion.identity);
				tail.Insert(0, g.transform);
			}

			InvokeRepeating("Move", 0.1f, 0.1f);
			SceneManager.SetActiveScene(prevActive);

		}

	}

	// Start is called before the first frame update
	void Start(){

		//PlayerPrefs.SetInt("isFirstTime", 0);

		instructions.gameObject.SetActive(false);

		if (PlayerPrefs.GetInt("isFirstTime") == 1)
		{

			StartCoroutine("DoInstructions");
		}
		else
		{
			var prevActive = SceneManager.GetActiveScene();
			SceneManager.SetActiveScene(SceneManager.GetSceneByName("Snake"));
			for (int i = tailLength; i > 0; i--)
			{
				Vector2 v = transform.position;
				v.x = v.x - i;
				GameObject g = (GameObject)Instantiate(tailPrefab,
														  v,
														  Quaternion.identity);
				tail.Insert(0, g.transform);
			}

			InvokeRepeating("Move", 0.1f, 0.1f);
			SceneManager.SetActiveScene(prevActive);
		}

    }

    // Update is called once per frame
    void Update(){
		if (!isDied) {
			if (Input.GetKey(KeyCode.RightArrow))
				dir = Vector2.right;
			else if (Input.GetKey(KeyCode.DownArrow))
				dir = -Vector2.up;    // '-up' means 'down'
			else if (Input.GetKey(KeyCode.LeftArrow))
				dir = -Vector2.right; // '-right' means 'left'
			else if (Input.GetKey(KeyCode.UpArrow))
				dir = Vector2.up;

			/*if (Input.GetButtonDown("RightButton"))
				dir = Vector2.right;
			else if (Input.GetButtonDown("DownButton"))
				dir = -Vector2.up;    // '-up' means 'down'
			else if (Input.GetButtonDown("LeftButton"))
				dir = -Vector2.right; // '-right' means 'left'
			else if (Input.GetButtonDown("UpButton"))
				dir = Vector2.up;*/
			/*if (butDir == "up"){
				dir = Vector2.up;
				//Debug.Log(butDir);
			}
			else if (butDir == "down")
				dir = -Vector2.up;
			else if (butDir == "right")
				dir = Vector2.right;
			else if (butDir == "left")
				dir = -Vector2.right;*/
			//Debug.Log(butDir);
		//}
		    if (CrossPlatformInputManager.GetAxis("Horizontal") != 0){
		    	dir = Vector2.right * CrossPlatformInputManager.GetAxis("Horizontal");
		    }
		    else if (CrossPlatformInputManager.GetAxis("Vertical") != 0){
		    	dir = Vector2.up * CrossPlatformInputManager.GetAxis("Vertical");
		    }
	    }
    }
    void Move(){
    	Vector2 v = transform.position;
    	Debug.Log(dir.ToString());
		transform.Translate(dir);
		if (!isDied){
			if (ate) {
		        // Load Prefab into the world
		        //GameObject g =(GameObject)Instantiate(tailPrefab,
		        //                                      v,
		        //                                      Quaternion.identity);

		        // Keep track of it in our tail list
		        //tail.Insert(0, g.transform);
		        if(tail.Count >1){
					Destroy(tail[tail.Count-1].gameObject);
					tail.RemoveAt(tail.Count-1);
					tail.Last().position = v;
					tail.Insert(0, tail.Last());
					tail.RemoveAt(tail.Count-1);
			        // Reset the flag
			        ate = false;
		    	}
		    	else{
		    		Destroy(tail[tail.Count-1].gameObject);

					SceneManager.UnloadSceneAsync("Snake");
					Scene mt = SceneManager.GetActiveScene();
					foreach (GameObject obj in mt.GetRootGameObjects())
					{
						if (obj.name != "PauseMenu")
						{
							obj.SetActive(true);
						}
					}
				}
		    }
		    else if (tail.Count > 0){
		        // Move last Tail Element to where the Head was
		        tail.Last().position = v;

		        // Add to front of list, remove from the back
		        tail.Insert(0, tail.Last());
		        tail.RemoveAt(tail.Count-1);
		    }
	    }	
    }

    public IEnumerator DoGameOver()
    {
	    yield return new WaitForSeconds(1.5f);

        /*.completed += e =>
	    {
	        SceneManager.LoadScene("Snake", LoadSceneMode.Additive);
	    };*/
    }

    void OnTriggerEnter2D(Collider2D coll) {
    // Food?
	    if (coll.name.StartsWith("FoodPrefab")) {
			// Make collision sound
			GetComponent<AudioSource>().Play();
			// Get longer in next Move call
			ate = true;
			
			// Remove the Food
			Destroy(coll.gameObject);
	    }
	    // Collided with Tail or Border
	    else {
	        // ToDo 'You lose' screen
	        isDied = true;
			// SceneManager.LoadScene("GameOver");


			SceneManager.UnloadSceneAsync("Snake").completed += e =>
			{
	    			SceneManager.LoadScene("Snake", LoadSceneMode.Additive);
			};

			// StartCoroutine("DoGameOver");

		}
	}
	public void changeDir(string but){
		if (but =="up")
			dir = Vector2.up;
		else if (but == "down")
			dir = Vector2.down;
		else if (but =="right")
			dir = Vector2.right;
		else if (but =="left")
			dir = Vector2.left;
	}
}
