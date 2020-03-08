using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinecartMove : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D m_Rigidbody;
    private bool inAir = false;
    private float countdown = 1.0f;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.localEulerAngles = new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, Mathf.Clamp(transform.rotation.eulerAngles.z, -40, 40));

        if (inAir && countdown > 0.0f)
        {
            countdown -= Time.deltaTime;
        }
        else
        {
            countdown = 1.0f;
            inAir = false;
        }

        if ((Input.GetMouseButton(0) || Input.touchCount > 0) && !inAir)
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up);
        //print(hit.normal);
        //https://answers.unity.com/questions/27340/rotating-an-object-to-equal-normals-of-object-belo.html
        if (hit.collider != null)
        {
            Vector2 vec = new Vector2(5.0f, -9.81f);
            m_Rigidbody.AddRelativeForce(vec, ForceMode2D.Impulse);
        }
    }

    void Jump()
    {
        Vector2 vec = new Vector2(0.0f, 100.0f);
        m_Rigidbody.AddRelativeForce(vec, ForceMode2D.Impulse);
        inAir = true;
    }
}
