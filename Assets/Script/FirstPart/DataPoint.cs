using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPoint : MonoBehaviour
{
    public CordPoint coordinate;

    private void Awake()
    {
        coordinate = new CordPoint(0,0,0,0);
    }

    public void SetData(CordPoint newCord) 
    {
        coordinate.x = newCord.x;
        coordinate.y = newCord.y;
        Debug.Log("New Coordinate initialzied");
    }
}

public struct CordPoint 
{
    public int type;
    public int id;
    public float x;
    public float y;

    public CordPoint(int type, int id, float xCord, float yCord) 
    {
        this.type = type;
        this.id = id;
        this.x = xCord;
        this.y = yCord;
    }
}