using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
	[SerializeField] public GameObject cardBack;
	[SerializeField] public sceneController controller;

	public void OnMouseDown()
	{
		if (cardBack.activeSelf && controller.canReveal())
		{
			cardBack.SetActive(false);
			controller.cardReveal(this);
		}
	}

	private int _id;
	public int id
	{
		get { return _id; }
	}

	public void changeSprite(int id, Sprite img)
	{
		_id = id;
		GetComponent<SpriteRenderer>().sprite = img;
	}

	public void unReveal()
	{
		cardBack.SetActive(true);
	}
}
