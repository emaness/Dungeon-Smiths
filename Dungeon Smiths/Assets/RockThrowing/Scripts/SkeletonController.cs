using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonController : MonoBehaviour
{
    private int walkingHash = Animator.StringToHash("Walking");
    private int walkingStateHash = Animator.StringToHash("Base Layer.Walking");

    public int InitialHealth = 5;
    public int Health;

    public IEnumerator IdleThenWalk()
    {
        yield return new WaitForSeconds(2.0f);

        var anim = GetComponent<Animator>();
        anim.SetTrigger(walkingHash);
    }

    void Start()
    {
        Health = InitialHealth;

        StartCoroutine("IdleThenWalk");
    }

    void Update()
    {
        var anim = GetComponent<Animator>();
        var stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        if(stateInfo.fullPathHash == walkingStateHash)
        {
            transform.position = transform.position + new Vector3(-0.5f * Time.deltaTime, 0, 0);
        }
    }
}
