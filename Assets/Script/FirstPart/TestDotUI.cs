using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDotUI : MonoBehaviour
{
    public List<RectTransform> dotList;
    public GameObject sampleDotGenerate;
    public CalculateCoordinate calCord;
    public Transform RedGroup;
    public Transform BlueGroup;

    public void Awake()
    {
        dotList = new List<RectTransform>();
    }
    public void GenerateDot(CordPoint cordPoint)
    {
        switch (cordPoint.type) 
        {
            case 0:
                //Red Side coordinate
                RectTransform redVar = Instantiate(sampleDotGenerate, RedGroup).GetComponent<RectTransform>();
                redVar.anchoredPosition = calCord.ConvertToRedCord(cordPoint);
                dotList.Add(redVar);
                Debug.Log("NEW RED DOT GENERATED");
                break;
            case 1:
                //Blue Side coordinate
                RectTransform blueVar = Instantiate(sampleDotGenerate, BlueGroup).GetComponent<RectTransform>();
                blueVar.anchoredPosition = calCord.ConvertToBlueCord(cordPoint);
                dotList.Add(blueVar);
                Debug.Log("NEW BLUE DOT GENERATED");
                break;
        }
    }

    public void DestroyAllDot() //temp func, switch to poolable when done
    {
        foreach (RectTransform dot in dotList) 
        {
            Destroy(dot.gameObject);
            Debug.Log("I FUKIN DED");
        }
        dotList.Clear();
    }

    public void ChangeEndDot(CordPoint cordPoint) 
    {
        dotList[dotList.Count].anchoredPosition = cordPoint.type == 0 ? calCord.ConvertToRedCord(cordPoint) : calCord.ConvertToRedCord(cordPoint);
        Debug.Log("DOT ALTERED");
    }
}
