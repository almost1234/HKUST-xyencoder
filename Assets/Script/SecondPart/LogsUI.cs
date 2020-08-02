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

    public Text logDateText;

    public void Awake()
    {
        logDateText = log.GetComponentInChildren<Text>();
    }
    public void GenerateLogs(Dictionary<string, List<CordPoint>> dataPoints) 
    {
        DestroyLogs();
       
        foreach(KeyValuePair<string, List<CordPoint>> data in dataPoints) 
        {
            logDateText.text = data.Key;
            if (data.Value == null) 
            {
                Debug.LogError("This bitch empty");

            }
            Button logButton = Instantiate(log, logsGroup).GetComponent<Button>();
            logButton.onClick.AddListener(delegate
            {
                dotUI.DestroyAllDot();
                Debug.Log("DOT DELETED");
                foreach (CordPoint cord in data.Value)
                {
                    dotUI.GenerateDot(cord);
                }
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
