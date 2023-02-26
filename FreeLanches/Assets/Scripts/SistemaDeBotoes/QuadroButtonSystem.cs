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
    private GameObject PedidoEscolhido;
    public bool PedidoPego;

    public void Start()
    {   
        PedidoPego = false;
        PedidoEscolhido = null;
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
        setPedidoEscolhido(pedido);
        PedidoPego = true;

        foreach(Transform child in ChildsPedidos){
            if(child.gameObject.GetComponent<Button>().interactable == false){
                child.gameObject.GetComponent<Button>().interactable = true;
            }
        }

        pedido.GetComponent<Button>().interactable = false;
        pedido.GetComponent<InterfacePedidos>().MontandoOrdemIngredientes(resumoDoPedido);
    }

    public void setPedidoEscolhido(GameObject pedido){
        this.PedidoEscolhido = pedido;
    }

    public void instantiatePedido(){
        Debug.Log("Parou aqui 2");
        Transform pedidoFeito = PedidoEscolhido.transform.GetChild(2);
        Comidas Comida = pedidoFeito.gameObject.GetComponent<Comidas>();
        Transform PickUpPoint = FindObjectOfType<Interactor>().PickUpPoint;
        if(Comida.itemIsPicked == false && Comida.Grounded){
            Instantiate(pedidoFeito, PickUpPoint);
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<BoxCollider>().enabled = false;
            pedidoFeito.parent = GameObject.Find("PickUpPoint").transform;
            Comida.itemIsPicked = true;
            Comida.Grounded = false;
        }
    }
}
