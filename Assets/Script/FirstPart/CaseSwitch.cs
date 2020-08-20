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
    public TestDotUI testDotUI;
    public SpectateUI spectateUI;


    public GameObject fieldUI;
    public GameObject commUI;
    public GameObject specUI;
    public GameObject logUI;
    private void Awake()
    {
        Communication.callReadCord += ReadCordId;
    }

    public void FixedUpdate()
    {
        
        if (comm.uartData != null && comm.uartData.IsOpen) 
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
                switch (Main.something.id)
                {
                    case 0:
                        break; //Data is thrown as its not needed
                    case 1:
                        dataManager.ReceiveCord(Main.something);
                        break; //Data is added to the DataMananger for checking + inserting purposes
                    case 2:
                        Communication.AbortComm();
                        comm.ChangePortState();
                        savedLogs.SaveLog(dataManager.SendTempCordList());
                        logsUI.GenerateLogs(savedLogs.GetLog());
                        Main.something.id = 0;
                        break; //Data is thrown, and all state is resetted
                }
            }
            catch (System.Exception e) { Debug.Log("No Data Detected"); }
        }
            
        
        
    }

    public void ChangeUI(uiState state) 
    {
        switch (state) 
        {
            case uiState.commUI:
                fieldUI.SetActive(true);
                logUI.SetActive(false);
                commUI.SetActive(true);
                specUI.SetActive(false);
                testDotUI.DestroyAllDot();
                break;
            case uiState.logsUI:
                fieldUI.SetActive(false);
                logUI.SetActive(true);
                testDotUI.DestroyAllDot();
                spectateUI.ClearDotList();
                break;
            case uiState.specUI:
                fieldUI.SetActive(true);
                logUI.SetActive(false);
                commUI.SetActive(false);
                specUI.SetActive(true);
                break;
        } 

    }

    public void BackToField() //lazy temp func
    {
        ChangeUI(uiState.logsUI);
    }

    public void BackToComm() 
    {
        ChangeUI(uiState.commUI);
    }
}

public enum uiState 
{
    commUI, logsUI, specUI
}
