using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class LargeTooltip : MonoBehaviour, IPointerClickHandler
{
    [HideInInspector]
    public static GameObject largeTooltip;
    private static Text textName, textInfo;

    public string name, info;

    private void Start()
    {
        if(!largeTooltip)
        {
            largeTooltip = GameObject.Find("LargeTooltip");

            textName = largeTooltip.transform.Find("Header").Find("TextName").GetComponent<Text>();
            textInfo = largeTooltip.transform.Find("TextInfo").GetComponent<Text>();

            largeTooltip.SetActive(false);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!largeTooltip.activeInHierarchy)
            largeTooltip.SetActive(true);

        textName.text = name;
        textInfo.text = info;

        if(eventData.clickCount == 2 )
        {
            if (GetComponent<ItemManager>())
                GetComponent<ItemManager>().AddToTask();
        }
    }
}
