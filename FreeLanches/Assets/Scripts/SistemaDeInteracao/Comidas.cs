using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comidas : MonoBehaviour, InterfaceInteractable
{
    [SerializeField] private string Prompt;
    public string InteractionPrompt => Prompt; //Aqui temos um getter que pega o prompt passado no quadro de pedidos 

    [SerializeField] public bool itemIsPicked = false; //FAZER GETTER E SETTERS PARA ESSES ATRIBUTOS E COMPONENTES
    [SerializeField] public Rigidbody rb;
    private Vector3 StartPosition;
    [SerializeField] public bool Grounded = true;

    void Start() {
        StartPosition = transform.position;

        gameObject.tag = "Comida";
        gameObject.layer = LayerMask.NameToLayer("Interactable");
    }

    void FixedUpdate() {
        if (transform.position.y < -10) {
            transform.position = StartPosition;
            rb.constraints = RigidbodyConstraints.FreezeAll;
            Grounded = true;
        }
    }

    public bool Interact(Interactor interactor, GameObject item = null){   
        
        Debug.Log("Item pego");

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

    void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.CompareTag("Plataforma")){
            transform.position = StartPosition;
            rb.constraints = RigidbodyConstraints.FreezeAll;
            Grounded = true;
        }
    }

}
