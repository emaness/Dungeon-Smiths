using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockExplosionController : MonoBehaviour
{
    private void Start()
    {
        var ps = GetComponent<ParticleSystem>();
        ps.Play();

        GetComponent<AudioSource>().volume = Random.Range(0.6f, .8f);
        GetComponent<AudioSource>().pitch = Random.Range(0.9f, 1.0f);
        GetComponent<AudioSource>().Play();
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
