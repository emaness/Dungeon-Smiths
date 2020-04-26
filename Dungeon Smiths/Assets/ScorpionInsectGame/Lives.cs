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

    public IEnumerator DoLose()
    {
        var canvas = GameObject.Find("Canvas");
        canvas.transform.Find("LoseText").gameObject.SetActive(true);
        GameObject.Find("BombSpawner").SetActive(false);
        GameObject.Find("ScorpionSpawner").gameObject.SetActive(false);
        GameObject.Find("Sword").gameObject.SetActive(false);

        yield return new WaitForSeconds(2.0f);

        SceneManager.UnloadSceneAsync("ScorpionInsect").completed += e =>
        {
            SceneManager.LoadScene("ScorpionInsect", LoadSceneMode.Additive);
        };

        yield return null;
    }

    public void decreaseLives(int decreaseValue){
    	this.lives -= decreaseValue;
    	GetComponent<Text>().text = "Lives: " + this.lives;
  		if (this.lives ==0){
            StartCoroutine("DoLose");
  		}
    }
    // Update is called once per frame
    public int getLives(){
    	return this.lives;
    }
}
