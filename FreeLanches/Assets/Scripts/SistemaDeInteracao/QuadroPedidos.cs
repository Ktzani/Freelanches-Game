using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuadroPedidos : MonoBehaviour, InterfaceInteractable {
    
    [SerializeField] private string Prompt;
    public Canvas quadroPedidos;
    public bool displayed = false;
    public string InteractionPrompt => Prompt;

    public GameObject pedidoUI;

    private List<Pedido> pedidos;

    void Start() {
        pedidos = new List<Pedido>();
        pedidos.Add(new Pedido("Hamburguer 1"));
        pedidos.Add(new Pedido("Hamburguer 2"));
        pedidos.Add(new Pedido("Hamburguer 3"));
        pedidos.Add(new Pedido("Hamburguer 4"));
        pedidos.Add(new Pedido("Hamburguer 5"));
        pedidos.Add(new Pedido("Hamburguer 6"));
        pedidos.Add(new Pedido("Hamburguer 7"));
    }

    public bool Interact(Interactor interactor, GameObject item = null) {   
        if(!displayed) {
            displayed = true;
            ColocaPedidoQuadro();
        }
        else {
            displayed = false;
        }
        quadroPedidos.gameObject.SetActive(displayed);
        
        return false;
    }

    void ColocaPedidoQuadro() {
        int x = -450, y = 300;
        for (var i = 0; i <  pedidos.Count; i++) {
            var texto = Instantiate(pedidoUI, quadroPedidos.gameObject.transform);
            texto.GetComponent<RectTransform>().localPosition = new Vector3(x, y, 0);
            texto.GetComponent<TextMeshProUGUI>().text = pedidos[i].getName();
            x += 250;
            if (x == 550) {
                x = -450;
                y -= 200;
            }
        }
    }
}
