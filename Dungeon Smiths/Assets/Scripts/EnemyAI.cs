using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
	public float range = 6;
	public GameObject player;
	public float speed = 0.01f;

    private int idleHash = Animator.StringToHash("Idle");
    private int walkingHash = Animator.StringToHash("Walking");

    public IEnumerator Idle(){
        yield return new WaitForSeconds(0.1f);

        var anim = GetComponent<Animator>();
        anim.SetTrigger(idleHash);
    }
    // Start is called before the first frame update
    void Start(){
        StartCoroutine("Idle");
		InvokeRepeating("AI", 0.1f, 0.1f);

    }

    // Update is called once per frame

    void AI(){
    	float step =  speed * Time.deltaTime;
    	float distance = Vector3.Distance(transform.position, player.transform.position);
    	Vector3 targetDirection = player.transform.position - transform.position;
    	Vector3 target = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        var anim = gameObject.GetComponent<Animator>();
    	if (distance < range){
    		Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, step, 0.0f);
    		Debug.DrawRay(transform.position, newDirection, Color.red);
            anim.SetTrigger(walkingHash);
    		transform.rotation = Quaternion.LookRotation(newDirection);
    		transform.position = Vector3.MoveTowards(transform.position, target, step);
    	}
    }
}
