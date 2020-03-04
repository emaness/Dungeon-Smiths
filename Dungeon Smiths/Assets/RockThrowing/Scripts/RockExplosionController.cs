using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockExplosionController : MonoBehaviour
{
    private void Start()
    {
        var ps = GetComponent<ParticleSystem>();
        ps.Play();
    }

    private void Update()
    {
        var ps = GetComponent<ParticleSystem>();
        if(ps.isStopped)
        {
            Destroy(gameObject);
        }
    }
}
