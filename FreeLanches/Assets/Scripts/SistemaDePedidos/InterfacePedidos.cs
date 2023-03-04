using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface InterfacePedidos
{
    public void MontandoOrdemIngredientes(Canvas ResumoPedido);

    public List<GameObject> getIngredientes();
}
