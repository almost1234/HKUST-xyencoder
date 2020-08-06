using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class TextFile : MonoBehaviour
{
    public string textFilePath;
    public Button testButton;
    public SavedLogs savedLogs;

    public void Awake()
    {
        testButton.onClick.AddListener(delegate { WriteText(savedLogs.GetLog()); });
    }
    public async void WriteText(Dictionary<string[], Dictionary<float, CordPoint>> data) 
    {
        StreamWriter textFile = new StreamWriter("C:\\Users\\Steven\\Desktop\\UI TEST PRACTICE\\Practicegame\\UART\\Assets\\Resources\\Text\\textFilePath.txt", false);//idk how to create an error check
        await textFile.WriteAsync(TextAssetTool.CreateStringDictionary(data));
        textFile.Close();
        Debug.LogWarning("Finished");
    }
    /*
    public Dictionary<string[], Dictionary<float, CordPoint>> ReadText() 
    {
        TextAsset textFile = Resources.Load<>
    }
    */
}
