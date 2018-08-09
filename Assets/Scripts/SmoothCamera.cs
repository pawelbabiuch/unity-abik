using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCamera : MonoBehaviour
{
    private Transform robot, cam;
    private float distance = 0;

    public float smooth = 5.0f;

    private void Start()
    {
        robot = GameObject.FindWithTag("Player").transform;
        cam = Camera.main.transform;
    }

    private void Update()
    {
        distance = Mathf.Abs(cam.position.x - robot.position.x);

        if(distance > 0.1f)
        {
            float lerpX = Mathf.Lerp(cam.position.x, robot.position.x, smooth * Time.deltaTime * Time.timeScale);
            cam.position = new Vector3(lerpX, cam.position.y, cam.position.z);
        }
    }
}
