using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DotDataUI : MonoBehaviour
{
    public RectTransform dataBox;
    private void OnMouseOver()
    {
        dataBox.gameObject.SetActive(true);
    }

    private void OnMouseExit()
    {
        dataBox.gameObject.SetActive(false);
    }
}
