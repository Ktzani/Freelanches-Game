using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadroPedidos : MonoBehaviour, InterfaceInteractable
{
    [SerializeField] private string Prompt;
    public Canvas quadroPedidos;
    public bool displayed = false;
    public string InteractionPrompt => Prompt;

    public bool Interact(Interactor interactor, GameObject item = null) {   
        if(!displayed) {
            displayed = true;
        }
        else {
            displayed = false;
        }
        
        quadroPedidos.gameObject.SetActive(displayed);
        
        return false;
    }
}
