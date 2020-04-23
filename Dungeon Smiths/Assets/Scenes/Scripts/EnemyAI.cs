using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float range = 10;
    public GameObject player;
    public float speed = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("AI", 0.05f, 0.05f);

    }

    void AI()
    {
        float step = speed * Time.deltaTime * 2;
        float distance = Vector3.Distance(transform.position, player.transform.position);
        Vector3 target = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        
        if (distance < range)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, step);
        }
    }
}
