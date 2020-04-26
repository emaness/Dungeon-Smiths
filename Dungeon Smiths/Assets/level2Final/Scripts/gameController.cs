using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameController : MonoBehaviour
{
	[SerializeField] public GameObject[] images;
	[SerializeField] public Text YouWin;
	[SerializeField] public Text TryAgain;
	public Text instructions;
	private int[] numbersOne = { 0, 1, 2, 3, };
	private int[] numbersTwo = { 0, 1, 3, 2, 0 };
	private int[] numbersThree = { 2, 4, 3, 1, 0, 3, 0};
	private int counter;
	private bool isOne;
	private bool isTwo;
	private bool isThree;
	private bool canPress;

	private IEnumerator DoInstruction()
	{
		float pauseEndTime = Time.realtimeSinceStartup + 8;
		while (Time.realtimeSinceStartup < pauseEndTime)
		{
			yield return 0;
		}
		Time.timeScale = 1f;
		instructions.gameObject.SetActive(false);
		startGame();

	}
	// Start is called before the first frame update
	void Start()
    {

		YouWin.gameObject.SetActive(false);
		TryAgain.gameObject.SetActive(false);
		numbersOne = ShuffleArray(numbersOne);
		numbersTwo = ShuffleArray(numbersTwo);
		numbersThree = ShuffleArray(numbersThree);

		counter = 0;
		isOne = true;
		isTwo = false;
		isThree = false;
		canPress = false;
		instructions.gameObject.SetActive(false);
		if (PlayerPrefs.GetInt("isFirstTime") == 1)
		{
			print("entered isfirst time");
			instructions.gameObject.SetActive(true);
			Time.timeScale = 0.0f;
			StartCoroutine("DoInstruction");
			PlayerPrefs.SetInt("isFirstTime", 0);
		}
		else { startGame(); }

		

	}
	void  startGame()
	{
		StartCoroutine("SetUpOne");
	}

	private IEnumerator DoRestart()
	{
		yield return new WaitForSeconds(2.0f);
		SceneManager.UnloadSceneAsync("final2Scene").completed += e =>
		{
			SceneManager.LoadScene("final2Scene", LoadSceneMode.Additive);
		};
	}

	private IEnumerator SetUpOne()
	{
		GameObject[] objects = new GameObject[numbersOne.Length]; 
		//create the sequence
		for(int i = 0; i < numbersOne.Length; i++)
		{
			GameObject first = Instantiate(images[numbersOne[i]]);
			float x = 2f *  i +  -7.5f ;
			first.transform.position = new Vector3(x, 3, 0);
			if (numbersOne[i] == 3)
			{
				first.transform.Rotate(0.0f, 180.0f, 0.0f, Space.World);
				first.transform.localScale += new Vector3(3.5f, 3.5f, 1);
				first.transform.position = new Vector3(x, 3.5f, 0);
			}
			else if (numbersOne[i] == 2) first.transform.localScale += new Vector3(3, 3, 1);
			else if(numbersOne[i] == 4) { first.transform.localScale += new Vector3(1.5f, 1.5f, 1); }
			//else { first.transform.localScale += new Vector3(1.1f, 1.1f, 1); }
			objects[i] = first;
			//print(first.transform.name);
			//print(numbersOne[i]);

		}
		yield return new WaitForSeconds(3.0f);
		//delete the sequence
		for(int i = 0; i < numbersOne.Length; i++)
		{
			Destroy(objects[i].gameObject);
		}
		canPress = true;
	}


	private IEnumerator SetUpTwo()
	{
		GameObject[] objects = new GameObject[numbersTwo.Length];
		//create the sequence
		for (int i = 0; i < numbersTwo.Length; i++)
		{
			GameObject first = Instantiate(images[numbersTwo[i]]);
			float x = 2f * i + -7.5f;
			first.transform.position = new Vector3(x, 3, 0);
			if (numbersTwo[i] == 3)
			{
				first.transform.Rotate(0.0f, 180.0f, 0.0f, Space.World);
				first.transform.localScale += new Vector3(3.5f, 3.5f, 1);
				first.transform.position = new Vector3(x, 3.5f, 0);
			}
			else if (numbersTwo[i] == 2) first.transform.localScale += new Vector3(3, 3, 1);
			else if (numbersTwo[i] == 4) { first.transform.localScale += new Vector3(1.5f, 1.5f, 1); }
			//else { first.transform.localScale += new Vector3(1.1f, 1.1f, 1); }
			objects[i] = first;


		}
		yield return new WaitForSeconds(5.0f);
		//delete the sequence
		for (int i = 0; i < numbersTwo.Length; i++)
		{
			Destroy(objects[i].gameObject);
		}
		canPress = true;
	}

	private IEnumerator SetUpThree()
	{
		GameObject[] objects = new GameObject[numbersThree.Length];
		//create the sequence
		for (int i = 0; i < numbersThree.Length; i++)
		{
			GameObject first = Instantiate(images[numbersThree[i]]);
			float x = 2.0f * i + -7.5f;
			first.transform.position = new Vector3(x, 3, 0);
			if (numbersThree[i] == 3)
			{
				first.transform.Rotate(0.0f, 180.0f, 0.0f, Space.World);
				first.transform.localScale += new Vector3(3.5f, 3.5f, 1);
				first.transform.position = new Vector3(x, 3.5f, 0);
			}
			else if (numbersThree[i] == 2) first.transform.localScale += new Vector3(3, 3, 1);
			else if (numbersThree[i] == 4) { first.transform.localScale += new Vector3(1.5f, 1.5f, 1); }
			//else { first.transform.localScale += new Vector3(1.1f, 1.1f, 1); }
			objects[i] = first;


		}
		yield return new WaitForSeconds(7.0f);
		//delete the sequence
		for (int i = 0; i < numbersThree.Length; i++)
		{
			Destroy(objects[i].gameObject);
		}
		canPress = true;
	}

	private IEnumerator DoWin()
	{
		yield return new WaitForSeconds(2.5f);

		SceneManager.UnloadSceneAsync("final2Scene");

		Scene mt = SceneManager.GetActiveScene();
		// Scene mt = SceneManager.GetSceneByName("Level1");
		foreach (GameObject obj in mt.GetRootGameObjects())
		{
			if (obj.name != "PauseMenu")
			{
				obj.SetActive(true);
			}
		}
	}
	// Update is called once per frame
	void Update()
    {
		if (Input.GetMouseButtonDown(0) && canPress)
		{
			//print("clicked");
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
			RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
			
			if (hit.collider != null)
			{
				bool correctHit = false;
				//print("got in here");
				if (hit.transform.name == "skull")
				{
					//print("skull"); index value == 3
					if (isOne)
					{
						if (numbersOne[counter] == 3) correctHit = true;
					}
					else if (isTwo)
					{
						if (numbersTwo[counter] == 3) correctHit = true;
					}
					else
					{
						if (numbersThree[counter] == 3) correctHit = true;
					}
				}
				if (hit.transform.name == "pot_A")
				{
					//print("pot"); index value == 4
					if (isOne)
					{
						if (numbersOne[counter] == 4) correctHit = true;
					}
					else if (isTwo)
					{
						if (numbersTwo[counter] == 4) correctHit = true;
					}
					else
					{
						if (numbersThree[counter] == 4) correctHit = true;
					}
				}
				if (hit.transform.name == "barrel_A")
				{
					if (isOne)
					{
						if (numbersOne[counter] == 1) correctHit = true;
					}
					else if (isTwo)
					{
						if (numbersTwo[counter] == 1) correctHit = true;
					}
					else
					{
						if (numbersThree[counter] == 1) correctHit = true;
					}
					//print("barrel"); index value == 1
				}
				if (hit.transform.name == "chest_A")
				{
					//print("chest"); index value == 0
					if (isOne)
					{
						if (numbersOne[counter] == 0) correctHit = true;
					}
					else if (isTwo)
					{
						if (numbersTwo[counter] == 0) correctHit = true;
					}
					else
					{
						if (numbersThree[counter] == 0) correctHit = true;
					}
				}
				if (hit.transform.name == "lantern_A")
				{
					//print("lantern"); index value == 2
					if (isOne)
					{
						if (numbersOne[counter] == 2) correctHit = true;
					}
					else if (isTwo)
					{
						if (numbersTwo[counter] == 2) correctHit = true;
					}
					else
					{
						if (numbersThree[counter] == 2) correctHit = true;
					}
				}
				//print(hit.transform.name);
				//print(numbersOne[counter]);
				//print(counter);
				counter++;
				if (!correctHit)
				{
					TryAgain.gameObject.SetActive(true);
					StartCoroutine("DoRestart");
				}
			}
			
		}
		if(isOne && counter == numbersOne.Length)
		{
			isOne = false;
			isTwo = true;
			counter = 0;
			canPress = false;
			//set up round two
			StartCoroutine("SetUpTwo");

		}

		if (isTwo && counter == numbersTwo.Length)
		{
			isThree = true;
			isTwo = false;
			counter = 0;
			canPress = false;
			//set up round three
			StartCoroutine("SetUpThree");
		}
		if(isThree && counter == numbersThree.Length)
		{
			//win
			YouWin.gameObject.SetActive(true);
			StartCoroutine("DoWin");
		}
	}

	

		private int[] ShuffleArray(int[] arr)
	{
		int[] newArr = arr.Clone() as int[];
		for (int i = 0; i < newArr.Length; i++)
		{
			int temp = newArr[i];
			int r = Random.Range(i, newArr.Length);
			newArr[i] = newArr[r];
			newArr[r] = temp;
		}
		return newArr;
	}
}
