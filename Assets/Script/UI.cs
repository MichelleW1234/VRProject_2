using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{

    [SerializeField]
    private TMP_Text timer_text, countdown_text, checkpoint_reached;

    private bool timerActive; //to see if timer would be runned
    private float currentTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentTime = 0f;
        timerActive = false;
        StartCoroutine(StartCountdown());
    }

    // Update is called once per frame
    void Update()
    {
        if (timerActive) currentTime += Time.unscaledDeltaTime;

        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        timer_text.text = time.Minutes.ToString("00") + ":" + time.Seconds.ToString("00"); //convert timer to UI
    }


    private IEnumerator StartCountdown()
    {
        Time.timeScale = 0f;

        countdown_text.gameObject.SetActive(true);

        countdown_text.text = "3";
        yield return new WaitForSecondsRealtime(1f);

        countdown_text.text = "2";
        yield return new WaitForSecondsRealtime(1f);

        countdown_text.text = "1";
        yield return new WaitForSecondsRealtime(1f);

        countdown_text.gameObject.SetActive(false);

        Time.timeScale = 1f;
        StartTimer();
    }

    public void StartTimer()
    {
        timerActive = true;
    }

    public void StopTimer()
    {
        timerActive = false;
    }

    public void UpdateCheckPoint(int count)
    {
        checkpoint_reached.text = count.ToString();
    }
}
