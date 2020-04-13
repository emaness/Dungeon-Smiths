using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpKingCamera : MonoBehaviour
{
    public GameObject player;
    public GameObject textCanvas;

    // Start is called before the first frame update
    void Start()
    {
        float width = 16f;
        float height = 9f;
        Camera.main.aspect = width / height;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y < 11)
        {
            Vector3 camMove = transform.position;
            camMove.y = 0.1f;
            transform.position = camMove;
            textCanvas.SetActive(true);
        }
        else if ((player.transform.position.y >= 11) && (player.transform.position.y < 32.5))
        {
            Vector3 camMove = transform.position;
            camMove.y = 21.6f;
            transform.position = camMove;
            textCanvas.SetActive(false);
        }
        else if (player.transform.position.y >= 32.5)
        {
            Vector3 camMove = transform.position;
            camMove.y = 43.1f;
            transform.position = camMove;
            textCanvas.SetActive(false);
        }
    }
}
