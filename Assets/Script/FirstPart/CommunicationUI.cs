using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.IO;

public class CommunicationUI : MonoBehaviour
{
    public Dropdown selectComm;

    public Dropdown selectBaud;
    private int baudRate = 9600;

    public void Awake()
    {
        selectComm.onValueChanged.AddListener(OnUpdateDropdown);
        selectBaud.onValueChanged.AddListener(UpdateBaudOption);
    }

    public void UpdateCommOptions() 
    {
        selectComm.ClearOptions();
        List<string> commOption = new List<string>() { "None" };
        foreach (string comm in SerialPort.GetPortNames()) 
        {
            commOption.Add(comm);
        }
        selectComm.AddOptions(commOption);
    }

    private void UpdateBaudOption(int value)
    {
        baudRate = int.Parse(selectBaud.options[value].text);
        Debug.Log("Current Baud rate is selected at " + baudRate);
    }

    public void OnUpdateDropdown(int value) 
    {
        string portName = selectComm.options[value].text;
        SerialPort test = new SerialPort(portName, baudRate, Parity.None, 8, StopBits.One);
        test.ReadTimeout = 500;
        try { test.Open(); }
        catch (IOException e)
        {
            Debug.LogWarning("The specified port does not exist!");// iinsert warning Retry port again
            return;
        }
        try { test.ReadTo("}"); }
        catch (TimeoutException e)
        {
            Debug.LogWarning("The comm doesnt receive any data please choose another one");
            test.Close();
            return;
        }
        catch (IOException e) 
        {
            Debug.LogWarning("The specified port does not exist!");
            test.Close();
            return;
        }
        Debug.Log(portName + " is receiving the JSON value, using...");
        Communication.Instance.uartData = test;
        test.Close();
    }


}
