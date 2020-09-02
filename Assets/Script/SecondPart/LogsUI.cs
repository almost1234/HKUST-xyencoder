using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogsUI : MonoBehaviour
{
    public SavedLogs savedLogs;
    public Transform logsGroup;
    public TestDotUI dotUI;
    public List<Transform> logsGroupList; //poolsite
    public GameObject log; // prepare a pool for this too
    public SpectateUI spectateUI;
    public LineGraph lineGraph;
    public Text logDateText;
    public CaseSwitch caseSwitch;

    public GameObject LogUI;
    public GameObject FieldUI;
    public void Awake()
    {
        logDateText = log.GetComponentInChildren<Text>();
    }
    public void Start()
    {
        GenerateLogs(savedLogs.GetLog());
        if (lineGraph == null) 
        {
            Debug.LogWarning("NO LINE");
        }
    }
    public void GenerateLogs(Dictionary<string, List<CordPoint>> dataPoints) 
    {
        DestroyLogs();
        Debug.LogError("The number of datapoints is " + dataPoints.Count);
        foreach (KeyValuePair<string, List<CordPoint>> data in dataPoints) 
        {
            
            logDateText.text = data.Key;
            Button logButton = Instantiate(log, logsGroup).GetComponent<Button>();
            logButton.onClick.AddListener(delegate
            {
                dotUI.DestroyAllDot();
                foreach (CordPoint cord in data.Value)
                {
                    dotUI.GenerateDot(cord);
                }
                caseSwitch.ChangeUI(uiState.specUI);
                float tempTime = data.Value[data.Value.Count - 1].time;
                Debug.LogError("THE TIME VALUE EXIST WITH " + tempTime);
                spectateUI.SpectateButtonSetup(new List<DataPoint>(dotUI.getDotList())); //this kinda need a bitta rework
                lineGraph.CompileAllDataType(new List<CordPoint>(data.Value));//Need to make a delegate with spectate for readability
                spectateUI.UpdateSpectateUI(tempTime); // call once just to update the shit
                
                Debug.LogWarning("Called replay: " + data.Key);
                //provide the data and point it was generated?
            }
            );
        }
    }

    public void DestroyLogs() 
    {
        Debug.Log("Replacing with " + logsGroupList.Count + " logs");
        foreach (Transform log in logsGroup) 
        {
            Destroy(log.gameObject);
        }
    }
    
    
}
