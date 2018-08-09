using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Item : MonoBehaviour, IPointerClickHandler
{
    public Task task;
    private static Transform parent;
    private Text lineNumberText;
    private int lineNumber;

    private void Start()
    {
        parent = transform.parent;
        lineNumberText = transform.Find("LineNumber").GetComponent<Text>();
        lineNumber = int.Parse(lineNumberText.text);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            int myChildID = transform.GetSiblingIndex();

            if (Input.GetKey(KeyCode.D))                                                   // Remove element
            {
                ReNumberList(myChildID);
                Destroy(this.gameObject);
            }
            else if (myChildID != 0)
            {
                ChangeLineNumber(myChildID - 1, lineNumber);
                lineNumberText.text = (--lineNumber).ToString();
                transform.SetSiblingIndex(myChildID - 1);
            }    
        }
        else if(eventData.button == PointerEventData.InputButton.Right)
        {
            if (eventData.clickCount == 1)                                                  // Move down
            {
                int chilCount = parent.childCount; // [1,2,3,4,5,...]
                int myChildID = transform.GetSiblingIndex(); // [0,1,2,3,4,...]

                if(myChildID +1 < chilCount)
                {
                    ChangeLineNumber(myChildID + 1, lineNumber);
                    lineNumberText.text = (++lineNumber).ToString();
                    transform.SetSiblingIndex(myChildID + 1);
                }
            }
        }
    }


    private void ReNumberList(int startCount)
    {
        for(int i = startCount +1; i < parent.childCount; i++)
        {
            ChangeLineNumber(i, lineNumber++);
        }
    }

    private void ChangeLineNumber(int objID, int number)
    {
        Transform item = parent.GetChild(objID);
        item.Find("LineNumber").GetComponent<Text>().text = number.ToString();
        item.GetComponent<Item>().lineNumber = number;
    }
}

public enum Task
{
    Null, Input, Output
}
