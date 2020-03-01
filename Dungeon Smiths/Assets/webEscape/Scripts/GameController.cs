using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{


	public int spiderCount = 6;
	[SerializeField] public ballDrop[] balls;
	public int ballCount = 6;
	[SerializeField] public Text countText;
	[SerializeField] public Text YouWin;
	[SerializeField] public Text TryAgain;
	void Start()
	{
		ballCount = 6;
		YouWin.gameObject.SetActive(false);
		TryAgain.gameObject.SetActive(false);
		countText.text = "Balls Left: " + ballCount.ToString();
	}

	private IEnumerator DoWin()
	{
		yield return new WaitForSeconds(2.5f);

		SceneManager.UnloadSceneAsync("SpiderScene");

		Scene mt = SceneManager.GetSceneByName("Level1");
		foreach (GameObject obj in mt.GetRootGameObjects())
		{
			if (obj.name != "PauseMenu")
			{
				obj.SetActive(true);
			}
		}
	}

	private IEnumerator DoRestart()
	{
		yield return new WaitForSeconds(2.5f);
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
	void Update()
	{
		if(spiderCount == 0)
		{
			//game won
			YouWin.gameObject.SetActive(true);
			StartCoroutine("DoWin");
		}
		if(spiderCount > 0 && ballCount < 1 && balls[0].transform.position.y < -8 )
		{
			//restart
			TryAgain.gameObject.SetActive(true);
			StartCoroutine("DoRestart");

		}

		if (Input.GetMouseButtonDown(0) && ballCount > 0)
		{
			if(Camera.main.ScreenToWorldPoint(Input.mousePosition).y > 6)
			{
				balls[ballCount - 1].transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				Vector3 pos = (balls[ballCount - 1].transform.position);
				pos.z = 0;
				balls[ballCount - 1].transform.position = pos;
				balls[ballCount - 1].GetComponent<Rigidbody2D>().gravityScale = 1f;
				ballCount--;
				countText.text = "Balls Left: " + ballCount.ToString();
			}
		}
	}


	void spiderLost()
	{
		spiderCount--;

	}

	void ballTossed()
	{
		ballCount--;
	}

    public void BackToDungeon()
    {
		SceneManager.UnloadSceneAsync("spiderScene");

		Scene mt = SceneManager.GetSceneByName("Level1");
		foreach (GameObject obj in mt.GetRootGameObjects())
		{
			obj.SetActive(true);
		}
	}
   
}
