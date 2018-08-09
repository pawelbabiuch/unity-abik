using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
    private Transform content;

    public static TaskManager instance;

    private void Start()
    {
        instance    = this;
        content     = this.transform;
    }

    public void AddTask(GameObject task, string taskName, Task taskType)
    {
        GameObject newTask = Instantiate(task, content);
        newTask.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

        int lineNumber = content.childCount;
        newTask.transform.Find("LineNumber").GetComponent<Text>().text = lineNumber++.ToString();
        newTask.transform.Find("Content").Find("Label").GetComponent<Text>().text = taskName;
        newTask.GetComponent<Item>().task = taskType;

    }

    public void ReadTasks()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            Task task = transform.GetChild(i).GetComponent<Item>().task;
            RobotController.instance.tasks.Add(task);
            //Debug.Log(task);
        }
    }
}
