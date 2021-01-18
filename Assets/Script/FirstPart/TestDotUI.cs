﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDotUI : MonoBehaviour
{
    public List<DataPoint> dotList;
    public GameObject sampleDotGenerate;
    public CalculateCoordinate calCord;
    public Transform RedGroup;
    public Transform BlueGroup;

    public void Awake()
    {
        dotList = new List<DataPoint>();
    }
    public void GenerateDot(CordPoint cordPoint)//add dual function in the future //improve more you know how shit it is
    {   
        DataPoint dotVar = Instantiate(sampleDotGenerate, RedGroup).GetComponent<DataPoint>();
        dotVar.SetCord(cordPoint);
        Vector3 directionLine = new Vector3(dotVar.coordinate.x2 - dotVar.coordinate.x1, dotVar.coordinate.y2 - dotVar.coordinate.y1, 0);
        switch (cordPoint.type) 
        {
            case 0:
                //Red Side coordinate
                dotVar.SetData(calCord.ConvertToRedCord(cordPoint), new Vector3[2] {directionLine, new Vector3(0,0,0) });
                break;
            case 1:
                //Blue Side coordinate
                dotVar.SetData(calCord.ConvertToBlueCord(cordPoint), new Vector3[2] { directionLine, new Vector3(0, 0, 0) });
                break;
        }
        dotList.Add(dotVar);
    }

    public List<DataPoint> getDotList() 
    {
        Debug.Log("there is " + dotList.Count + " coordinates");
        return dotList;
    }
    public void DestroyAllDot() //temp func, switch to poolable when done
    {
        foreach (DataPoint dot in dotList) 
        {
            Destroy(dot.gameObject);
        }
        dotList.Clear();
    }

    public void ChangeEndDot(CordPoint cordPoint) 
    {
        dotList[dotList.Count].rectTransform.anchoredPosition = cordPoint.type == 0 ? calCord.ConvertToRedCord(cordPoint) : calCord.ConvertToRedCord(cordPoint);
        Debug.Log("DOT ALTERED");
    }
}
