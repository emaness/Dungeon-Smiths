using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameObject character;
    //public GameObject BlockController;
    public Text GameOver;
    public Button TryAgain;

    void Start()
	{
        GameOver.gameObject.SetActive(false);
        TryAgain.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Hit block");
        if (collider.gameObject.CompareTag("Rock"))
        {
            Debug.Log("with tag Rock");
            DoLose();
            

        }

    }

    public void DoLose()
    {
        Destroy(character);
        GameOver.gameObject.SetActive(true);
        TryAgain.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
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
