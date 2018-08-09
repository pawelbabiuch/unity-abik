using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SlidgerController : MonoBehaviour
{
    private Slider slidger;
    private Text percent;
    private int p = 0;

    public bool hideInGame = false;
    public ShowIn showIn = ShowIn.Percent;
    public GameObject percentImage;

    private void Start()
    {
        if (hideInGame)
        {
            percentImage.SetActive(false);
            return;
        }

        slidger = GetComponent<Slider>();
        percent = percentImage.GetComponentInChildren<Text>();
        OnChange();
    }

    void Update()
    {
        if (hideInGame) return;
        if ((int)(slidger.value * 100) != p) OnChange();
    }

    public void OnChange()
    {
        switch (showIn)
        {
            case ShowIn.Percent:
                p = (int)(slidger.value / slidger.maxValue * 100);
                percent.text = string.Format("{0}%", p);
                break;
            case ShowIn.Value:
                p = (int)(slidger.value);
                percent.text = string.Format("x{0}", p);
                break;
        }

    }
}

public enum ShowIn
{
    Percent, Value
}
