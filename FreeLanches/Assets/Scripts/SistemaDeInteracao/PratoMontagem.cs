using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PratoMontagem : MonoBehaviour, InterfaceInteractable
{
    [SerializeField] private string Prompt;
    public string InteractionPrompt => Prompt;
    private List<GameObject> Ingredientes;
    [SerializeField] private GameObject ResumoPedidos;

    void Start() {
        gameObject.tag = "PratoMontagem";
        gameObject.layer = LayerMask.NameToLayer("Interactable");
    }
    public bool Interact(Interactor interactor, GameObject itemCarregado = null) {   
        Comidas comida = itemCarregado.GetComponent<Comidas>();

        if(comida != null && Ingredientes.Count > 0){
            if(Input.GetKeyDown(KeyCode.Space) && comida.itemIsPicked == true) {
                if(itemCarregado.name == Ingredientes[Ingredientes.Count - 1].name){
                    comida.transform.parent = null;
                    comida.transform.position = comida.StartPosition;
                    itemCarregado.GetComponent<Rigidbody>().useGravity = true;
                    itemCarregado.GetComponent<BoxCollider>().enabled = true;
                    comida.itemIsPicked = false;
                    comida.Grounded = true;     
                    Ingredientes.RemoveAt(Ingredientes.Count - 1);
                    FindObjectOfType<IngredientesResumoDoPedido>().deletaIngrediente();
                    if (Ingredientes.Count == 0) {
                        Debug.Log("Parou aqui 1");
                        FindObjectOfType<QuadroButtonSystem>().instantiatePedido();
                        ResumoPedidos.SetActive(false);
                        return false;
                    }
                    return true;
                }

                else{
                    Debug.Log("Alerta");       
                }
            }
        }
        return false;
    }

    public void setIngredientes(List<GameObject> ingredientes){
        foreach (GameObject i in ingredientes) {
            this.Ingredientes.Add(Instantiate(i));
        }
    }
}
