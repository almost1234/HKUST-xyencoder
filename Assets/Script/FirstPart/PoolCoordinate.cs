using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolCoordinate : MonoBehaviour
{
    public Dictionary<string, Queue<GameObject> > poolDict;

    [System.Serializable]
    public class PoolItem 
    {
        public string name;
        public int NumberOfCall;
        public GameObject objectCalled;
    }
    public List<PoolItem> poolList;

    public void CallPoolList(List<PoolItem> poolList) 
    {
        poolDict = new Dictionary<string, Queue<GameObject>>();
        foreach (PoolItem data in poolList) 
        {
            Queue<GameObject> newQueue = new Queue<GameObject>();
            for (int i = 0; i < data.NumberOfCall; i++) 
            {
                
            }
        }
    }


}
