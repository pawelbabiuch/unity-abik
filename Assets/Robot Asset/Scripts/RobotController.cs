using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotController : MonoBehaviour
{
    public Animator handsController, robotController;
    public float robotSpeed = 0.015f;
    public bool canBeControll = false;
    public Transform input, output;

    private float horizontal, vertical;
    private bool handsUp, doneTask = true;
    private float xDistance, yDistance;
    private int parentID = 0;

    [HideInInspector]
    public List<Task> tasks = new List<Task>();
    public static RobotController instance;

    private void Start()
    {
        instance = this;
    }

    private void Update()
    {
        if(canBeControll)
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");

            if (Input.GetKeyUp(KeyCode.Space))
                handsUp = !handsUp;
        }

        handsController.SetFloat("horizontal", horizontal);
        handsController.SetBool("handsUp", handsUp);

        robotController.SetFloat("horizontal", horizontal);
        robotController.SetFloat("vertical", vertical);

        if(horizontal != 0 || vertical != 0)
        {
            transform.Translate(new Vector3(horizontal, vertical, 0) * robotSpeed * Time.deltaTime);
        }
    }

    public void RunTasks()
    {
        for(int i = 0; i < tasks.Count; i++)
        {
            switch(tasks[i])
            {
                case Task.Input:
                    InputTask();
                    break;
                case Task.Output:
                    OutputTask();
                    break;
            }
        }

        EndTask();
    }

    public void EndTask()
    {
        StartCoroutine(EndTaskEnum());
    }

    private IEnumerator EndTaskEnum()
    {
        while (!doneTask) yield return null;

        yield return new WaitForSeconds(2);

        try
        {
            if (handsUp || input.childCount != 0)
                throw new Exception("Zadanie nie zostało poprawnie wykonane! Robot wciąż nie rozłożył wszystkich produktów!");


            ControlPanelManager.instance.sound.clip = ControlPanelManager.instance.defeatAudioClip;
            BossManager.instance.OpenSuccessMenu("00", TimeManager.instance.timerText.text, TaskManager.instance.transform.childCount.ToString());



        }
        catch (Exception ex)
        {
            BossManager.instance.OpenDefeatMenu(ex.Message);
            ControlPanelManager.instance.sound.clip = ControlPanelManager.instance.defeatAudioClip;
        }
        finally
        {
            ControlPanelManager.instance.sound.Play();
        }


        Debug.LogWarning("@ Koniec zadań!");
    }

    public void InputTask()
    {
        StartCoroutine(InputTaskEnum());
    }

    public void OutputTask()
    {
        StartCoroutine(OutputTaskEnum());
    }

    private IEnumerator OutputTaskEnum()
    {
        while (!doneTask) yield return null;

        Debug.Log("@ Rozpoczynam zadanie odkładania!");

        try
        {
            if (!handsUp)
                throw new Exception("Nie mam nic w rękach abym mógł to odłożyć!");
        }catch(Exception ex)
        {
            Debug.LogError(ex.Message);
            StopAllCoroutines();

            BossManager.instance.OpenDefeatMenu(ex.Message);
            ControlPanelManager.instance.sound.clip = ControlPanelManager.instance.defeatAudioClip;
            ControlPanelManager.instance.sound.Play();
        }

        doneTask = false;

        StartCoroutine(GoToPosition(output.position));

        while (!doneTask) yield return null;

        Transform outputObject = transform.GetChild(1).GetChild(0);
        Transform outputParent = output.GetChild(parentID++);
        doneTask = false;

        StartCoroutine(ChangeParent(outputObject, outputParent));

    }

    private IEnumerator InputTaskEnum()
    {
        while (!doneTask) yield return null;

        Debug.Log("@ Rozpoczynam zadanie podnoszenia!");

        try
        {
            if (input.childCount == 0)
                throw new Exception("Brak elementów do zabrania!");

            if (handsUp)
                throw new Exception("Już trzymasz jeden obiekt w rękach!");
        }
        catch(Exception ex)
        {
            Debug.LogWarning(ex.Message);
            StopAllCoroutines();

            BossManager.instance.OpenDefeatMenu(ex.Message);
            ControlPanelManager.instance.sound.clip = ControlPanelManager.instance.defeatAudioClip;
            ControlPanelManager.instance.sound.Play();
        }

        Transform inputObject = input.GetChild(0);
        doneTask = false;

        StartCoroutine(GoToPosition(inputObject.position));

        while (!doneTask) yield return null;

        doneTask = false;
        StartCoroutine(ChangeParent(inputObject, transform.GetChild(1)));
    }

    private IEnumerator GoToPosition(Vector3 position)
    {
        yDistance = Math.Abs(position.y - transform.position.y);

        while (yDistance > 0.1f)
        {
            vertical = (transform.position.y < position.y) ? 1 : -1;
            yDistance = Math.Abs(position.y - transform.position.y);
            yield return null;
        }

        vertical = 0;

        xDistance = Math.Abs(position.x - transform.position.x);

        while(xDistance > 0.1f)
        {
            horizontal = (transform.position.x < position.x) ? 1 : -1;
            xDistance = Math.Abs(position.x - transform.position.x);
            yield return null;
        }

        horizontal = 0;

        doneTask = true; 
    }

    private IEnumerator ChangeParent(Transform child, Transform parent)
    {
        child.parent = parent;

        float distance = Vector3.Distance(child.position, parent.position);
        handsUp = !handsUp;

        while (distance > 0.01f)
        {
            child.position = Vector3.Lerp(child.position, parent.position, Time.deltaTime * 5.0f);
            distance = Vector3.Distance(child.position, parent.position);
            yield return null;
        }

        doneTask = true;
    }
}
