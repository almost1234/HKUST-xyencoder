using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommunicationIndicatorUI : MonoBehaviour
{
    public static Image indicatorComm;
    //insert animation 
    public void Awake()
    {
        indicatorComm = gameObject.GetComponent<Image>();

    }
}

public enum CommIndicatorState 
{
    idle, reading, stop
}