using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System;

public class Communication : MonoBehaviour
{
    public SerialPort uartData;
    public string portName;
    public int baudRate;
    //insert more data like parity, bit and stopbit
    public static string receivedData;
    public delegate void ReadCord();
    public static event ReadCord callReadCord;
    private void Awake()
    {
        uartData = new SerialPort(portName, baudRate, Parity.None, 8, StopBits.One);
        if (uartData != null) 
        {
            Debug.LogError("There serial port detected!");
        }
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
