using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutBomb : MonoBehaviour
{
	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.tag == "Cut") {
			Destroy (this.gameObject);
			GameObject lifeText = GameObject.Find ("LifeText");
 			lifeText.GetComponent<Lives> ().decreaseLives(1);
		}
	}
}
