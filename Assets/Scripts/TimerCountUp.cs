using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerCountUp : MonoBehaviour
{
    [SerializeField] Text timerText; 
    void Update()
    {
        float t = Time.timeSinceLevelLoad;
        int seconds = (int)(t % 60);
        t /= 60;
        int minutes = (int)(t % 60);

        timerText.text = string.Format("{0}:{1}", minutes.ToString("00"), seconds.ToString("00") );

    }
}
