using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IngredienteIncorretoUI : MonoBehaviour
{
    private Camera MainCamera;
    [SerializeField] private GameObject UiPanel;
    private bool displayed = false;

    public bool IsDisplayed() {
        return displayed;
    }

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

    public void SetUp(){
        UiPanel.SetActive(true);
        displayed = true;
    }

    public void Close(){
        UiPanel.SetActive(false);
        displayed = false;
    }
}
 