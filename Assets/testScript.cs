using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System;

public class testScript : MonoBehaviour
{
    // Start is called before the first frame update
    SerialPort uartData;
    public string portName;
    public Int32 baudRate;

    private void Awake()
    {
        uartData = new SerialPort(portName, baudRate, Parity.None, 8, StopBits.One);
    }

    public void ReadValue()
    { 
            uartData.Open();
            Debug.Log(uartData.ReadTo("}"));
            uartData.Close();
            Debug.Log("Done");
    }
}
