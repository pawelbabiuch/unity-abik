using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ReadInfo : MonoBehaviour
{
    public string info = "";

    [Range(0.1f, 5.0f)]
    public float readSpeed = 2;

    public bool unlockGroup = false;
    public CanvasGroup canvasGroup;

    private WaitForSeconds wFS;
    private Text txt;

    private void Start()
    {
        wFS = new WaitForSeconds(readSpeed/10.0f);
        txt = GetComponent<Text>();
        StartCoroutine(Read());
    }

    private IEnumerator Read()
    {
        int i = 0;

        while(!txt.text.Contains(info))
        {
            txt.text += info[i++];
            yield return wFS;
        }

        if(unlockGroup && canvasGroup != null)
        {
            canvasGroup.interactable = true;
        }
    }
}
