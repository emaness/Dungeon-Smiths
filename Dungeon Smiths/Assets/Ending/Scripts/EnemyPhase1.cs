using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPhase1 : MonoBehaviour
{
    public int health = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Rogue_weapon_001(Clone)")
        {
            health -= 10;
            Destroy(collision.gameObject);
        }
    }
}
