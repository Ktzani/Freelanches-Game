using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EntregaPedidoUI : MonoBehaviour
{
    private Camera MainCamera;
    [SerializeField] private GameObject UiPanel;
    [SerializeField] private GameObject Seta;
    private bool displayed = false;

    public bool IsDisplayed() {
        return displayed;
    }

    void Start()
    {
        MainCamera = Camera.main;
        UiPanel.SetActive(false); //Evitar de vermos o painel UI assim que iniciarmos o jogo
        Seta.SetActive(false); //Evitar de vermos o painel UI assim que iniciarmos o jogo
    }

    private void LateUpdate()
    {
        var rotation = MainCamera.transform.rotation;
        transform.LookAt(transform.position + rotation * Vector3.forward, rotation * Vector3.up); 
    }

    public void SetUp(){
        UiPanel.SetActive(true);
        Seta.SetActive(true);
        displayed = true;
    }

    public void Close(){
        UiPanel.SetActive(false);
        Seta.SetActive(false);
        displayed = false;
    }
}
 