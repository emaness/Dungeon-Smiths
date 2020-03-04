using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonWeakPointController : MonoBehaviour
{
    private float t;

    private void Start()
    {
        t = 0.0f;
    }

    void Update()
    {
        // fade effect
        float opacity = 0.5f + 0.5f * ((Mathf.Cos(t * 10.0f) + 1.0f) / 2.0f);
        var sr = GetComponent<SpriteRenderer>();
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, opacity);
        t += Time.deltaTime;

        // face the camera
        transform.LookAt(transform.position - new Vector3(0, 0, 1));
    }
}
