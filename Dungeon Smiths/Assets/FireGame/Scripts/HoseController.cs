using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HoseController : MonoBehaviour
{
    public GameObject waterDroplet;
    public GameObject waterSpawnPoint;

    public Image healthBar;

    private float health;
    private float dropletTimeout;

    public bool dead = false;

    private void Start()
    {
        health = 100.0f;
        dropletTimeout = 0.0f;
    }

    public void ApplyDamage(float damage)
    {
        health -= damage;
        if(health <= 0.01f)
        {
            dead = true;
            health = 0.0f;
            GameObject.Find("FireManager").GetComponent<FireManagerController>().GameOver();
        }
        healthBar.rectTransform.localScale = new Vector3(health / 100.0f, 1.0f, 1.0f);
        if(dead)
        {
            gameObject.SetActive(false);
        }
    }

    void Update()
    {
        const float dropletsPerSecond = 100;
        const float maxAnglesPerSecond = 40;

        if(dead)
        {
            return;
        }

        dropletTimeout -= Time.deltaTime;
        dropletTimeout = Mathf.Max(0, dropletTimeout);

        if(Input.mousePresent || Input.touchSupported)
        {
            bool down;
            Vector3 pos;
            if(Input.mousePresent)
            {
                down = Input.GetMouseButton(0);
                pos = Input.mousePosition;
            } else
            {
                down = Input.touchCount > 0;
                pos = Input.touches[0].position;
            }

            if(down)
            {
                // turn
                Vector3 wp = Camera.main.ScreenToWorldPoint(pos);
                Vector3 d = wp - gameObject.transform.position;
                if(Vector2.Dot(new Vector2(d.x, d.y), new Vector2(0, 1)) > 0.4)
                {
                    float currentAngle = gameObject.transform.rotation.eulerAngles.z;
                    if(currentAngle > 180)
                    {
                        currentAngle -= 360;
                    }

                    float targetAngle = Mathf.Atan2(d.y, d.x) * Mathf.Rad2Deg;
                    float maxTheta = maxAnglesPerSecond * Time.deltaTime;
                    float theta = targetAngle - currentAngle;
                    if(theta < -maxTheta)
                    {
                        theta = -maxTheta;
                    } else if(theta > maxTheta)
                    {
                        theta = maxTheta;
                    }

                    gameObject.transform.Rotate(new Vector3(0, 0, 1), theta);
                }

                // shoot water
                if(dropletTimeout < 0.01f)
                {
                    // Play hose noise
                    if (GetComponent<AudioSource>().isPlaying == false)
                    {
                        GetComponent<AudioSource>().volume = Random.Range(0.6f, .8f);
                        GetComponent<AudioSource>().pitch = Random.Range(0.9f, 1);
                        GetComponent<AudioSource>().Play();
                    }

                    Vector3 p = gameObject.transform.TransformPoint(waterSpawnPoint.transform.localPosition + new Vector3(0, Random.Range(-0.25f, 0.25f)));
                    GameObject droplet = Instantiate(waterDroplet, p, Quaternion.identity);
                    Vector3 dir = p - gameObject.transform.position;
                    dir.Normalize();
                    var rb = droplet.GetComponent<Rigidbody2D>();
                    rb.velocity = dir * 3.0f;
                    dropletTimeout = 1.0f / dropletsPerSecond;
                }
            }
            else
            {
                GetComponent<AudioSource>().Stop();
            }
        }
    }
}
