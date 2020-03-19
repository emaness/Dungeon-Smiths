using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyPos
{
    public string enemyName;
    public Transform[] pos;
}

/// <summary>
/// enemy management
/// </summary>
public class EnemyManager : Singleton<EnemyManager>
{
    public List<EnemyPos> enemyPosList = new List<EnemyPos>();

    private float time;

    [Header("Generate enemies at intervals")]
    [SerializeField]
    private float interal = 5.0f;

    [Header("initPosition")]
    [SerializeField]
    private Transform initPos;

    [Header("enemyPrefab")]
    [SerializeField]
    private GameObject enemyPrefab;

    private bool isAdd = false;

    [SerializeField]
    private Transform enemyParent;

    [Header("enemy move speed")]
    [SerializeField]
    private float speed = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        GameObject go = GameObject.Instantiate(enemyPrefab) as GameObject;
        go.transform.parent = enemyParent;
        go.transform.localPosition = new Vector3(-3.21f, 3.23f, 0.0f);
        go.GetComponent<EnemyMove>().ways1 = enemyPosList[Random.Range(0, enemyPosList.Count)].pos;
        go.GetComponent<EnemyMove>().speed = speed;
    }

    // Update is called once per frame
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
