using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public GameObject character;
    //public GameObject BlockController;
    public Text GameOver;

    public Text Win;
	public Text instructions;
    private bool Losing;
    private float TimeElapsed;

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
        Losing = false;
        GameOver.gameObject.SetActive(false);
		instructions.gameObject.SetActive(false);

        Win.gameObject.SetActive(false);
        TimeElapsed = 0.0f;
		if (PlayerPrefs.GetInt("isFirstTime") == 1)
		{
			print("entered isfirst time");
			instructions.gameObject.SetActive(true);
			Time.timeScale = 0.0f;
			StartCoroutine("DoInstruction");
			PlayerPrefs.SetInt("isFirstTime", 0);
		}
	}

    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D collider)
    { 
        if (collider.gameObject.CompareTag("Rock"))
        {
            
            DoLose();
        }
    }

    public void DoLose()
    {
        Losing = true;
        TimeElapsed = 7.0f;
        /*
        Destroy(character);

        SceneManager.UnloadSceneAsync("CaveIn").completed += e =>
        {
            SceneManager.LoadScene("CaveIn", LoadSceneMode.Additive);
        };
        */
    }

    // Update is called once per frame
    void Update()
    {

        if(Losing)
        {
            Destroy(character);
            GameOver.gameObject.SetActive(true);
            SceneManager.LoadScene("CaveIn");
            return;
            //Debug.Log(TimeElapsed);
        }

        TimeElapsed += Time.deltaTime;
        if(TimeElapsed >= 9.0f)
		{

            Win.gameObject.SetActive(true);

            
            SceneManager.LoadScene("Level 2");
            
            // SceneManager.UnloadSceneAsync("Level1");
            // SceneManager.UnloadSceneAsync("CaveIn");
            return;
        }

        if (Input.mousePresent || Input.touchSupported)
        {
            bool down;
            float posx;
            if (Input.mousePresent)
            {
                down = Input.GetMouseButton(0);
                posx = Input.mousePosition.x;
            }
            else
            {
                down = Input.touchCount > 0;
                posx = Input.touches[0].position.x;
            }

            if (down)
            {
                // get the touch position from the screen touch to world point
                Vector3 touchedPos = Camera.main.ScreenToWorldPoint(new Vector3(posx, 0, 0));
                // lerp and set the position of the current object to that of the touch, but smoothly over time.
                character.transform.position = new Vector3(Vector3.Lerp(character.transform.position, touchedPos, Time.deltaTime*5).x, character.transform.position.y, character.transform.position.z);
       
            }
        }
    }
}
