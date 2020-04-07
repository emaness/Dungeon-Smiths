using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RockController : MonoBehaviour
{
    public GameObject rockExplosionPrefab;
    public Image skelBar;

    private void Die()
    {
        Instantiate(rockExplosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Die();
        } else if(collision.gameObject.CompareTag("SkeletonWeakPoint"))
        {
            var skel = GameObject.FindWithTag("Skeleton");
            var ctrl = skel.GetComponent<SkeletonController>();
            ctrl.Health = GameObject.FindGameObjectsWithTag("SkeletonWeakPoint").Length - 1;

            skelBar.rectTransform.localScale = new Vector3(ctrl.Health / (float)ctrl.InitialHealth, 1.0f, 1.0f);

            if (ctrl.Health == 0)
            {
                transform.parent.gameObject.SendMessage("StartWin");
                ctrl.Die();
            }
           
            Destroy(collision.gameObject);
            Die();
        } else if(collision.gameObject.CompareTag("Skeleton"))
        {
            Die();
        }
    }
}