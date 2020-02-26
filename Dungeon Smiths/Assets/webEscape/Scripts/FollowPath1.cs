using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath1 : MonoBehaviour
{
	[SerializeField] public Transform[] waypoints;
	
	[SerializeField] public float moveSpeed = 2f;
	
	private int waypointIndex = 0;
	
	private void Start()
	{
		
		this.transform.position = waypoints[waypointIndex].transform.position;
	}
	
	private void Update()
	{
		
		Move();

	}
	
	private void Move()
	{			

		if(waypointIndex >= waypoints.Length)
		{
			waypointIndex = 0;
		}
		this.transform.position = Vector2.MoveTowards(transform.position,
		   waypoints[waypointIndex].transform.position,
		   moveSpeed * Time.deltaTime);
		 Vector3 offset = (this.transform.position - waypoints[waypointIndex].transform.position);
		if ( offset.sqrMagnitude <= .1)
		{
			waypointIndex += 1;
			if(waypointIndex == waypoints.Length){
				waypointIndex = 0;
			}
		}
	}
}
