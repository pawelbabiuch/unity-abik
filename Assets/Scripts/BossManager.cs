using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossManager : MonoBehaviour
{
    public static BossManager instance;

    public FadeUI gameCanvas;

    [Header("Success menu")]
    public GameObject successMenu;
    public Text daySuccess, timeSuccess, lineSuccess;

    [Header("Defeat menu")]
    public GameObject defeatMenu;
    public ReadInfo errorMsg;

    private void Start()
    {
        instance = this;
    }

    public void OpenSuccessMenu(string day, string time, string lineNumber)
    {
        daySuccess.text = day;
        timeSuccess.text = time;
        lineSuccess.text = lineNumber;
        successMenu.SetActive(true);
        gameCanvas.FadeOut();
    }

    public void OpenDefeatMenu(string errorText)
    {
        errorMsg.info = errorText;
        defeatMenu.SetActive(true);
        gameCanvas.FadeOut();
    }
}
