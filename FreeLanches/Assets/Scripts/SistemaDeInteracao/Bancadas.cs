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
        interactor.forceMultiplier = 10f;
        if(comida != null){
            if(Input.GetKeyDown(KeyCode.Space) && comida.itemIsPicked == true) {
                // comida.readyToThrow = true;
                comida.rb.AddForce(interactor.transform.forward * interactor.forceMultiplier);
                comida.transform.parent = null;
                item.GetComponent<Rigidbody>().useGravity = true;
                item.GetComponent<MeshCollider>().enabled = true;
                comida.itemIsPicked = false;
                interactor.forceMultiplier = 0;
                // comida.readyToThrow = false;
                return true;
            }
        }

        return false;
    }
}
