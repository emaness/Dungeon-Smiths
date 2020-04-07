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
	[SerializeField] public Text instructions;

	private IEnumerator DoInstruction()
	{
		float pauseEndTime = Time.realtimeSinceStartup + 5;
		while (Time.realtimeSinceStartup < pauseEndTime)
		{
			yield return 0;
		}
		Time.timeScale = 1f;
		instructions.gameObject.SetActive(false);
	}

	void Start()
	{
		ballCount = 6;
		YouWin.gameObject.SetActive(false);
		TryAgain.gameObject.SetActive(false);
		instructions.gameObject.SetActive(false);
		countText.text = "Balls Left: " + ballCount.ToString();

		if (PlayerPrefs.GetInt("isFirstTime") == 1)
		{
			print("entered isfirst time");
			instructions.gameObject.SetActive(true);
			Time.timeScale = 0.0f;
			StartCoroutine("DoInstruction");
			PlayerPrefs.SetInt("isFirstTime", 0);
		}
	}

	private IEnumerator DoWin()
	{
		yield return new WaitForSeconds(2.5f);

		SceneManager.UnloadSceneAsync("SpiderScene");

		Scene mt = SceneManager.GetActiveScene();
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
		SceneManager.UnloadSceneAsync("spiderScene").completed += e =>
		{
			SceneManager.LoadScene("spiderScene", LoadSceneMode.Additive);
		};
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
			if(Camera.main.ScreenToWorldPoint(Input.mousePosition).y > 5.3)
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
}
