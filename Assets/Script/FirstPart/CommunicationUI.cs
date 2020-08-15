using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.IO;
using UnityEngine.Experimental.PlayerLoop;

public class CommunicationUI : MonoBehaviour
{
    public Dropdown selectComm;
    public Communication comm;
    public delegate void CallErrorUI(string text);
    public static event CallErrorUI addErrorMessage;

    public void Awake()
    {
        selectComm.onValueChanged.AddListener(OnUpdateDropdown);
        UpdateCommOptions();
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
        try 
        { 
            test.Open();
            test.ReadTo("}");
        }
        catch (Exception e)
        {
            Debug.LogError("The error is " + e.ToString());
            switch (e.GetType().ToString()) 
            {
                case "System.TimeoutException":
                    SendErrorMessage("The comm doesnt receive any data please choose another one");
                    break;
                case "System.IO.IOException":
                    SendErrorMessage("The specified port does not exist!");
                    break;
            }
            test.Close();
            return;
        }
        Debug.Log(comm.portName + " is receiving the JSON value, using...");
        comm.uartData = test;
        test.Close();
    }

    public void SendErrorMessage(string text) 
    {
        addErrorMessage?.Invoke(text);
    }


}
