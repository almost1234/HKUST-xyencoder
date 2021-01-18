using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineGraphUI : MonoBehaviour
{
    public Text maxXAxis;
    public Text maxYAxis;

    [SerializeField]
    private Button[] buttonList;

    public void SetMaxAxisValue(float maxX, float maxY) 
    {
        maxXAxis.text = maxX.ToString();
        maxYAxis.text = maxY.ToString();
    }

    public Button[] GetButtonList() 
    {
        return buttonList;
    }

    public void DisableButtonAnimation(Button dataButton) 
    {
        foreach (Button button in buttonList) 
        {
            if (button == dataButton)
            {
                button.interactable = false;
            }
            else 
            {
                button.interactable = true;
            }
        }
    }
}
