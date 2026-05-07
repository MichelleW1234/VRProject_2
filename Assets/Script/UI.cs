using UnityEngine;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UI : MonoBehaviour
{

    [SerializeField]
    private TMP_Text timer_text, countdown_text;

    private bool timerActive; //to see if timer would be runned
    private bool countActive; //to see if countdown should be runned
    private float currentTime;
    private float countdownTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentTime = 0f;
        countdownTime = 3f;
        Time.timeScale = 0f; //pause all when start of game
        countActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (countActive)
        {

            countdown_text.text = Mathf.CeilToInt(countdownTime).ToString();
            Debug.Log("Countdown: " + countdownTime);

            if (countdownTime <= 0f)
            {
                countActive = false;
                Time.timeScale = 1f;
                StartTimer();
                Debug.Log("Countdown finished, timer started");

            }

            return;
        }

        if (timerActive) currentTime += Time.deltaTime;



        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        timer_text.text = time.Minutes.ToString("00") + ":" + time.Seconds.ToString("00"); //convert timer to UI
    }


    //IEnumerator StartCountdown()
    //{

    //}

    public void StartTimer()
    {
        timerActive = true;
    }

    public void StopTimer()
    {
        timerActive = false;
    }
}
