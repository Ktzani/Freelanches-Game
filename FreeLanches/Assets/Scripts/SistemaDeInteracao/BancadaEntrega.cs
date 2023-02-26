using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BancadaEntrega : MonoBehaviour, InterfaceInteractable
{
    [SerializeField] private string Prompt;
    public string InteractionPrompt => Prompt;

    void Start() {
        gameObject.tag = "BancadaEntrega";
        gameObject.layer = LayerMask.NameToLayer("Interactable");
    }
    public bool Interact(Interactor interactor, GameObject item = null) {   
        Comidas comida = item.GetComponent<Comidas>();

        if(comida != null){
            if(Input.GetKeyDown(KeyCode.Space) && comida.itemIsPicked == true) {
                comida.transform.parent = null;
                comida.transform.position = comida.StartPosition;
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
