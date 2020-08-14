using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dictionaryButton : MonoBehaviour
{
    public LineRenderer testLine;
    public void test() 
    {
        Vector3[] testPos = new Vector3[] { new Vector3(100, 100, 0), new Vector3(0, 0, 0) };
        testLine.SetPositions(testPos);
    }
}
