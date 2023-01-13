using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bancadas : MonoBehaviour, InterfaceInteractable
{
    [SerializeField] private string Prompt;
    // [SerializeField] private string Type;
    public string InteractionPrompt => Prompt; //Aqui temos um getter que pega o prompt passado no quadro de pedidos 
    // public string InteractableType => Type;

    public bool Interact(Interactor interactor, GameObject item = null)
    {   
        Comidas comida = item.GetComponent<Comidas>();
        if(comida != null){
            if(Input.GetKeyDown(KeyCode.Space) && comida.itemIsPicked == true) {
                comida.rb.AddForce(interactor.transform.forward * 1f);
                comida.transform.parent = null;
                item.GetComponent<Rigidbody>().useGravity = true;
                item.GetComponent<BoxCollider>().enabled = true;
                comida.itemIsPicked = false;
                comida.Grounded = true;
                return true;
            }
        }

        return false;
    }
}