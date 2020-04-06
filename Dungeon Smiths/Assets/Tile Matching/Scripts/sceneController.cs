using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class sceneController : MonoBehaviour
{

	public const int gridRows = 3;
	public const int gridCols = 6;

	public const float offsetX = 3;
	public const float offsetY = 3;

	[SerializeField] public Card originalCard;
	[SerializeField] public Sprite[] images;
	[SerializeField] public Text YouWin;
	public Text instructions;
	private int numCards = 9;

	private IEnumerator DoInstruction()
	{
		float pauseEndTime = Time.realtimeSinceStartup + 3;
		while (Time.realtimeSinceStartup < pauseEndTime)
		{
			yield return 0;
		}
		Time.timeScale = 1f;
		instructions.gameObject.SetActive(false);
	}

	private void Start()
	{
		YouWin.gameObject.SetActive(false);
		instructions.gameObject.SetActive(false);

		Vector3 startPos = originalCard.transform.position;

		int[] numbers = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7,8,8 };
		numbers = ShuffleArray(numbers);

		for(int i = 0; i < gridCols; i++)
		{
			Card card;
			for(int j = 0; j < gridRows; j++)
			{
				if(i == 0 && j == 0)
				{
					card = originalCard;
				}
				else
				{
					card = Instantiate(originalCard) as Card;
				}

				int index = j * gridCols + i;
				int id = numbers[index];

				card.changeSprite(id, images[id]);

				float x = offsetX * i + startPos.x;
				float y = offsetY * j + startPos.y;

				card.transform.position = new Vector3(x, y, startPos.z);

			}
		}

		if (PlayerPrefs.GetInt("isFirstTime") == 1)
		{
			print("entered isfirst time");
			instructions.gameObject.SetActive(true);
			Time.timeScale = 0.0f;
			StartCoroutine("DoInstruction");
			PlayerPrefs.SetInt("isFirstTime", 0);

		}
	}

	private int[] ShuffleArray(int[] arr)
	{
		int[] newArr = arr.Clone() as int[];
		for(int i = 0; i < newArr.Length; i++)
		{
			int temp = newArr[i];
			int r = Random.Range(i, newArr.Length);
			newArr[i] = newArr[r];
			newArr[r] = temp;
		}
		return newArr;
	}



	private Card firstCard;
	private Card secondCard;

	public bool canReveal()
	{
		return (secondCard == null);
	}

	public void cardReveal(Card c)
	{
		if(firstCard == null)
		{
			firstCard = c;
		}
		else
		{
			secondCard = c;
			StartCoroutine(checkMatch());
		}
	}
	private IEnumerator DoWin()
	{
		yield return new WaitForSeconds(2.5f);

		SceneManager.UnloadSceneAsync("TileMatchingGame");

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


	private IEnumerator checkMatch()
	{
		if(firstCard.id == secondCard.id)
		{
			yield return new WaitForSeconds(.5f);
			GetComponent<AudioSource>().Play();
			Destroy(firstCard.gameObject);
			Destroy(secondCard.gameObject);
			numCards--;
			if(numCards == 0)
			{
				//end game
				YouWin.gameObject.SetActive(true);
				StartCoroutine("DoWin");

			}


		}
		else
		{
			yield return new WaitForSeconds(.5f);
			firstCard.unReveal();
			secondCard.unReveal();
		}
		firstCard = null;
		secondCard = null;
	}

}
