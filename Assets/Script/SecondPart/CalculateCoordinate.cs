using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateCoordinate : MonoBehaviour
{
    public RectTransform redArea;
    public RectTransform blueArea;

    public float realWidth;
    public float realHeight;

    public Vector2 redAreaSize;
    public Vector2 blueAreaSize;
    //Vector2 size is width, height
    public void Awake()
    {
        redAreaSize = redArea.sizeDelta;
        blueAreaSize = blueArea.sizeDelta;
    }
    public Vector2 ConvertToRedCord(CordPoint cord) 
    {
        Debug.Log("THE CORD IS " + cord.x1 + "/" + cord.y1);
        float xCord = ((cord.x1 / realWidth) * redAreaSize.x) - (redAreaSize.x/2) ;
        float yCord = ((cord.y1/ realHeight) * redAreaSize.y)-(redAreaSize.y / 2);
        Debug.Log("THE CORD IS " + xCord + "/" + yCord);
        return new Vector2(xCord, yCord);
    }

    public Vector2 ConvertToBlueCord(CordPoint cord) 
    {
        Debug.Log("THE CORD IS " + cord.x1 + "/" + cord.y1);
        float xCord = (-(cord.x1 / realWidth) * blueAreaSize.x) + (blueAreaSize.x / 2);
        float yCord = ((cord.y1 / realHeight) * blueAreaSize.y) - (blueAreaSize.y / 2);
        return new Vector2(xCord, yCord);
    }


}

public enum FieldArea 
{
    blue,red
}