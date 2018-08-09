using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CanvasGroup))]
public class FadeUI : MonoBehaviour, IPointerClickHandler
{

    [Header("Triggers")]
    public bool onMouseClick;
    public bool onMouseEnter;   // ONLY WHEN !FADEIN
    public bool onMouseLeave;   // Only when FADEIN

    [Header("Fade settings")]
    [Range(0, 1)]
    public float startDelay = 1;

    [Range(0.01f, 3)]
    public float fadeTime = 0.5f;

    [Space]
    public bool onStart;
    public bool changeInteractable;
    public bool changeRaycastBlock;

    [Space]
    public bool removeAfterTriggerDetect = false;
    public bool turnOffObject;

    [Header("Alpha settings")]
    public float minAlpha = 0;
    public float maxAlpha = 1;

    private CanvasGroup canvasGroup;
    private float curDelay = 0;
    private bool _startEvent = false;

    [SerializeField]
    [Space]
    private bool fadeIn = false;

    private void Awake()
    {
        canvasGroup = this.GetComponent<CanvasGroup>();

        if(fadeIn)
            canvasGroup.alpha = maxAlpha;
        else
            canvasGroup.alpha = minAlpha;
    }

    private void Start()
    {
        if (onStart) startEvent = true;
    }

    private void Update()
    {
        if (!startEvent) return;

        if(curDelay < startDelay)
        {
            curDelay += 1 * Time.deltaTime;
            return;
        }

        if(!fadeIn)
        {
            if (canvasGroup.alpha < maxAlpha)
            {
                canvasGroup.alpha += fadeTime * Time.deltaTime;

                if ((maxAlpha - canvasGroup.alpha) < 0.1f)
                    canvasGroup.alpha = maxAlpha;
            }
            else
            {
                Change(true);
            }
        }
        else
        {
            if (canvasGroup.alpha > minAlpha)
            {
                canvasGroup.alpha -= fadeTime * Time.deltaTime;

                if ((minAlpha + canvasGroup.alpha) < 0.1f)
                    canvasGroup.alpha = minAlpha;
            }
            else
            {
                Change(false);

                if (turnOffObject) gameObject.SetActive(false);
            }
        }
    }

    private void Change(bool fadeIn)
    {
        if (removeAfterTriggerDetect) Destroy(GetComponent<FadeUI>());

        if (changeInteractable)
            canvasGroup.interactable = !canvasGroup.interactable;

        if (changeRaycastBlock)
            canvasGroup.blocksRaycasts = !canvasGroup.blocksRaycasts;

        startEvent = false;
        this.fadeIn = fadeIn;
        curDelay = 0;
    }


    public void OnPointerClick(PointerEventData eventData)
    {

        if (onMouseClick && !startEvent) startEvent = true;
    }

    public bool startEvent
    {
        get { return _startEvent; }
        set { _startEvent = value; }
    }


    public void FadeIn()
    {
        fadeIn = false;
        canvasGroup.alpha = minAlpha;
        startEvent = true;
    }

    public void FadeOut()
    {
        fadeIn = true;
        canvasGroup.alpha = maxAlpha;
        startEvent = true;
    }

    public void Fade()
    {
        startEvent = true;
    }
}
