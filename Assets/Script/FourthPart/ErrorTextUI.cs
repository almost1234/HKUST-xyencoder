using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Security.Policy;
using UnityEngine;
using UnityEngine.UI;

public class ErrorTextUI : MonoBehaviour
{
    public Text errorMessage;
    public GameObject textBoxError;
    public Button closeButton;
   
    public void Awake()
    {
        closeButton.onClick.AddListener(ClosePopUp);
        CommunicationUI.addErrorMessage += PopOutErrorUI;
    }

    public void PopOutErrorUI(string text) 
    {
        errorMessage.text = text;
        textBoxError.SetActive(true);
    }

    public void ClosePopUp() 
    {
        textBoxError.SetActive(false);
    }
}
