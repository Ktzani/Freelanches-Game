using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedidos : MonoBehaviour
{
    public bool JaPossuiPedido = false;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q)) JaPossuiPedido = !JaPossuiPedido;
    }
}
