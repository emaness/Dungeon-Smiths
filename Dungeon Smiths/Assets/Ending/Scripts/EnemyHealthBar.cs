using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public GameObject enemy1;
    public GameObject enemy2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy2.activeSelf)
        {
            gameObject.GetComponent<Text>().text = "Enemy Health: " + enemy2.GetComponent<EnemyPhase2>().health;
        }
        else
        {
            gameObject.GetComponent<Text>().text = "Enemy Health: " + enemy1.GetComponent<EnemyPhase1>().health;
        }
    }
}
