﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SavedLogs : MonoBehaviour
{
    Dictionary<string[], Dictionary<float, CordPoint>> data;

    private void Awake()
    {
        TextAsset textData = Resources.Load<TextAsset>("Text/textFilePath");
        data = TextAssetTool.CreateCoordinateDictionary(textData.text);
        //data = new Dictionary<string[], Dictionary<float, CordPoint>>();
        
    }

    public void SaveLog(Dictionary<float, CordPoint> dataGet) 
    {
        data.Add(new string[] { Stopwatch.timer.ToString(), DateTime.Now.ToString() }, dataGet);
        Debug.LogWarning("Current saved log " + data.Count);
    }

    public Dictionary<string[], Dictionary<float,CordPoint>> GetLog() 
    {
        return data;
    }


}
