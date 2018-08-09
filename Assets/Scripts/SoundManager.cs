using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class SoundManager : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    [Header("Triggers")]
    public bool onMouseEnter;
    public bool onMouseClick;

    [Header("Sounds")]
    public AudioClip clipEnter;
    public AudioClip clipClick;

    [Space]
    public bool mainAudioSource = false;

    private AudioSource aS;

    public void Start()
    {
        if(mainAudioSource)
            aS = GameObject.Find("soundMain").GetComponent<AudioSource>();
        else
            aS = GameObject.Find("soundEffects").GetComponent<AudioSource>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(onMouseEnter && clipEnter != null)
        {
            aS.PlayOneShot(clipEnter);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (onMouseClick && clipClick != null)
        {
            aS.PlayOneShot(clipClick);
        }
    }
}
