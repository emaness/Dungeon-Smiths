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
        //transform.localEulerAngles = new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, Mathf.Clamp(transform.rotation.eulerAngles.z, -40, 40));

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
        if (hit.collider != null)
        {
            float step = 130.0f * Time.deltaTime;
            Vector2 target = new Vector2(988.3f, -4.6f);
            m_Rigidbody.position = Vector2.MoveTowards(m_Rigidbody.position, target, step);
            
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.FromToRotation(Vector3.up, hit.normal), 5 * Time.deltaTime);

            //Vector2 vec = new Vector2(5.0f, -9.81f);
            //m_Rigidbody.AddRelativeForce(vec, ForceMode2D.Impulse);
        }
    }

    void Jump()
    {
        Vector2 vec = new Vector2(10.0f, 50.0f);
        m_Rigidbody.AddRelativeForce(vec, ForceMode2D.Impulse);
        inAir = true;
    }
}
