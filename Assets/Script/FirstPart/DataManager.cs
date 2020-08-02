using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class DataManager : MonoBehaviour
{
    public bool newList = true;
    public bool generatedNewCord = false;
    public List<CordPoint> tempCordList;
    public int counter;
    public float thresholdDifference;

    public TestDotUI draw;
    public Stopwatch stopwatch;
    public void Awake()
    {
        tempCordList = new List<CordPoint>();
    }
    public void ReceiveCord(CordPoint cordPoint) //call this function when received a vaild data
    {
        if (newList)
        {
            tempCordList.Clear();
            tempCordList.Add(cordPoint);
            newList = false;
            counter = 0;
            draw.GenerateDot(tempCordList[counter]);
        }

        else 
        {
            ComparePreviousCord(cordPoint);
        }
    }

    public void ComparePreviousCord(CordPoint newCord) 
    {
        float xDifference = Mathf.Abs(tempCordList[counter].x - newCord.x);
        float yDifference = Mathf.Abs(tempCordList[counter].y - newCord.y);

        if (xDifference >= thresholdDifference || yDifference >= thresholdDifference) 
        {
            if ((xDifference >= thresholdDifference && yDifference >= thresholdDifference) || generatedNewCord == true)
            {
                tempCordList.Add(newCord);
                generatedNewCord = !generatedNewCord;
                counter++;
                Debug.Log("New cord drawn");
            }

            else
            {
                tempCordList[counter] = newCord;
                Debug.Log("Edited the coordinate");
            }
            draw.GenerateDot(tempCordList[counter]);
            //create a delegate that call LastDotUIUpdate
        }
        //if there is no significant changes, then it will be discarded.
    }

    public List<CordPoint> SendTempCordList() 
    {
        newList = true;
        return tempCordList;
    }


}
