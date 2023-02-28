using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BancadaEntrega : MonoBehaviour, InterfaceInteractable
{
    [SerializeField] private string Prompt;
    public string InteractionPrompt => Prompt;
    [SerializeField] private GameObject QuadroPedidos;

    void Start() {
        gameObject.tag = "BancadaEntrega";
        gameObject.layer = LayerMask.NameToLayer("Interactable");
    }
    public bool Interact(Interactor interactor, GameObject item = null) {   
        QuadroButtonSystem quadroButton = QuadroPedidos.GetComponent<QuadroButtonSystem>();
        if(quadroButton != null){
            quadroButton.entregaPedidoEscolhido();
            Destroy(item);
            
            return true;
        }

        return false;
    }
}
