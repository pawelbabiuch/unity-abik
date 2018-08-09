using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlPanelManager : MonoBehaviour
{
    public Image buttonPauseImage;
    public Slider timeScaleSlidger;
    public CanvasGroup refPanel, taskPanel;
    public AudioClip playAudioClip, successAudioClip, defeatAudioClip;

    [HideInInspector]
    public AudioSource sound;

    public static ControlPanelManager instance;

    private void Start()
    {
        instance = this;
    }

    public void Play()
    {
        sound = GameObject.Find("soundMain").GetComponent<AudioSource>();


        try
        {
            if(TaskManager.instance.transform.childCount == 0)
            {
                throw new Exception("Nie możesz rozpocząć programu z pustym kodem!");
            }

            refPanel.gameObject.SetActive(false);
            taskPanel.blocksRaycasts = false;
            LargeTooltip.largeTooltip.SetActive(false);
            SmallTooltip.smallTooltip.SetActive(false);
            TimeManager.instance.StopTimer();

            TaskManager.instance.ReadTasks();
            RobotController.instance.RunTasks();

            sound.clip = playAudioClip;  
        }
        catch(Exception ex)
        {
            BossManager.instance.OpenDefeatMenu(ex.Message);
            sound.clip = defeatAudioClip;
        }
        finally
        {
            sound.Play();
        }
    }

    public void Pause()
    {
        if(Time.timeScale == 0) // PLAY AGAIN
        {
            sound.volume = 1.0f;
            buttonPauseImage.color = Color.white;
            Time.timeScale = timeScaleSlidger.value;
            timeScaleSlidger.interactable = true;
        }
        else                    // STOP
        {
            sound.volume = 0.3f;
            buttonPauseImage.color = Color.red;
            Time.timeScale = 0;
            timeScaleSlidger.interactable = false;
        }
    }

    public void TimeScale()
    {
        Time.timeScale = timeScaleSlidger.value;
    }
}
