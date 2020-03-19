using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    private FireState state;

    public FireState State
    {
        get
        {
            return state;
        }

        set
        {
            state = value;
            UpdateFromState();
        }
    }

    public void ApplyDamage(float damage)
    {
        state.Health -= damage;
        UpdateFromState();        
    }

    private void UpdateFromState()
    {
        if (state.Health <= 0.01f)
        {
            Destroy(gameObject);
        } else
        {
            float s = 0.2f + 0.8f * state.Health / 100.0f;
            gameObject.transform.localScale = new Vector3(s, s, 1);
        }
    }
}
