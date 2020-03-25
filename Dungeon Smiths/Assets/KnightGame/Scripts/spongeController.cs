using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spongeController : MonoBehaviour
{
	[SerializeField] public knightGameController gc;
    // Start is called before the first frame update
    void Start()
    {
        
    }

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "blood")
		{
			gc.hitsOnStain++;
			GetComponent<AudioSource>().Play();
		}
	}
	// Update is called once per frame
	void Update()
    {
        
    }
}
