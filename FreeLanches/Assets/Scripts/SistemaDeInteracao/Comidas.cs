using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comidas : MonoBehaviour, InterfaceInteractable
{
    [SerializeField] private string Prompt;
    public string InteractionPrompt => Prompt; //Aqui temos um getter que pega o prompt passado no quadro de pedidos 
    // public string InteractableType => this.GetType().Name;
    [SerializeField] public bool readyToThrow = false;
    [SerializeField] public bool itemIsPicked = false; //FAZER GETTER E SETTERS PARA ESSES ATRIBUTOS E COMPONENTES
    [SerializeField] public Rigidbody rb;

    public bool Interact(Interactor interactor, GameObject item = null)
    {   
        //Aqui criamos alguns parametros que devem se corresponder para que a interaçao seja um sucesso, como por exemplo
        //se o jogador possui ou nao um pedido
        var pedidos = interactor.GetComponent<Pedidos>();  
        
        if(pedidos == null) return false;

        if(pedidos.JaPossuiPedido == false) {
            Debug.Log("Pedido ja foi selecionado");

            if(Input.GetKeyDown(KeyCode.Space) && itemIsPicked == false){
                GetComponent<Rigidbody>().useGravity = false;
                GetComponent<MeshCollider>().enabled = false;
                this.transform.position = interactor.PickUpPoint.position;
                this.transform.parent = GameObject.Find("PickUpPoint").transform;

                itemIsPicked = true;
                interactor.forceMultiplier = 0;
            }

            return true;
        }

        
        Debug.Log("Ainda não foi selecionado um pedido");
        return false;
    }


}
