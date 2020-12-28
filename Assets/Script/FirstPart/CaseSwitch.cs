using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class CaseSwitch : MonoBehaviour
{
    public DataManager dataManager;
    public SavedLogs savedLogs;
    public Communication comm;
    public LogsUI logsUI;

    public GameObject ReadUI;
    public GameObject LogUI;
    private void Start()
    {
        comm = Communication.Instance;
    }

    public void FixedUpdate()
    {
        if (comm.uartData.IsOpen) 
        {
            ReadCordId();
        }
    }
    public void ReadCordId() 
    {
        
        if (comm.uartData.IsOpen) 
        {
            try
            {
                switch (comm.convertedData.id)
                {
                    case 0:
                        break; //Data is thrown as its not needed
                    case 1:
                        dataManager.ReceiveCord(comm.convertedData);
                        break; //Data is added to the DataMananger for checking + inserting purposes
                    case 2:
                        comm.StopReadingData();
                        savedLogs.SaveLog(dataManager.SendTempCordList());
                        logsUI.GenerateLogs(savedLogs.GetLog());
                        //Main.something.id = 0; this shouldnt matter
                        break; //Data is thrown, and all state is resetted
                }
            }
            catch (System.Exception e) { Debug.Log("No Data Detected"); }
        }
            
        
        
    }

    public void ChangeUI(int insertEnum) 
    {
        switch (insertEnum) 
        {
            case 0:
                LogUI.SetActive(true);
                ReadUI.SetActive(false);
                break;
            case 1:
                ReadUI.SetActive(true);
                ReadUI.SetActive(false);
                break;
            case 2:
                //insert change to SpectateUI
                break;
        } 

    }
}
