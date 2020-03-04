using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyPos
{
    public string enemyName;
    public Transform[] pos; /// component of controlling the position of GameObject
}

/// Management of Enemy

public class EnemyManager : Singleton<EnemyManager>
{
    public List<EnemyPos> enemyPosList = new List<EnemyPos>();

    private float time;

    [Header("create enemy in a certain time")]
    [SerializeField]
    private float interal = 5.0f;

    [Header("initial position")]
    [SerializeField]
    private Transform initPos;

    [Header("enemy prefab")]
    [SerializeField]
    private GameObject enemyPrefab;

    private bool isAdd = false;

    [SerializeField]
    private Transform enemyParent;

    [Header("moving speed")]
    [SerializeField]
    private float speed = 2.0f;

   
    void Start()
    {
        GameObject go = GameObject.Instantiate(enemyPrefab) as GameObject;
        go.transform.parent = enemyParent;
        go.transform.localPosition = new Vector3(-3.21f, 3.23f, 0.0f);
        go.GetComponent<EnemyMove>().ways1 = enemyPosList[Random.Range(0, enemyPosList.Count)].pos;
        go.GetComponent<EnemyMove>().speed = speed;
    }

 
    void Update()
    {
        SetEnemy();
    }

    private void SetEnemy()
    {
        time += Time.deltaTime;

        if(time >= interal)
        {
            isAdd = true;
            time = 0.0f;
        }

        if (isAdd)
        {
            GameObject go = GameObject.Instantiate(enemyPrefab) as GameObject;
            go.transform.parent = enemyParent;
            go.transform.localPosition = new Vector3(-3.21f, 3.23f, 0.0f);
            go.GetComponent<EnemyMove>().ways1 = enemyPosList[Random.Range(0, enemyPosList.Count)].pos;
            go.GetComponent<EnemyMove>().speed = speed;
            isAdd = false;
        }
    }

}
