using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class knightGameController : MonoBehaviour
{

	public int bloodCount = 5;
	[SerializeField] public Text YouWin;
	[SerializeField] public GameObject Sponge;
	[SerializeField] public GameObject bloodStain;
	public int hitsOnStain = 0;
	public int randomWashNumber = 5;
	private Vector3 touchPosition;
	private Vector3 direction;
	private Rigidbody2D rb;
	private float moveSpeed = 10f;
	// Start is called before the first frame update
	void Start()
    {
		YouWin.gameObject.SetActive(false);
		rb = Sponge.GetComponent<Rigidbody2D>();

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

		if (Input.touchCount > 0  && bloodCount > 0)
		{
			Touch touch = Input.GetTouch(0);
			touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
			touchPosition.z = -2;
			direction = (touchPosition - Sponge.transform.position);
			rb.velocity = new Vector2(direction.x, direction.y)* moveSpeed;
			if(touch.phase == TouchPhase.Ended)
			{
				rb.velocity = Vector2.zero;
			}
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
