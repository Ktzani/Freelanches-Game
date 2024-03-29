using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PratoMontagem : MonoBehaviour, InterfaceInteractable
{
    [SerializeField] private string Prompt;
    public string InteractionPrompt => Prompt;
    private List<GameObject> Ingredientes;
    [SerializeField] private GameObject ResumoPedidos;
    [SerializeField] private GameObject QuadroPedidos;
    [SerializeField] private IngredienteIncorretoUI ingredienteIncorreto;

    void Start() {
        gameObject.tag = "PratoMontagem";
        gameObject.layer = LayerMask.NameToLayer("Interactable");
    }
    public bool Interact(Interactor interactor, GameObject itemCarregado = null) {   
        Comidas comida = itemCarregado.GetComponent<Comidas>();
 
        if(comida != null && Ingredientes != null){
            if(Ingredientes.Count > 0){
                if(Input.GetKeyDown(KeyCode.Space) && comida.itemIsPicked == true) {
                    if(itemCarregado.name == Ingredientes[0].name){
                        comida.transform.parent = null;
                        comida.transform.position = comida.StartPosition;
                        itemCarregado.GetComponent<Rigidbody>().useGravity = true;
                        itemCarregado.GetComponent<BoxCollider>().enabled = true;
                        comida.itemIsPicked = false;
                        comida.Grounded = true;     
                        Debug.Log(Ingredientes[0].name);
                        Ingredientes.RemoveAt(0);
                        FindObjectOfType<IngredientesResumoDoPedido>().deletaIngrediente();
                        if (Ingredientes.Count == 0) {
                            QuadroButtonSystem quadroButtonSystem = QuadroPedidos.GetComponent<QuadroButtonSystem>();
                            if(quadroButtonSystem != null){
                                quadroButtonSystem.instantiatePedido();
                            }
                            ResumoPedidos.SetActive(false);
                            return false;
                        }
                        return true;
                    }

                    else{
                        StartCoroutine (ApareceUIIngredienteIncorreto(2.5f));
                        Debug.Log("Alerta -> " + Ingredientes[0].name + " -> " + itemCarregado.name);       
                    }
                }
            }
        }
        return false;
    }

    public void setIngredientes(List<GameObject> ingredientes){
        this.Ingredientes = new List<GameObject>(ingredientes);
    }
    

    IEnumerator ApareceUIIngredienteIncorreto (float tempo){
        ingredienteIncorreto.SetUp();
        yield return new WaitForSeconds(tempo);
        ingredienteIncorreto.Close();
    }
}
