using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam_Temple.Level
{
    using UnityEngine;
    using UnityEngine.UI;

    public class Timer : MonoBehaviour
    {
        [SerializeField]
        private Text timeText;
        private float startTime;
        public string TimerText { get; private set; }

        private void Start()
        {
            StartTimer();
        }

        private void Update()
        {
            float elapsedTime = Time.time - startTime;
            UpdateTimeText(elapsedTime);
        }

        private void StartTimer()
        {
            startTime = Time.time;
        }

        private void UpdateTimeText(float elapsedTime)
        {
            int minutes = Mathf.FloorToInt(elapsedTime / 60) % 60;
            int seconds = Mathf.FloorToInt(elapsedTime % 60);
            int milliseconds = Mathf.FloorToInt((elapsedTime * 1000) % 1000);

            TimerText="PlayingTime: " + string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
            timeText.text = TimerText;
        }
        public void Stop()
        {
            enabled = false;
        }
    }
}
