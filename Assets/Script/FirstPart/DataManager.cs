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
    public Dictionary<float, CordPoint> tempCordList;
    public float lastRecordedTime;
    public float thresholdDifference;

    public TestDotUI draw;
    public Stopwatch stopwatch;
    public void Awake()
    {
        tempCordList = new Dictionary<float, CordPoint>();
    }
    public void ReceiveCord(CordPoint cordPoint) //call this function when received a vaild data
    {
        if (newList)
        {
            tempCordList.Clear();
            stopwatch.StartTimer();
            lastRecordedTime = stopwatch.GetTimer();
            tempCordList.Add(lastRecordedTime, cordPoint);
            newList = false;
            draw.GenerateDot(lastRecordedTime, cordPoint);
        }

        else 
        {
            ComparePreviousCord(cordPoint);
        }
    }

    public void ComparePreviousCord(CordPoint newCord) 
    {
        float xDifference = Mathf.Abs(tempCordList[lastRecordedTime ].x1 - newCord.x1);
        float yDifference = Mathf.Abs(tempCordList[lastRecordedTime ].y1 - newCord.y1);
        if (xDifference >= thresholdDifference || yDifference >= thresholdDifference) 
        {
            if ((xDifference >= thresholdDifference && yDifference >= thresholdDifference) || generatedNewCord == true)
            {
                lastRecordedTime = stopwatch.GetTimer();
                tempCordList.Add(lastRecordedTime,newCord);
                generatedNewCord = !generatedNewCord;
                Debug.Log("New cord drawn");
            }

            else
            {
                tempCordList.Remove(lastRecordedTime);
                lastRecordedTime = stopwatch.GetTimer();
                tempCordList.Add(lastRecordedTime, newCord);
                Debug.LogWarning("Edited the coordinate");
            }
            draw.GenerateDot(lastRecordedTime, tempCordList[lastRecordedTime]);
            //create a delegate that call LastDotUIUpdate
        }
        //if there is no significant changes, then it will be discarded.
    }

    public Dictionary<float, CordPoint> SendTempCordList() 
    {
        newList = true;
        stopwatch.StopTimer();
        return new Dictionary<float, CordPoint>(tempCordList);
    }

    public void test() 
    {
        tempCordList.Clear();
    }

}
