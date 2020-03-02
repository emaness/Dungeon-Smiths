using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RockManagerController : MonoBehaviour
{
    public GameObject rockPrefab;
    public Image skelBar;
    private Vector3 rockInitialPos;

    private bool dragging;
    private Vector3 dragEnd;

    private bool winning;

    void Start()
    {
        var rock = gameObject.transform.GetChild(0).gameObject;
        rockInitialPos = rock.transform.position;
        rockInitialPos.z = 0;
        rock.GetComponent<RockController>().skelBar = skelBar;
        dragging = false;
        winning = false;
    }

    private IEnumerator DoWin()
    {
        yield return new WaitForSeconds(1.0f);

        SceneManager.UnloadSceneAsync("RockThrowing");
        Scene mt = SceneManager.GetSceneByName("Level1");
        foreach (GameObject obj in mt.GetRootGameObjects())
        {
            obj.SetActive(true);
        }
    }

    public void StartWin()
    {
        winning = true;
        StartCoroutine("DoWin");
    }

    void Update()
    {
        if(winning)
        {
            return;
        }

        if(gameObject.transform.childCount == 0)
        {
            // the rock was destroyed, need to spawn another one
            var go = Instantiate(rockPrefab, rockInitialPos, Quaternion.identity, transform);
            go.GetComponent<RockController>().skelBar = skelBar;
            return;
        }

        var rock = gameObject.transform.GetChild(0).gameObject;
        var rb = rock.GetComponent<Rigidbody2D>();

        if(rb.bodyType != RigidbodyType2D.Static)
        {
            // rock is currently flying
            return;
        }

        if (Input.mousePresent || Input.touchSupported)
        {
            bool down;
            Vector3 pos;
            if (Input.mousePresent)
            {
                down = Input.GetMouseButton(0);
                pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                pos.z = 0;
            }
            else
            {
                down = Input.touchCount > 0;
                pos = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
                pos.z = 0;
            }

            float maxMagnitude = 1.0f;

            if (down)
            {
                Vector3 dir = pos - rockInitialPos;
                
                if (dragging || dir.magnitude < maxMagnitude * 1.5f)
                {
                    if (dir.magnitude > maxMagnitude)
                    {
                        pos = rockInitialPos + dir.normalized * maxMagnitude;
                    }

                    if (!dragging)
                    {
                        dragging = true;
                    }
                    dragEnd = pos;

                    rock.transform.position = pos;
                }
            } else if(dragging)
            {
                rb.bodyType = RigidbodyType2D.Dynamic;

                Vector3 dir = dragEnd - rockInitialPos;
                var force = (-dir.normalized) * dir.magnitude*600.0f;
                rb.AddForce(force);
                rb.AddTorque(Random.Range(-30f, 30f));

                dragging = false;
            }
        }
    }
}
