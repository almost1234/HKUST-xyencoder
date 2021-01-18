using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dictionaryButton : MonoBehaviour
{
    public LineRenderer testLine;
    public Color something = new Color(1,0,0,0.2f);
    public void test() 
    {
        something.a = 1;
        Vector3[] testPos = new Vector3[] { new Vector3(100, 100, 0), new Vector3(0, 0, 0) };
        testLine.SetPositions(testPos);
        testLine.startColor = something;
        testLine.endColor = something;
    }
}
