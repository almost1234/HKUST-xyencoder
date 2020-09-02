using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class LineGraph : MonoBehaviour
{
    public LineGraphUI lineGraphUI;

    public LineRenderer[] lineGraph; // need to assign it
    public RectTransform graph;
    public Vector2 graphCord;
    public float maxXValue;
    public float maxYValue;
    public float roofBuffer;

    public List<Vector3[]> dataCompiled = new List<Vector3[]>();
    public List<LineGraphDataSelection> tempList = new List<LineGraphDataSelection>() { LineGraphDataSelection.velocity, LineGraphDataSelection.xtime, LineGraphDataSelection.ytime, LineGraphDataSelection.xy };
    public Dictionary<LineGraphDataSelection, float[]> maxValueData;
    public Dictionary<LineGraphDataSelection, List<Vector3[]>> positionData; 
    public Dictionary<string, List<CordPoint>> temp;


    //Create the data compiled to be Dictionary<float[], List<Vector3>>
    /*public void Awake()
    {
        graphCord = graph.sizeDelta;
        maxValueData = new Dictionary<LineGraphDataSelection, float[]>();
        positionData = new Dictionary<LineGraphDataSelection, List<Vector3[]>>();
        Button[] tempVar = lineGraphUI.GetButtonList();
        foreach (LineGraphDataSelection selection in tempList) 
        {
            int index = tempList.IndexOf(selection);
            tempVar[index].onClick.AddListener(delegate 
            {
                CaseSwitchLineRenderer(selection);
                lineGraphUI.DisableButtonAnimation(tempVar[index]);
            });
        }
        
    }*/

    public void Start()
    {
        graphCord = graph.sizeDelta;
        maxValueData = new Dictionary<LineGraphDataSelection, float[]>();
        positionData = new Dictionary<LineGraphDataSelection, List<Vector3[]>>();
        Button[] tempVar = lineGraphUI.GetButtonList();
        foreach (LineGraphDataSelection selection in tempList)
        {
            int index = tempList.IndexOf(selection);
            tempVar[index].onClick.AddListener(delegate
            {
                ButtonFunction(selection, tempVar[index]);
            });
        }
        Debug.LogWarning("FUKIN LOAD");
    }

    public virtual void ButtonFunction(LineGraphDataSelection selection, Button button) //THIS SHOULD BE IN THE UI SECTION STEVEN
    {
        CaseSwitchLineRenderer(selection);
        lineGraphUI.DisableButtonAnimation(button);
    }

    public void CompileAllDataType(List<CordPoint> data) //try make this async for each function idk how
    {
        maxValueData.Clear();
        positionData.Clear();
        CompileCordDataVelocity(data);
        CompileCordDataXTime(data);
        CompileCordDataXY(data);
        CompileCordDataYTime(data);
        Debug.LogError("Line Data Renderered");
        gameObject.SetActive(false);
    }
    public void CompileCordDataVelocity(List<CordPoint> data) //idk how to make this a semi generic kind
    {

        int numberOfData = data.Count;
        Vector3[] realDataCompiled = new Vector3[numberOfData];
        Vector3[] acutalDataCompiled = new Vector3[numberOfData];
        maxXValue = data[numberOfData - 1].time;
        maxYValue = data[numberOfData - 1].velocity > data[numberOfData - 1].expectedVelocity ? data[numberOfData - 1].velocity : data[numberOfData - 1].expectedVelocity;
        for (int i = 0; i < numberOfData; i++)
        {
            realDataCompiled[i] = new Vector3(AdjustXCord(data[i].time), AdjustYCord(data[i].velocity));
            acutalDataCompiled[i] = new Vector3(AdjustXCord(data[i].time), AdjustYCord(data[i].expectedVelocity));
        }
        maxValueData.Add(LineGraphDataSelection.velocity, new float[2] { maxXValue, maxYValue });
        positionData.Add(LineGraphDataSelection.velocity, new List<Vector3[]>() { realDataCompiled, acutalDataCompiled });
    }

    public void CompileCordDataXY(List<CordPoint> data) //idk how to make this a semi generic kind
    {
        int numberOfData = data.Count;
        Vector3[] realDataCompiled = new Vector3[numberOfData];
        Vector3[] acutalDataCompiled = new Vector3[numberOfData];
        maxXValue = data[numberOfData - 1].x1 > data[numberOfData - 1].x2 ? data[numberOfData - 1].x1 : data[numberOfData - 1].x2;
        maxYValue = data[numberOfData - 1].y1 > data[numberOfData - 1].y2 ? data[numberOfData - 1].y1 : data[numberOfData - 1].y2;
        for (int i = 0; i < numberOfData; i++)
        {
            realDataCompiled[i] = new Vector3(AdjustXCord(data[i].x1), AdjustYCord(data[i].y1));
            acutalDataCompiled[i] = new Vector3(AdjustXCord(data[i].x2), AdjustYCord(data[i].y2));
        }
        maxValueData.Add(LineGraphDataSelection.xy, new float[2] { maxXValue, maxYValue });
        positionData.Add(LineGraphDataSelection.xy, new List<Vector3[]>() { realDataCompiled, acutalDataCompiled });
    }

    public void CompileCordDataXTime(List<CordPoint> data) //idk how to make this a semi generic kind
    {
        int numberOfData = data.Count;
        Vector3[] realDataCompiled = new Vector3[numberOfData];
        Vector3[] acutalDataCompiled = new Vector3[numberOfData];
        maxXValue = data[numberOfData - 1].time;
        maxYValue = data[numberOfData - 1].x1 > data[numberOfData - 1].x2 ? data[numberOfData - 1].x1 : data[numberOfData - 1].x2;
        for (int i = 0; i < numberOfData; i++)
        {
            realDataCompiled[i] = new Vector3(AdjustXCord(data[i].time), AdjustYCord(data[i].x1));
            acutalDataCompiled[i] = new Vector3(AdjustXCord(data[i].time), AdjustYCord(data[i].x2));
        }
        maxValueData.Add(LineGraphDataSelection.xtime, new float[2] { maxXValue, maxYValue });
        positionData.Add(LineGraphDataSelection.xtime, new List<Vector3[]>() { realDataCompiled, acutalDataCompiled });
    }

    public void CompileCordDataYTime(List<CordPoint> data) //idk how to make this a semi generic kind
    {
        int numberOfData = data.Count;
        Vector3[] realDataCompiled = new Vector3[numberOfData];
        Vector3[] acutalDataCompiled = new Vector3[numberOfData];
        maxXValue = data[numberOfData - 1].time;
        maxYValue = data[numberOfData - 1].y1 > data[numberOfData - 1].y2 ? data[numberOfData - 1].y1 : data[numberOfData - 1].y2;
        for (int i = 0; i < numberOfData; i++)
        {
            realDataCompiled[i] = new Vector3(AdjustXCord(data[i].time), AdjustYCord(data[i].y1));
            acutalDataCompiled[i] = new Vector3(AdjustXCord(data[i].time), AdjustYCord(data[i].y2));
        }
        maxValueData.Add(LineGraphDataSelection.ytime, new float[2] { maxXValue, maxYValue });
        positionData.Add(LineGraphDataSelection.ytime, new List<Vector3[]>() { realDataCompiled, acutalDataCompiled });
    }

    public void DrawLineFromData(List<Vector3[]> dataCompiled)
    {
        Debug.LogError(dataCompiled[0].Length);
        for (int i = 0; i < lineGraph.Length; i++) 
        {
            lineGraph[i].positionCount = dataCompiled[i].Length;
            lineGraph[i].SetPositions(dataCompiled[i]);
        }
        lineGraph[0].SetPositions(dataCompiled[0]);
        lineGraph[1].SetPositions(dataCompiled[1]);
    }

    public float AdjustXCord(float data)
    {
        return (data / maxXValue) * graphCord.x - (graphCord.x / 2);
    }

    public float AdjustYCord(float data) 
    {
        return (data / maxYValue) * graphCord.y - (graphCord.y / 2);
    }

    public void CaseSwitchLineRenderer(LineGraphDataSelection selection) 
    {
        DrawLineFromData(positionData[selection]);
        lineGraphUI.SetMaxAxisValue(maxValueData[selection][0], maxValueData[selection][1]);
    }
}

public enum LineGraphDataSelection 
{
    velocity,xy,xtime,ytime
}