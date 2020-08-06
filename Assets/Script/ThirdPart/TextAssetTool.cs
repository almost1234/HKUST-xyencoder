using System.Collections;
using System.Collections.Generic;
using TMPro.SpriteAssetUtilities;
using UnityEngine;

public class TextAssetTool : MonoBehaviour
{
    public static string[] ReadAndExtractText(string pathfile)
    {
        TextAsset textFile = Resources.Load<TextAsset>(pathfile);// the pathfile should based in resources
        if (textFile == null) { Debug.LogError("No text detected for reading!"); return null; }
        string content = textFile.text;
        string[] rows = content.Split('\n');
        return rows;
    }

    /*<summary>
     Data format : maxTime, Date, cordTime1 : type: id: x: y ; cordTime2 : type : id: x: y ; cordTime3 : type: id: x: y and so on \n 
     */
    public static Dictionary<string[], Dictionary<float, CordPoint>> CreateCoordinateDictionary(string textData)
    {
        Dictionary<string[], Dictionary<float, CordPoint>> temp = new Dictionary<string[], Dictionary<float, CordPoint>>();
        string[] firstParse = textData.Split('\n');// Seperation for first line (,) to get maxTime, Date, Dictionary<float, CordPoint>
        foreach (string data in firstParse) 
        {
            Debug.Log(data);
            string[] secondParse = data.Split(',');
            Debug.LogFormat("Second parse : {0}^{1}^{2}", secondParse);
            string[] initialKey = new string[2] { secondParse[0], secondParse[1] };
            temp.Add(initialKey, new Dictionary<float, CordPoint>());
            string[] thirdParse = secondParse[2].Split(';');// Seperation of second line (;) to get all string of float time + CordPoint in json
            foreach (string cord in thirdParse)
            {
                Debug.Log(cord);
                string[] fourthParse = cord.Split(':');// seperation of third line (:) to get the float, JsonCordPoint
                Debug.LogFormat("{0},{1},{2},{3},{4}", fourthParse);
                temp[initialKey].Add(float.Parse(fourthParse[0]), new CordPoint(int.Parse(fourthParse[1]), int.Parse(fourthParse[2]), float.Parse(fourthParse[3]), float.Parse(fourthParse[4])));// this too
            }
            Debug.Log("DATA ENTERED WITH DATE: " + secondParse[1]);
        } 
        return temp; //check if it will get referenced or not
    }

    public static string CreateStringDictionary(Dictionary<string[], Dictionary<float, CordPoint>> data) //this is a very poor way to write, need somebody opinion
    {
        string temp = "";
        foreach (KeyValuePair<string[], Dictionary<float, CordPoint>> nextData in data) 
        {
            string dataLine = "";
            foreach (string text in nextData.Key) 
            {
                dataLine += text + ','; 
            }
            string cordLine = "";
            foreach (KeyValuePair<float, CordPoint> cordData in nextData.Value) 
            {
                cordLine += cordData.Key.ToString() + ":" + cordData.Value.type + ":" + cordData.Value.id + ":" + cordData.Value.x + ":" + cordData.Value.y + ";";
            }
            cordLine = cordLine.Remove(cordLine.Length-1);
            cordLine += "\n";
            temp += dataLine + cordLine;
        }
        temp = temp.Remove(temp.Length - 1);
        Debug.LogWarning("STRING FORMAT HAS BEEN GENERATED, CONTINUE WRITING - " + data.Count);
        Debug.Log(temp);
        return temp;
    }
}
