using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float totalSeconds = 600.0f;
    private float internalTimer;
    private Text timeText;

    // Start is called before the first frame update
    void Start()
    {
        timeText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        internalTimer = totalSeconds;
        internalTimer -= Time.time;
        if (internalTimer <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
        int minutes = Mathf.FloorToInt(internalTimer / 60.0F);
        int seconds = Mathf.FloorToInt(internalTimer - minutes * 60);
        string formatted = string.Format("{0:00}:{1:00}", minutes, seconds);
        timeText.text = formatted;
    }
}
