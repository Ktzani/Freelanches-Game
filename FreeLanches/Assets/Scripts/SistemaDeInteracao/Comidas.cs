using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comidas : MonoBehaviour, InterfaceInteractable
{
    [SerializeField] private string Prompt;
    public string InteractionPrompt => Prompt; //Aqui temos um getter que pega o prompt passado no quadro de pedidos 

    // [SerializeField] public bool readyToThrow = false;
    [SerializeField] public bool itemIsPicked = false; //FAZER GETTER E SETTERS PARA ESSES ATRIBUTOS E COMPONENTES
    [SerializeField] public Rigidbody rb;
    private Vector3 StartPosition;
    public bool Grounded = true;

    void Start()
    {
        StartPosition = transform.position;
    }

    public bool Interact(Interactor interactor, GameObject item = null)
    {   
        //Aqui criamos alguns parametros que devem se corresponder para que a interaçao seja um sucesso, como por exemplo
        //se o jogador possui ou nao um pedido
        var pedidos = interactor.GetComponent<Pedidos>();  
        
        if(pedidos == null) return false;

        if(pedidos.JaPossuiPedido == false ) {
            Debug.Log("Pedido ja foi selecionado");

            if(Input.GetKeyDown(KeyCode.Space) && itemIsPicked == false && Grounded){
                GetComponent<Rigidbody>().useGravity = false;
                GetComponent<BoxCollider>().enabled = false;
                this.transform.position = interactor.PickUpPoint.position;
                this.transform.parent = GameObject.Find("PickUpPoint").transform;
                itemIsPicked = true;
                Grounded = false;
            }

            return true;
        }

        
        Debug.Log("Ainda não foi selecionado um pedido");
        return false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Plataforma")){
            transform.position = StartPosition;
            rb.constraints = RigidbodyConstraints.FreezeAll;
            Grounded = true;
        }
    }

}
