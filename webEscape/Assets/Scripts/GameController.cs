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

	void Start()
	{
		ballCount = 6;
		countText.text = "Balls Left: " + ballCount.ToString();
	}

	void Update()
	{
		if(spiderCount == 0)
		{
			//game won
			print("win");
		}
		if(spiderCount > 0 && ballCount < 1 && balls[0].transform.position.y < -8 )
		{
			//restart
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
	
   
}
