using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextChange : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DisableText", 5f);//invoke after 5 seconds
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DisableText()
    {
        gameObject.SetActive(false);
    }
}
