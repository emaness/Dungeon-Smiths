using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatMove : MonoBehaviour
{
    Rigidbody m_Rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float step = 25.0f * Time.deltaTime;
        Vector3 target = new Vector3(10.0f, 10.0f, 0.0f);
        m_Rigidbody.position = Vector3.MoveTowards(m_Rigidbody.position, target, step);
    }
}
