using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SavedLogs : MonoBehaviour
{
    Dictionary<string, List<CordPoint>> data;

    private void Awake()
    {
        TextAsset textData = Resources.Load<TextAsset>("Text/textFilePath");
        data = TextAssetTool.CreateCoordinateDictionary(textData.text);
        //data = new Dictionary<string, List<CordPoint>>();

    }

    public void SaveLog(List<CordPoint> dataGet) 
    {
        data.Add(DateTime.Now.ToString(), dataGet);
        Debug.LogWarning("Current saved log " + data.Count);
    }

    public Dictionary<string, List<CordPoint>> GetLog() 
    {
        return data;
    }


}
