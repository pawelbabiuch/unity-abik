using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    public GameObject prefab;
    public string name;
    public Task taskType;

    public bool autoName = true;

    private void Start()
    {
        name = transform.Find("Content").Find("Label").GetComponent<Text>().text;
    }

    public void AddToTask()
    {
        TaskManager.instance.AddTask(prefab, name, taskType);
    }
}
