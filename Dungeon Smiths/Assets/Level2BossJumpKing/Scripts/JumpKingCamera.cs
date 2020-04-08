﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpKingCamera : MonoBehaviour
{
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y < 11)
        {
            Vector3 camMove = transform.position;
            camMove.y = 0.1f;
            transform.position = camMove;
        }
        else if ((player.transform.position.y >= 11) && (player.transform.position.y < 32.5))
        {
            Vector3 camMove = transform.position;
            camMove.y = 21.6f;
            transform.position = camMove;
        }
        else if ((player.transform.position.y >= 32.5) && (player.transform.position.y < 53.5))
        {
            Vector3 camMove = transform.position;
            camMove.y = 43.1f;
            transform.position = camMove;
        }
        else if ((player.transform.position.y >= 53.5) && (player.transform.position.y < 75.5))
        {
            Vector3 camMove = transform.position;
            camMove.y = 64.6f;
            transform.position = camMove;
        }
        else if (player.transform.position.y >= 75.5)
        {
            Vector3 camMove = transform.position;
            camMove.y = 86.1f;
            transform.position = camMove;
        }
    }
}
