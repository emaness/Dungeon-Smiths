using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public int index;
    public Transform[] ways1;// position of path
    public float speed = 5.0f;

    private float time;
    private bool isMove = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject != null)
        {
            time += Time.deltaTime;

            if (time >= 20.0f)
            {
                isMove = false;
               // Destroy(gameObject);
               
            }
        }

        if (isMove)
        {
            MoveToway1();
        }

       
    }
    private void MoveToway1()
    {
        if (index > ways1.Length - 1)
        {
           return;
        }

        if (gameObject != null)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, ways1[index].localPosition, speed * Time.deltaTime);
            if (Vector2.Distance(ways1[index].localPosition, transform.localPosition) < 0.01f)
            {
                index++;


            }

            if (index < ways1.Length - 1)
            {
                //transform.LookAt(ways1[index].localPosition);
            }
        }
    }
}
