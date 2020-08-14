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
    }
    public void GenerateLogs(Dictionary<string[], Dictionary<float,CordPoint>> dataPoints) 
    {
        DestroyLogs();
        Debug.LogError("The number of datapoints is " + dataPoints.Count);
        foreach (KeyValuePair<string[], Dictionary<float,CordPoint>> data in dataPoints) 
        {
            
            logDateText.text = data.Key[1];
            Button logButton = Instantiate(log, logsGroup).GetComponent<Button>();
            logButton.onClick.AddListener(delegate
            {
                dotUI.DestroyAllDot();
                Debug.LogError ("The number of coordinates generated is " + data.Value.Count);
                foreach (KeyValuePair<float, CordPoint> cord in data.Value)
                {
                    dotUI.GenerateDot(cord.Key, cord.Value);
                }
                caseSwitch.ChangeUI(uiState.specUI);
                spectateUI.SpectateButtonSetup(float.Parse(data.Key[0]), new Dictionary<float, DataPoint>(dotUI.getDotList()));
                
                Debug.LogWarning("Called replay: " + data.Key[1]);
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
