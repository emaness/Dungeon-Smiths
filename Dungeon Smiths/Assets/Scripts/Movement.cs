using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public GameObject camera;
    private Rigidbody rigid;
    private float moveSpeed = 10.0f;
    private float camSpeed = 100.0f;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rigid.constraints = RigidbodyConstraints.FreezeAll;
        if (Input.GetKey("w"))
        {
            transform.Translate(0, 0, -moveSpeed * Time.deltaTime);
            rigid.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
        }
        if (Input.GetKey("a"))
        {
            transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
            transform.Rotate(0, -camSpeed * Time.deltaTime, 0);
            camera.transform.Rotate(0, -camSpeed * Time.deltaTime, 0);
            rigid.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
        }
        if (Input.GetKey("s"))
        {
            transform.Translate(0, 0, moveSpeed * Time.deltaTime);
            rigid.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
        }
        if (Input.GetKey("d"))
        {
            transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
            transform.Rotate(0, camSpeed * Time.deltaTime, 0);
            camera.transform.Rotate(0, camSpeed * Time.deltaTime, 0);
            rigid.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
        }
    }
}
