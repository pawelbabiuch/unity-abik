using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public Text timerText;
    public static TimeManager instance;
    

    private int sec, min;

    private void Start()
    {
        instance = this;
    }

    public void StartTimer()
    {
        InvokeRepeating("Tick", 0, 1);
    }

    public void StopTimer()
    {
        CancelInvoke("Tick");
    }

    private void Tick()
    {
        if(++sec >= 60)
        {
            sec = 0;
            min++;
        }

        string minStr = (min < 9) ? "0" + min : min.ToString();
        string secStr = (sec < 9) ? "0" + sec : sec.ToString();

        timerText.text = string.Format("{0}:{1}", minStr, secStr);
    }
}
