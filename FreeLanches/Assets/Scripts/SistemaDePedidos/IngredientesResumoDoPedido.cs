using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientesResumoDoPedido : MonoBehaviour
{
    public void deletaIngrediente(){
        Transform ingrediente = transform.GetChild(0);
        Destroy(ingrediente.gameObject);
    }
}

