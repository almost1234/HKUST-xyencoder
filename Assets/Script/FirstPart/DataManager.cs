using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions.Must;
using Debug = UnityEngine.Debug;

public class DataManager : MonoBehaviour
{
    public bool newList = true;
    public bool generatedNewCord = false;
    public List<CordPoint> tempCordList;
    public float lastRecordedTime;
    public float thresholdDifference;

    public TestDotUI draw;
    public Stopwatch stopwatch;
    public void Awake()
    {
        tempCordList = new List<CordPoint>();
        CaseSwitch.OnUpdateCordData += ReceiveCord;
    }
    public void ReceiveCord(CordPoint cordPoint) //call this function when received a vaild data
    {
        Debug.Log("Data received");
        if (newList)
        {
            //since time is given via coords, timer is no longer needed
            tempCordList.Clear();
            //stopwatch.StartTimer();
            //lastRecordedTime = stopwatch.GetTimer();
            tempCordList.Add(cordPoint);
            newList = false;
            draw.GenerateDot(cordPoint);
        }

        else 
        {
            ComparePreviousCord(cordPoint);
        }
    }

    public void ComparePreviousCord(CordPoint newCord) 
    {
        int tempIndex = tempCordList.Count - 1;
        float xDifference = Mathf.Abs(tempCordList[tempIndex ].x1 - newCord.x1);
        float yDifference = Mathf.Abs(tempCordList[tempIndex ].y1 - newCord.y1);
        if (xDifference >= thresholdDifference || yDifference >= thresholdDifference)  //check if there is difference in the x/y axis (one)
        {
            if ((xDifference >= thresholdDifference && yDifference >= thresholdDifference) || generatedNewCord == true) // if x & y axis is different by certain threshold
            {
                tempCordList.Add(newCord);
                generatedNewCord = !generatedNewCord; //This indicator is to create a second coordinate after the first coordinate (When the first new different cords is created, another cords with slight diff will be made)
                Debug.Log("New cord drawn");
            }

            else
            {
                tempCordList.RemoveAt(tempIndex);
                tempCordList.Add(newCord);
                Debug.LogWarning("Edited the coordinate");
            }
            draw.GenerateDot(tempCordList[tempIndex]);
            //create a delegate that call LastDotUIUpdate
        }
        //if there is no significant changes, then it will be discarded.
    }

    public List<CordPoint> SendTempCordList() 
    {
        newList = true;
        //stopwatch.StopTimer();
        return new List<CordPoint>(tempCordList);
    }

    public void test() 
    {
        tempCordList.Clear();
    }

}
