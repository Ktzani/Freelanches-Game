using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractionPromptUI : MonoBehaviour
{
    private Camera MainCamera;
    [SerializeField] private GameObject UiPanel;
    [SerializeField] private TextMeshProUGUI PromptText;
    public bool IsDisplayed = false;

    void Start()
    {
        MainCamera = Camera.main;
        UiPanel.SetActive(false); //Evitar de vermos o painel UI assim que iniciarmos o jogo
    }

    private void LateUpdate()
    {
        var rotation = MainCamera.transform.rotation;
        transform.LookAt(transform.position + rotation * Vector3.forward, rotation * Vector3.up); 
    }

    public void SetUp(string promptText){
        PromptText.text = promptText;
        UiPanel.SetActive(true);
        IsDisplayed = true;
    }

    public void Close(){
        UiPanel.SetActive(false);
        IsDisplayed = false;
    }
}
 