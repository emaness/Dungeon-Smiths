using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;


public class Snake : MonoBehaviour{
	Vector2 dir = Vector2.right;
	List<Transform> tail = new List<Transform>();
	bool ate = false;
	bool isDied = false;
	int tailLength = 10;
	public GameObject tailPrefab;
    // Start is called before the first frame update
    void Start(){
    	for(int i = tailLength; i>0; i--){
			Vector2 v = transform.position;
			v.x = v.x - i;
			GameObject g =(GameObject)Instantiate(tailPrefab,
		                                              v,
		                                              Quaternion.identity);
			tail.Insert(0, g.transform);
		}
        InvokeRepeating("Move", 0.1f, 0.1f);
    }

    // Update is called once per frame
    void Update(){
    	if (!isDied){
    		/*
		    if (Input.GetKey(KeyCode.RightArrow))
		        dir = Vector2.right;
		    else if (Input.GetKey(KeyCode.DownArrow))
		        dir = -Vector2.up;    // '-up' means 'down'
		    else if (Input.GetKey(KeyCode.LeftArrow))
		        dir = -Vector2.right; // '-right' means 'left'
		    else if (Input.GetKey(KeyCode.UpArrow))
		        dir = Vector2.up;*/
		    if (CrossPlatformInputManager.GetAxis("Horizontal") != 0){
		    	dir = Vector2.right * CrossPlatformInputManager.GetAxis("Horizontal");
		    }
		    else if (CrossPlatformInputManager.GetAxis("Vertical") != 0){
		    	dir = Vector2.up * CrossPlatformInputManager.GetAxis("Vertical");
		    }
	    }
    }
    void Move(){
    	Vector2 v = transform.position;
		transform.Translate(dir);
		if (!isDied){
			if (ate) {
		        // Load Prefab into the world
		        //GameObject g =(GameObject)Instantiate(tailPrefab,
		        //                                      v,
		        //                                      Quaternion.identity);

		        // Keep track of it in our tail list
		        //tail.Insert(0, g.transform);
		        if(tail.Count >1){
					Destroy(tail[tail.Count-1].gameObject);
					tail.RemoveAt(tail.Count-1);
					tail.Last().position = v;
					tail.Insert(0, tail.Last());
					tail.RemoveAt(tail.Count-1);
			        // Reset the flag
			        ate = false;
		    	}
		    	else{
		    		Destroy(tail[tail.Count-1].gameObject);
		    		SceneManager.LoadScene("Win");
		    	}
		    }
		    else if (tail.Count > 0){
		        // Move last Tail Element to where the Head was
		        tail.Last().position = v;

		        // Add to front of list, remove from the back
		        tail.Insert(0, tail.Last());
		        tail.RemoveAt(tail.Count-1);
		    }
	    }	
    }
    void OnTriggerEnter2D(Collider2D coll) {
    // Food?
	    if (coll.name.StartsWith("FoodPrefab")) {
	        // Get longer in next Move call
	        ate = true;
	       
	        // Remove the Food
	        Destroy(coll.gameObject);
	    }
	    // Collided with Tail or Border
	    else {
	        // ToDo 'You lose' screen
	        isDied = true;
	        SceneManager.LoadScene("GameOver");
	    }
	}
}
