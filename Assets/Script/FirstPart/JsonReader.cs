using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class JsonReader : MonoBehaviour
{
    //I just realized how useless this script

    public static CordPoint ConvertToCordPoint(string json) 
    {
        Debug.Log("Decrypting on");
        try
        { 
            return JsonUtility.FromJson<CordPoint>(json); 
        }
        catch (ArgumentException)
        { Debug.Log("There are some error with the code, dumping it");
            return new CordPoint(0, 0, 0, 0, 0 ,0, 0, 0, 0);
        }
    }
   
}
