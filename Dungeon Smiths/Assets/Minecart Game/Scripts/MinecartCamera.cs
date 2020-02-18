using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinecartCamera : MonoBehaviour
{
    public GameObject minecart;

    // Start is called before the first frame update
    void Start()
    {
        //Locking camera on the minecart
        transform.position = minecart.transform.position;
        Vector3 pos = transform.position;
        pos.z = -30;
        transform.position = pos;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = minecart.transform.position;
        Vector3 pos = transform.position;
        pos.z = -30;
        transform.position = pos;
    }
}
