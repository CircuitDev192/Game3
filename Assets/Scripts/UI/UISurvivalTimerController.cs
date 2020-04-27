using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISurvivalTimerController : MonoBehaviour
{
    private Text timerText;
    private bool startTimer;
    [SerializeField] private float timerValue = 120f;

    // Start is called before the first frame update
    void Start()
    {
        timerText = GetComponentInChildren<Text>();
        EventManager.StartSurvivalCountdown += StartSurvivalCountdown;
    }

    private void StartSurvivalCountdown()
    {
        timerText.enabled = true;
        startTimer = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (startTimer)
        {
            timerValue -= Time.deltaTime;
            float time = timerValue;
            //update timerText
            int seconds = (int)(time % 60);
            time /= 60;
            int minutes = (int)(time % 60);

            timerText.text = string.Format("Time left:  {0}:{1}", minutes.ToString("0"), seconds.ToString("00"));

            if (timerValue <= 0f)
            {
                EventManager.EndMission();
                startTimer = false;
            }
        }
    }
}
