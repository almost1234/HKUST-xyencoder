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
        float xCord = ((cord.x / realWidth) * redAreaSize.x) - (redAreaSize.x/2) ;
        float yCord = ((cord.y/ realHeight) * redAreaSize.y)-(redAreaSize.y / 2);
        return new Vector2(xCord, yCord);
    }

    public Vector2 ConvertToBlueCord(CordPoint cord) 
    {
        float xCord = (-(cord.x / realWidth) * blueAreaSize.x) + (blueAreaSize.x / 2);
        float yCord = ((cord.y / realHeight) * blueAreaSize.y) - (blueAreaSize.y / 2);
        return new Vector2(xCord, yCord);
    }


}

public enum FieldArea 
{
    blue,red
}