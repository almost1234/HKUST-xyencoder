using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System;
using System.Threading;

public class Communication : MonoBehaviour
{
    public SerialPort uartData;
    public string portName;
    public int baudRate;
    //insert more data like parity, bit and stopbit
    public static string receivedData;
    public delegate void ReadCord();
    public static event ReadCord callReadCord;
    public static Thread readThread;

    public void Start()
    {
        uartData = new SerialPort();
    }
    public static void AbortComm() 
    {
        readThread.Abort();
    }
    public void CallCommunication()
    {
        try
        {
            uartData.Open();
            readThread = new Thread(CallReadComm);
            readThread.Start();
        }
        catch (Exception e) { Debug.LogError("There no proper COM selected"); }; // insert warning error here
    }
    public void CallReadComm()
    {
        try
        {
            while (uartData.IsOpen)
            {
                Main.something = JsonReader.ConvertToCordPoint(ReadData());
                readThread.Suspend();
            }
        }
        catch (TimeoutException e)
        {
            Debug.LogError("The comm didnt send any message, please retry again");
            uartData = null;
            readThread.Abort();
        }; // insert warningUI

    }
    public string ReadData()
    {
        string varidk = uartData.ReadTo("}") + "}";
        Debug.Log(varidk);
        return varidk;
    }
    public void ChangePortState() 
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
    }


}
