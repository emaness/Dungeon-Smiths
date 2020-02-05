using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public GameObject cam;
    public Joystick moveStick;
    public Joystick camStick;
    private Rigidbody rigid;
    //private float moveSpeed = 10.0f;
    //private float camSpeed = 100.0f;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rigid.constraints = RigidbodyConstraints.FreezeAll;
        /*
        if (Input.GetKey("w"))
        {
            transform.Translate(0, 0, -moveSpeed * Time.deltaTime);
            rigid.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
        }
        if (Input.GetKey("a"))
        {
            transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
            transform.Rotate(0, -camSpeed * Time.deltaTime, 0);
            cam.transform.Rotate(0, -camSpeed * Time.deltaTime, 0);
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
            cam.transform.Rotate(0, camSpeed * Time.deltaTime, 0);
            rigid.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
        }*/

        if (moveStick.Horizontal != 0 || moveStick.Vertical != 0)
        {
            transform.Translate(-moveStick.Horizontal, 0, -moveStick.Vertical);
            rigid.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
        }
        if (camStick.Horizontal != 0 || camStick.Vertical != 0)
        {
            transform.Rotate(0, camStick.Horizontal, 0);
            cam.transform.Rotate(0, camStick.Horizontal, 0);
            rigid.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
        }
    }
}
