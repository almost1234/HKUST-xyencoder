using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class DynamicLineGraph : LineGraph
{
    public List<CordPoint> bufferData;
    public int bufferThreshold;
    public int ticksThreshold;
    public LineGraphDataSelection currentSelection = LineGraphDataSelection.velocity;

    public int ticks = 0;
    public void Awake()
    {
        CaseSwitch.OnUpdateCordData += UpdateBufferList;
    }

    public void FixedUpdate()
    {
        if (ticks == ticksThreshold)
        {
            UpdateLineRenderer(currentSelection);
            ticks = 0;
        }
        else { ticks++; }
    }
    public void UpdateBufferList(CordPoint data) 
    {
        if (bufferData == null) { bufferData = new List<CordPoint>(); }
        if (bufferData.Count == bufferThreshold) 
        {
            bufferData.RemoveAt(0);
        }
        bufferData.Add(data);
    }

    public override void ButtonFunction(LineGraphDataSelection selection, Button button)
    {
        currentSelection = selection;
        lineGraphUI.DisableButtonAnimation(button);
    }
    public void UpdateLineRenderer(LineGraphDataSelection selection) 
    {
        if (bufferData == null) { return; }
        maxValueData.Clear(); // Bad implementation
        positionData.Clear();
        switch (selection) 
        {
            case LineGraphDataSelection.velocity:
                CompileCordDataVelocity(bufferData);
                break;
            case LineGraphDataSelection.xtime:
                CompileCordDataXTime(bufferData);
                break;
            case LineGraphDataSelection.xy:
                CompileCordDataXY(bufferData);
                break;
            case LineGraphDataSelection.ytime:
                CompileCordDataYTime(bufferData);
                break;

        }
        CaseSwitchLineRenderer(selection);
    }
}
