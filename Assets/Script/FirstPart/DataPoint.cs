using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DataPoint : MonoBehaviour
{
    public CordPoint coordinate;
    public LineRenderer lineRenderer;
    public RectTransform rectTransform;
    public bool initialized = false;

    private void Awake() //this shit doesnt work cuz its not active
    {
        coordinate = new CordPoint(0, 0, 0, 0, 0, 0);
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        rectTransform = gameObject.GetComponent<RectTransform>();
    }

    public void SetCord(CordPoint cord) 
    {
        coordinate = cord;
    }

    /*public void SetData(CordPoint newCord)  //future set data : need to set the b and actual cord + generation of linerenderer
    {
        coordinate.x = newCord.x;
        coordinate.y = newCord.y;
        Debug.Log("New Coordinate initialzied");
    }*/

    public void SetData(Vector2 dotPosition, Vector3[] linePosition) //calCord will be improved in the future (Change to static)
    {
        rectTransform.anchoredPosition = dotPosition;
        lineRenderer.SetPositions(linePosition);
        Debug.Log("Data is set?");
    }
}

public struct CordPoint 
{
    public int type;
    public int id;
    public float x1;
    public float y1;
    public float x2;
    public float y2;

    public CordPoint(int type, int id, float xCord, float yCord, float xCordB, float yCordB) 
    {
        this.type = type;
        this.id = id;
        this.x1 = xCord;
        this.y1 = yCord;
        this.x2 = xCordB;
        this.y2 = yCordB;
    }
}