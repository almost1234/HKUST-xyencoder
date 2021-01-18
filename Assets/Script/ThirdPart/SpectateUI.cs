using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpectateUI : MonoBehaviour
{
    public Slider timeSlider;
    public float previousSetTime;
    public Text timeText;
    public List<DataPoint> spectateDotData;
    public float dotLifetime;
    //The time slider will be linked to function to see the dots at the certain point
    //possibly, the button must send the generatedlist for the Dictionary<float, CordPoint>

    public void Awake()
    {
        timeSlider.onValueChanged.AddListener(UpdateSpectateUI);
        spectateDotData = new List<DataPoint>();

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
        
            foreach (DataPoint data in spectateDotData)
            {
                if (data.coordinate.time < currentTime && currentTime - dotLifetime < data.coordinate.time) // if the dot is in range of currentTime and dotLifeTime
                {
                    data.gameObject.SetActive(true);
                    data.SetLineAlpha((dotLifetime - (currentTime - data.coordinate.time)) / dotLifetime);

                }
                else 
                {
                    data.gameObject.SetActive(false);
                }
                //data.Value.gameObject.SetActive(data.Key < currentTime ? true : false);
                if (currentTime < previousSetTime) //To break the dotcheck earlier (Since this is listed now, i can technically use index to point which dot to which dot for performance)
                {   //if the currentTime is smaller than previous time, check if the dot has surpassed previous time
                    if (data.coordinate.time > previousSetTime) // U need to update until the previousSetTime at least
                    {
                        previousSetTime = data.coordinate.time;
                        break;
                    }
                }
                else
                {
                    //if the currentTime larger than previous time, check if the dot has surpassed currentTime
                    if (data.coordinate.time > currentTime)
                    {
                        previousSetTime = data.coordinate.time;
                        break;
                    }
                }
            }
        //TODO: Steven of the past, I really dont know how you can update the previousSetTime, i feel like you never tested this feature since u dont feel the lag
        //So Imma just add some time stuff to update it at least
        timeText.text = Mathf.RoundToInt(currentTime / 60).ToString() + " : " + Mathf.RoundToInt(currentTime % 60).ToString();

    }

    public void SetDotList(List<DataPoint> data)
    {
        spectateDotData =  data;
    }

    public void SpectateButtonSetup(List<DataPoint> dotGroup)
    {
        SetMaxValue(dotGroup[dotGroup.Count -1].coordinate.time);
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
