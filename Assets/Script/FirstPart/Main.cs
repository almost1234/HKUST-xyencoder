using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class Main : MonoBehaviour
{
    //basically the thing that runs idk fuck me and my coding skills
    public Communication comm;
    public string[] testSample;
    public CaseSwitch caseSwitch;
    public static Thread readThread;
    public static CordPoint something; 
    public void Start()
    {
        
        
    }

    public void CallCommunication() 
    {
        comm.uartData.Open();
        readThread = new Thread(testFunction);
        readThread.Start();
    }
    public void testFunction() 
    {
        while (comm.uartData.IsOpen) 
        {
            something = JsonReader.ConvertToCordPoint(comm.ReadData());
        }
    }
}
