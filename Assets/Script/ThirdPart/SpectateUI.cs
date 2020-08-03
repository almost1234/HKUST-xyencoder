using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpectateUI : MonoBehaviour
{
    public Slider timeSlider;
    public float previousSetTime;
    public List<Dictionary<float, RectTransform>> spectateDotData;
    //The time slider will be linked to function to see the dots at the certain point
    //possibly, the button must send the generatedlist for the Dictionary<float, CordPoint>

    public void Awake()
    {
        timeSlider.onValueChanged.AddListener(UpdateSpectateUI);
        spectateDotData = new List<Dictionary<float, RectTransform>>();

    }
    public void SetMaxValue(float time)
    {
        timeSlider.maxValue = time;
        timeSlider.value = time;
        previousSetTime = time;
        Debug.LogWarning("TIME SET");
    }

    public void UpdateSpectateUI(float currentTime)
    {
        foreach (Dictionary<float, RectTransform> rectList in spectateDotData)
        {
            foreach (KeyValuePair<float, RectTransform> data in rectList)
            {
                data.Value.gameObject.SetActive(data.Key < currentTime ? true : false);
                if (currentTime < previousSetTime)
                {
                    if (data.Key > previousSetTime)
                    {
                        break;
                    }
                }
                else
                {
                    if (data.Key > currentTime)
                    {
                        break;
                    }
                }
            }
        }

    }

    public void SetDotList(Dictionary<float, RectTransform> data)
    {
        spectateDotData.Add(data);
    }

    public void SpectateButtonSetup(float time, Dictionary<float, RectTransform> dotGroup)
    {
        SetMaxValue(time);
        if (dotGroup == null)
        {
            Debug.LogWarning("THERE NO FUKIN data");
        }
        SetDotList(dotGroup);
        Debug.LogWarning("Data is set ");
    }
    public void ClearDotList()
    {
        spectateDotData.Clear();
    }
}
