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
    private Interactor interactor;
    public void Start()
    {   
        interactor = FindObjectOfType<Interactor>();
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
        interactor.PedidoPego = true;

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

    public void entregaPedidoEscolhido(){
        PedidoEscolhido.SetActive(false);
        PedidoEscolhido = null;
        interactor.PedidoFeito = false;
        interactor.PedidoPego = false;
    }
    
    public void instantiatePedido(){
        Transform pedidoFeito = PedidoEscolhido.transform.GetChild(2);
        Transform PickUpPoint = interactor.PickUpPoint;
        Transform ClonePedidoFeito = Instantiate(pedidoFeito, PickUpPoint);
        Comidas Comida = ClonePedidoFeito.gameObject.GetComponent<Comidas>();
        if(Comida.itemIsPicked == false && Comida.Grounded){
            interactor.setCarriableItem(ClonePedidoFeito.gameObject);
            ClonePedidoFeito.GetComponent<Rigidbody>().useGravity = false;
            ClonePedidoFeito.GetComponent<BoxCollider>().enabled = false;
            Comida.itemIsPicked = true;
            Comida.Grounded = false;
            interactor.PedidoFeito = true;
        }
    }
}
