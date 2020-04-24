using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class knightGameController : MonoBehaviour
{

	public int bloodCount = 5;
	[SerializeField] public Text YouWin;
	public Text instructions;
	[SerializeField] public GameObject Sponge;
	[SerializeField] public GameObject bloodStain;
	public int hitsOnStain = 0;
	public int randomWashNumber = 5;
	private Vector3 touchPosition;
	private Vector3 direction;
	private Rigidbody2D rb;
	private float moveSpeed = 10f;
	// Start is called before the first frame update
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
		//PlayerPrefs.SetInt("isFirstTime", 1);
		YouWin.gameObject.SetActive(false);
		instructions.gameObject.SetActive(false);
		rb = Sponge.GetComponent<Rigidbody2D>();
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
		YouWin.gameObject.SetActive(true);

		yield return new WaitForSeconds(2.5f);

		SceneManager.UnloadSceneAsync("KnightScene");

		Scene mt = SceneManager.GetActiveScene();
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
		if (bloodCount == 0)
		{
			//game won
			Destroy(bloodStain.gameObject);
			StartCoroutine("DoWin");
		}

		if ((Input.GetMouseButton(0) ) && bloodCount > 0)
		{
			//Touch touch = Input.GetTouch(0);
			touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); //touch.position);
			touchPosition.z = -2;
			direction = (touchPosition - Sponge.transform.position);
			rb.velocity = new Vector2(direction.x, direction.y)* moveSpeed;
			
			//now if colides with red stain and has done this 5 times - subtract one from stain and move stain randomly somewhere else
			
			if(hitsOnStain >= randomWashNumber)
			{
				//move red dot
				Vector3 rndPosWithin;
				rndPosWithin = new Vector3(Random.Range(-1.6f, 1.6f), Random.Range(-1.6f, 3.4f), -2);
				//rndPosWithin = transform.TransformPoint(rndPosWithin * .5f);
				bloodStain.transform.position = rndPosWithin;
				hitsOnStain = 0;
				randomWashNumber = Random.Range(2, 4);
				bloodCount--;
			}
			
		}
		else
		{
			rb.velocity = Vector2.zero;
		}
		
	}
}
