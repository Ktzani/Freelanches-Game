using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadroPedidos : MonoBehaviour, InterfaceInteractable
{
    [SerializeField] private string Prompt;
    // [SerializeField] private string Type;
    public string InteractionPrompt => Prompt; //Aqui temos um getter que pega o prompt passado no quadro de pedidos 
    // public string InteractableType => Type;

    public bool Interact(Interactor interactor, GameObject item = null)
    {   
        //Aqui criamos alguns parametros que devem se corresponder para que a interaçao seja um sucesso, como por exemplo
        //se o jogador possui ou nao um pedido
        var pedidos = interactor.GetComponent<Pedidos>();  
        
        if(pedidos == null) return false;

        if(pedidos.JaPossuiPedido == false) {
            Debug.Log("Abrindo quadro de pedidos !");
            return true;
        }

        Debug.Log("Jogador ja possui pedido em mãos");
        return false;
    }
}
