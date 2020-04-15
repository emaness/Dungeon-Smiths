using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Lives : MonoBehaviour{

	private int lives =3;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Text>().text = "Lives: " + this.lives;
    }

    public void decreaseLives(int decreaseValue){
    	this.lives -= decreaseValue;
    	GetComponent<Text>().text = "Lives: " + this.lives;
  		if (this.lives ==0){
  			SceneManager.LoadScene("Lose");
  		}
    }
    // Update is called once per frame
    public int getLives(){
    	return this.lives;
    }
}
