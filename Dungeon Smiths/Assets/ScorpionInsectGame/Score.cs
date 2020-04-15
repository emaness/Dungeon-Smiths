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

    public IEnumerator DoWin()
    {
		var canvas = GameObject.Find("Canvas");
		canvas.transform.Find("WinText").gameObject.SetActive(true);
        GameObject.Find("BombSpawner").gameObject.SetActive(false);
		GameObject.Find("ScorpionSpawner").gameObject.SetActive(false);
		GameObject.Find("Sword").gameObject.SetActive(false);

		yield return new WaitForSeconds(2.0f);

		SceneManager.UnloadSceneAsync("ScorpionInsect");
		Scene mt = SceneManager.GetActiveScene();
		foreach (GameObject obj in mt.GetRootGameObjects())
        {
			obj.SetActive(true);
        }

		yield return null;
    }

	public void incrementScore(int incrementValue){
		this.score += incrementValue;
		GetComponent<Text>().text = "Score: " + this.score;
		if (score >= goal){
			StartCoroutine("DoWin");
		}
	}

	public int getScore() {
		return this.score;
	}
}
