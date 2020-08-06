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
    public Communication comm;

    public void Awake()
    {
        selectComm.onValueChanged.AddListener(OnUpdateDropdown);
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

    public void OnUpdateDropdown(int value) 
    {
        comm.portName = selectComm.options[value].text;
        SerialPort test = new SerialPort(comm.portName, comm.baudRate, Parity.None, 8, StopBits.One);
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
        Debug.Log(comm.portName + " is receiving the JSON value, using...");
        comm.uartData = test;
        test.Close();
    }


}
