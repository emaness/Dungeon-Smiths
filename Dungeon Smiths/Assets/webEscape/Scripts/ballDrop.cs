using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballDrop : MonoBehaviour
{
	[SerializeField] public GameController con;
	[SerializeField] public Sprite img;

	// Start is called before the first frame update
	void Start()
    {
		this.GetComponent<Rigidbody2D>().gravityScale = 0f;
		this.GetComponent<SpriteRenderer>().sprite = img;

	}

   

	void OnCollisionEnter2D(Collision2D col)
	{
		if(col.gameObject.tag == "Spider")
		{
			GetComponent<AudioSource>().Play();
			Destroy(col.gameObject);
			con.spiderCount--;
		}


	}
}
