using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HamburguerTradicional : MonoBehaviour, InterfacePedidos
{   
    [SerializeField] private List<GameObject> Ingredientes;
    public void MontandoOrdemIngredientes(Canvas ResumoPedido){
            //Antes disso é bom colocar a ordem dos ingredientes na forma correta, ja que na lista eles podem vir desordenados

            Transform ChildBackgroud = ResumoPedido.gameObject.transform.Find("Backgroud");
            Transform ChildPedido = ChildBackgroud.Find("Pedido");
            Transform ResumoIngredientesChild = ChildPedido.Find("Ingredientes");

            if(ResumoIngredientesChild != null){
                Transform[] childs = ResumoIngredientesChild.GetComponentsInChildren<Transform>();
                
                //Igual a 1 pois a funçao GetComponentsInChildren também pega o proprio pai
                if(childs.Length == 1){
                    foreach (GameObject ingrediente in Ingredientes){   
                        Instantiate(ingrediente, ResumoIngredientesChild);
                    }
                }

                else {
                    foreach (Transform child in childs){
                        if(child.CompareTag("Ingrediente"))
                            Destroy(child.gameObject);
                    }

                    foreach (GameObject ingrediente in Ingredientes){   
                        Instantiate(ingrediente, ResumoIngredientesChild);
                    }
                }
            }

            ResumoPedido.gameObject.SetActive(true);
            FindObjectOfType<PratoMontagem>().setIngredientes(Ingredientes);
    }
}
