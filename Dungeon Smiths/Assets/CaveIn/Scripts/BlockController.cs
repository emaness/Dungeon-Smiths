using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BlockController : MonoBehaviour
{

    private const int maxBlocks = 40;
    private const float BlockInterval = 0.15f;

    private int blocksFallen;
    public GameObject Block;
    private float blockTimer;

    public GameObject character;
    


    // Start is called before the first frame update
    void Start()
    {    
        blocksFallen = 0;
        blockTimer = BlockInterval;
        SpawnBlock();

    }

    /*
    public IEnumerator advance()
    {
        yield return new WaitForSeconds(2.5f);
        
        SceneManager.UnloadSceneAsync("CaveIn");

        Scene mt = SceneManager.GetSceneByName("Level1");

        foreach (GameObject obj in mt.GetRootGameObjects())
        {
            if (obj.name != "PauseMenu")
            {
                obj.SetActive(true);
            }
        }
    }*/
    

    void SpawnBlock()
	{
        Vector3 spawnPos = new Vector3(Random.Range(-8.0f, 8.0f), 5.0f, 0.0f);
        GameObject newBlock = Instantiate(Block, spawnPos, Quaternion.Euler(0,0,90), gameObject.transform);
        var rb = newBlock.GetComponent<Rigidbody2D>();
        Vector3 vel = new Vector3(0, -15, 0);
        rb.velocity = vel;
        //Debug.Log(this.blocksFallen);
    }



    // Update is called once per frame
    void Update()
    {
        blockTimer -= Time.deltaTime;
        if(blockTimer < 0.0f)
		{
            if (this.blocksFallen < maxBlocks)
            {
                SpawnBlock();
                blocksFallen++;
                blockTimer = BlockInterval;

            }
            
        }

        //if (Input.touchCount > 0)
        //{
        //    Touch touch = Input.GetTouch(0); // get first touch since touch count is greater than zero

        //    if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
        //    {
        //        // get the touch position from the screen touch to world point
        //        Vector3 touchedPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 0));
        //        // lerp and set the position of the current object to that of the touch, but smoothly over time.
        //        transform.position = Vector3.Lerp(transform.position, touchedPos, Time.deltaTime);
        //    }
        //}
    }
}
