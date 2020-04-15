using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Score : MonoBehaviour {
 
	private int score = 0;
	public int goal;

	// Use this for initialization
	void Start () {
		GetComponent<Text>().text = "Score: " + this.score;
	}

	public void incrementScore(int incrementValue){
		this.score += incrementValue;
		GetComponent<Text>().text = "Score: " + this.score;
		if (score >= goal){
			SceneManager.LoadScene("Win");
		}
	}

	public int getScore() {
		return this.score;
	}
}
