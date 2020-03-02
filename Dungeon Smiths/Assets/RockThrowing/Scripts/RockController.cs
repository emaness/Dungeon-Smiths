using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RockController : MonoBehaviour
{
    public Image skelBar;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        } else if(collision.gameObject.CompareTag("Skeleton"))
        {
            var skel = collision.gameObject;
            var ctrl = skel.GetComponent<SkeletonController>();
            ctrl.Health -= 1;
            
            skelBar.rectTransform.localScale = new Vector3(ctrl.Health / (float)ctrl.InitialHealth, 1.0f, 1.0f);

            if (ctrl.Health == 0)
            {
                transform.parent.gameObject.SendMessage("StartWin");
                Destroy(skel);
            }
           
            Destroy(gameObject);
        }
    }
}