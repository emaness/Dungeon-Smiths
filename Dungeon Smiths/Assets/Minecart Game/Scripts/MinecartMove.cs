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
        m_Rigidbody.velocity = new Vector2(100.0f, 0.0f);
        //Vector2 vec = new Vector2(1.0f, 0.0f);
        //m_Rigidbody.(vec * 100, ForceMode2D.Force);
        //Vector3 pos = transform.position;
        //pos.x += 2;
        //transform.position = pos;
    }
}
