using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.Threading;
using System;

public class Communication : MonoBehaviour
{
    public SerialPort uartData;
    public string receivedData;
    public CordPoint convertedData;

    private Thread readThread;
    public static Communication Instance = null;

    private Communication()
    {
        uartData = new SerialPort(); //creating placeholder to prevent null (actually can improve this)
    }

    public void Awake()
    {
        Instance = new Communication();//singleton created
    }

    public void ReadData()
    {
        string receivedData = uartData.ReadTo("}") + "}";
        convertedData = JsonReader.ConvertToCordPoint(receivedData);
    }

    /*public void ChangePortState() 
    {
        if (uartData.IsOpen)
        {
            Debug.LogWarning("PORT CLOSE");
            uartData.Close();
        }
        else 
        {
            Debug.LogWarning("PORT OPEN");
            uartData.Open();
        }
    }*/

    public void CallCommunication()
    {
        try
        {
            uartData.Open();
            readThread = new Thread(InitiateReadingData);
            readThread.Start();
        }
        catch (Exception e) { Debug.LogError("There no proper COM selected"); }; // insert warning error here
    }

    public void InitiateReadingData()
    {
        try
        {
            while (uartData.IsOpen)
            {
                ReadData();
            }
        }
        catch (TimeoutException e) { Debug.LogError("The comm didnt send any message, please retry again"); }; // insert warningUI

    }
    public void StopReadingData()
    {
        if (uartData.IsOpen)
        {
            readThread.Abort();
            uartData.Close();
        }
    }
}
