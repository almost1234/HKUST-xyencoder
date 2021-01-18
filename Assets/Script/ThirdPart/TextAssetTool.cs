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
    public static Dictionary<string, List< CordPoint>> CreateCoordinateDictionary(string textData)
    {
        Debug.Log(textData);
        Dictionary<string, List< CordPoint>> temp = new Dictionary<string, List< CordPoint>>();
        if (textData == "") { Debug.LogError("no save file detected!"); return temp; } //terrible
        string[] firstParse = textData.Split('\n');// Seperation for first line (,) to get maxTime, Date, Dictionary<float, CordPoint>
        foreach (string data in firstParse) 
        {
            string[] secondParse = data.Split(',');
            temp.Add(secondParse[0], new List<CordPoint>());
            string[] thirdParse = secondParse[1].Split(';');// Seperation of second line (;) to get all string of float time + CordPoint in json
            foreach (string cord in thirdParse)
            {
                string[] fourthParse = cord.Split(':');// seperation of third line (:) to get the float, JsonCordPoint
                temp[secondParse[0]].Add(new CordPoint(int.Parse(fourthParse[0]), int.Parse(fourthParse[1]), float.Parse(fourthParse[2]), float.Parse(fourthParse[3]), float.Parse(fourthParse[4]), float.Parse(fourthParse[5]), float.Parse(fourthParse[6]), float.Parse(fourthParse[7]), float.Parse(fourthParse[8])));// this too
            }//This extraction for CordPoint can  be dynamiced via json formatting
        } 
        return temp; 
    }

    public static string CreateStringDictionary(Dictionary<string, List<CordPoint>> data) //this is a very poor way to write, need somebody opinion
    {
        string temp = "";
        foreach (KeyValuePair<string, List<CordPoint>> nextData in data) 
        {
            string dataLine = "";
            dataLine += nextData.Key + ','; 
            string cordLine = "";
            foreach (CordPoint cordData in nextData.Value)  //It can be dynamiced via forloop + function from cordpoint maybe
            {
                cordLine += cordData.type + ":" + cordData.id + ":" + cordData.x1 + ":" + cordData.y1 + ":" + cordData.x2 + ":" + cordData.y2 + ":"+ cordData.velocity + ":" + cordData.expectedVelocity + ":"+ cordData.time.ToString() + ";";
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
