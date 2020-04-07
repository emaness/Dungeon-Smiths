using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkeletonController : MonoBehaviour
{
    private int walkingHash = Animator.StringToHash("Walking");
    private int deadHash = Animator.StringToHash("Dead");
    private int walkingStateHash = Animator.StringToHash("Base Layer.Walking");
    private int deadStateHash = Animator.StringToHash("Base Layer.Dead");

    public GameObject weakPointPrefab;

    public int InitialHealth;
    public int Health;

    public IEnumerator IdleThenWalk()
    {
        yield return new WaitForSeconds(0.1f);

        var anim = GetComponent<Animator>();
        anim.SetTrigger(walkingHash);
    }

    public void Die()
    {
        var anim = GetComponent<Animator>();
        anim.SetTrigger(deadHash);
    }

    public void SetUpWeakpoints()
    {
        // pick numWeakpoints random weakpoints to keep
        // iterate over weakpoints and randomly discard until there are only numWeakpoints remaining
        int numWeakpoints = InitialHealth;
        GameObject[] weakpoints = GameObject.FindGameObjectsWithTag("SkeletonWeakPoint");
        int numRemaining = weakpoints.Length;
        do
        {
            for(int i = 0; i != weakpoints.Length; ++i)
            {
                if(Random.Range(0.0f, 1.0f) < 0.2f)
                {
                    Destroy(weakpoints[i]);
                    --numRemaining;
                    if(numRemaining == numWeakpoints)
                    {
                        break;
                    }
                }
            }
            weakpoints = GameObject.FindGameObjectsWithTag("SkeletonWeakPoint");
        } while (numRemaining > numWeakpoints);
    }

    void Start()
    {
        InitialHealth = 5;
        Health = InitialHealth;
        
        SetUpWeakpoints();

        StartCoroutine("IdleThenWalk");
    }

    void Update()
    {
        var anim = GetComponent<Animator>();
        var stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.fullPathHash == walkingStateHash)
        {
            transform.position = transform.position + new Vector3(-Time.deltaTime, 0, 0);
            if (transform.position.x < -4.15)
            {
                GameObject.Find("RockManager").GetComponent<RockManagerController>().StartCoroutine("GameOver");
            }
        } else if(stateInfo.fullPathHash == deadStateHash)
        {
            if(transform.position.y >= -3f)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - 1.0f * Time.deltaTime, transform.position.z);
            }
        }
    }
}
