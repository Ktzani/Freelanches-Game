using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuadroButtonSystem : MonoBehaviour
{
    // public Canvas quadroPedidosUI; //Esse script já esta no canvas do quadro de pedidos
    public Canvas resumoDoPedido;
    public GameObject quadroPedidos;
    public GameObject cozinheiro;
    public Transform FatherQuadro;
    private Transform[] objects;
    private List<Transform> ChildsPedidos;

    public void Start()
    {   
        ChildsPedidos = new List<Transform>();
        objects = FatherQuadro.GetComponentsInChildren<Transform>();

        foreach (Transform item in objects){
            if(item.CompareTag("Pedido")){
                ChildsPedidos.Add(item);
            }
        }
    }

    public void closeBoard(){
        if (quadroPedidos.GetComponent<QuadroPedidos>().displayed == true){
            quadroPedidos.GetComponent<QuadroPedidos>().displayed = false;
            this.gameObject.SetActive(false);
        }

        if(cozinheiro.GetComponent<Interactor>().PodeMover == false){
            cozinheiro.GetComponent<Interactor>().PodeMover = true;
            cozinheiro.GetComponent<Interactor>().scriptCozinheiro.enabled = true;
        }
    }

    public void getAnyPedido(GameObject pedido){
        closeBoard();

        //COMO FAZER ISSOOOO???? Acho que por conta de ChildsPedidos serem um conjunto de transform e nao um conjunto de gameObjects
        foreach(Transform child in ChildsPedidos){
            if(child.gameObject.GetComponent<Button>().interactable == false){
                child.gameObject.GetComponent<Button>().interactable = true;
            }
        }

        pedido.GetComponent<Button>().interactable = false;
        
        resumoDoPedido.gameObject.SetActive(true);
    }
}
