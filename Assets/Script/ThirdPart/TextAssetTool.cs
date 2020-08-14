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
        Debug.Log(textData);
        Dictionary<string[], Dictionary<float, CordPoint>> temp = new Dictionary<string[], Dictionary<float, CordPoint>>();
        if (textData == "") { Debug.LogError("no save file detected!"); return temp; } //terrible
        string[] firstParse = textData.Split('\n');// Seperation for first line (,) to get maxTime, Date, Dictionary<float, CordPoint>
        foreach (string data in firstParse) 
        {
            string[] secondParse = data.Split(',');
            string[] initialKey = new string[2] { secondParse[0], secondParse[1] };
            temp.Add(initialKey, new Dictionary<float, CordPoint>());
            string[] thirdParse = secondParse[2].Split(';');// Seperation of second line (;) to get all string of float time + CordPoint in json
            foreach (string cord in thirdParse)
            {
                string[] fourthParse = cord.Split(':');// seperation of third line (:) to get the float, JsonCordPoint
                temp[initialKey].Add(float.Parse(fourthParse[0]), new CordPoint(int.Parse(fourthParse[1]), int.Parse(fourthParse[2]), float.Parse(fourthParse[3]), float.Parse(fourthParse[4]), float.Parse(fourthParse[5]), float.Parse(fourthParse[6])));// this too
            }//This extraction for CordPoint can be dynamiced via json formatting
        } 
        return temp; 
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
            foreach (KeyValuePair<float, CordPoint> cordData in nextData.Value)  //It can be dynamiced via forloop + function from cordpoint maybe
            {
                cordLine += cordData.Key.ToString() + ":" + cordData.Value.type + ":" + cordData.Value.id + ":" + cordData.Value.x1 + ":" + cordData.Value.y1 + ":" + cordData.Value.x2 + ":" + cordData.Value.y2 + ";";
            }
            cordLine = cordLine.Remove(cordLine.Length-1);
            cordLine += "\n";
            temp += dataLine + cordLine;
        }
        if (temp == null) { Debug.LogError("no file to write detected!"); return temp; }
        temp = temp.Remove(temp.Length - 1);
        Debug.Log(temp);
        return temp;
    }
}
