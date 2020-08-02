using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavedLogs : MonoBehaviour
{
    Dictionary<string, List<CordPoint>> data;

    private void Awake()
    {
        data = new Dictionary<string, List<CordPoint>>();
        if (data == null) 
        {
            Debug.LogError("Dic not created!");
        }
        //Insert future read all saved logs in text
    }

    public void SaveLog(List<CordPoint> dataGet) 
    {
        if (dataGet == null) 
        {
            Debug.LogError("EMPTY!");
        }
        data.Add(DateTime.Now.ToString(), dataGet);
        Debug.LogWarning("Current saved log " + data.Count);
    }

    public Dictionary<string, List<CordPoint>> GetLog() 
    {
        return data;
    }
}
