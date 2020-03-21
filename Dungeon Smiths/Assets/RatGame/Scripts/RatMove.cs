using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatMove : MonoBehaviour
{
    public Vector2 coords1;
    public Vector2 coords2;
    Rigidbody m_Rigidbody;
    private Vector3 target;
    private bool reached = false;

    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        target = new Vector3(Random.Range(coords1.x, coords2.x), Random.Range(coords1.y, coords2.y), 1.4f);
    }

    // Update is called once per frame
    void Update()
    {
        float step = 25.0f * Time.deltaTime;
        if (reached)
        {
            target = new Vector3(Random.Range(coords1.x, coords2.x), Random.Range(coords1.y, coords2.y), 1.4f);
            reached = false;
        }
        else if (transform.position == target)
        {
            reached = true;
        }
        m_Rigidbody.position = Vector3.MoveTowards(m_Rigidbody.position, target, step);
    }
}
