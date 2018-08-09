using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SmallTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [HideInInspector]
    public static GameObject smallTooltip;
    private static Text tooltipText;
    private static RectTransform rectTransform;
    private static FadeUI fadeUI;
    private static Vector3 offset;

    private bool isEnter = false;
    private float curTime = 0;
    

    [Range(0, 2)]
    public float timeToEnter = 0.5f;
    public string text = "";

    private void Start()
    {
        if (!smallTooltip)
        {
            smallTooltip = GameObject.Find("SmallTooltip");
            rectTransform = smallTooltip.GetComponent<RectTransform>();

            if (smallTooltip.activeInHierarchy)
                smallTooltip.SetActive(false);

            offset.x = rectTransform.rect.width / 2 + 5;
            offset.y = rectTransform.rect.height / 2 + 5;

            fadeUI = smallTooltip.GetComponent<FadeUI>();

            tooltipText = smallTooltip.transform.Find("Text Tooltip").GetComponent<Text>();
        }
    }

    private void Update()
    {
        if (!isEnter) return;

        if (curTime < timeToEnter)
            curTime += Time.deltaTime;
        else if (!smallTooltip.activeInHierarchy)
        {
            smallTooltip.SetActive(true);
            fadeUI.startEvent = true;
            rectTransform.position = Input.mousePosition + offset;
        } 
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isEnter = true;
        tooltipText.text = text;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        curTime = 0;
        fadeUI.startEvent = true;
        isEnter = false;
    }
}
