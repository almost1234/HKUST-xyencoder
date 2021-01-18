using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class DataPoint : MonoBehaviour
{
    public CordPoint coordinate;
    public LineRenderer lineRenderer;
    public RectTransform rectTransform;
    public bool initialized = false;
    public Image dotImage;
    public Color lineColor = new Color(1, 0, 0, 0);
    public Color imageColor = new Color(1, 1, 1, 1);

    private void Awake() //this shit doesnt work cuz its not active
    {
        //questioning the legitamacy of this bullshit
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        rectTransform = gameObject.GetComponent<RectTransform>();
    }

    public void SetCord(CordPoint cord) 
    {
        coordinate = cord;
    }

    public void SetLineAlpha(float value) 
    {
        lineColor.a = value;
        imageColor.a = value;
        dotImage.color = imageColor;
        lineRenderer.startColor = lineColor;
        lineRenderer.endColor = lineColor;
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
    public float velocity;
    public float expectedVelocity;
    public float time;

    public CordPoint(int type, int id, float xCord, float yCord, float xCordB, float yCordB, float velocity, float expectedVelocity, float time) 
    {
        this.type = type;
        this.id = id;
        this.x1 = xCord;
        this.y1 = yCord;
        this.x2 = xCordB;
        this.y2 = yCordB;
        this.velocity = velocity;
        this.expectedVelocity = expectedVelocity;
        this.time = time;
    }
}