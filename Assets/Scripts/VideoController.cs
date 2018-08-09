using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VideoController : MonoBehaviour
{
    public RawImage rI;
    public MovieTexture mT;

    public float delay = 1.0f;

    private RectTransform rT;

    private void Start()
    {
        rT = GetComponent<RectTransform>();
        Invoke("StartPlay", delay);
    }

    private void StartPlay()
    {
        rI.enabled = true;
        rI.texture = mT;
        mT.loop = true;
        mT.Play();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0)) return;

        rT.position = Input.mousePosition;
    }
}
