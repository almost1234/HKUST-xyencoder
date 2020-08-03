using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavedLogs : MonoBehaviour
{
    Dictionary<string, Dictionary<float, CordPoint>> data;

    private void Awake()
    {
        data = new Dictionary<string, Dictionary<float, CordPoint>>();
        if (data == null) 
        {
            Debug.LogError("Dic not created!");
        }
        //Insert future read all saved logs in text
    }

    public void SaveLog(Dictionary<float, CordPoint> dataGet) 
    {
        data.Add(DateTime.Now.ToString(), dataGet);
        Debug.LogWarning("Current saved log " + data.Count);
    }

    public Dictionary<string, Dictionary<float,CordPoint>> GetLog() 
    {
        return data;
    }
}
