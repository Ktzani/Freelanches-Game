using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerBar : MonoBehaviour
{      
    private Image timerBar;
    private float maxTime;
    private float tempoRestante;
    private GameObject pedido; 

    // Start is called before the first frame update
    void Start()
    {   
        timerBar = GetComponent<Image>();
        maxTime = 0;
    }

    // Update is called once per frame
    void Update()
    {   
        pedido = transform.parent.gameObject;

        if(pedido != null){
            maxTime = pedido.GetComponent<HamburguerTradicional>().TempoMaximoPedido;
            tempoRestante = pedido.GetComponent<HamburguerTradicional>().TempoPedido;

            if(tempoRestante > 0){
                timerBar.fillAmount = tempoRestante / maxTime;
            }

            else{
                Time.timeScale = 0;
            }
        }
    }
}
