using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bancadas : MonoBehaviour, InterfaceInteractable {

    //Aqui temos um getter que pega o prompt passado no quadro de pedidos 
    public string InteractionPrompt => Prompt;
    [SerializeField] private string Prompt;
    
    private Vector3 itemPosition;

    void Start() {
        itemPosition = transform.position;
        itemPosition.y += 1;

        gameObject.tag = "Bancada";
        gameObject.layer = LayerMask.NameToLayer("Interactable");
    }

    public bool Interact(Interactor interactor, GameObject item = null) {
        Comidas comida = item.GetComponent<Comidas>();
        if(comida != null){
            if(Input.GetKeyDown(KeyCode.Space) && comida.itemIsPicked == true) {
                // comida.rb.AddForce(interactor.transform.forward * 1f);
                comida.transform.parent = null;
                comida.transform.position = itemPosition;
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
