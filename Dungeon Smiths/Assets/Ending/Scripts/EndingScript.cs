using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingScript : MonoBehaviour
{
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
        
    }
}
