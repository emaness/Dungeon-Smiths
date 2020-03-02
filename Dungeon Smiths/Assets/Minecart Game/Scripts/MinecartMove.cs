using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinecartMove : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D m_Rigidbody;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 vec = new Vector2(100.0f, -98.1f);
        m_Rigidbody.AddRelativeForce(vec, ForceMode2D.Force);
    }
}
