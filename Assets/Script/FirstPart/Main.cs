using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System;

public class Main : MonoBehaviour
{
    //basically the thing that runs idk fuck me and my coding skills
    public Communication comm;
    public string[] testSample;
    public CaseSwitch caseSwitch;
    public static Thread readThread;
    public static CordPoint something; 
    
    public void CallCommunication() 
    {
        try
        {
            comm.uartData.Open();
            readThread = new Thread(testFunction);
            readThread.Start();
        }
        catch (Exception e) { Debug.LogError("There no proper COM selected"); }; // insert warning error here
    }
    public void testFunction() 
    {
        try {
            while (comm.uartData.IsOpen)
            {
                something = JsonReader.ConvertToCordPoint(comm.ReadData());
            }
        }
        catch(TimeoutException e) 
        {
            Debug.LogError("The comm didnt send any message, please retry again");
            comm.uartData = null;
            readThread.Abort();
        }; // insert warningUI
        
    }
}
