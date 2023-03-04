using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 
using TMPro;

public class GameManager : MonoBehaviour
{

    public Canvas telaDeFim;
    public Canvas telaDePause;
    public float Segundos;
    public int Minutos;
    public TextMeshProUGUI TextSegundos;
    public TextMeshProUGUI TextMinutos;
    public int PontuacaoDuranteJogo;
    public TextMeshProUGUI TextPontuacaoDuranteJogo;
    public int PontuacaoFimJogo;
    public TextMeshProUGUI TextPontuacaoFimJogo;
    public Canvas quadroPedidos;
    private int NumeroDePedidos;
    private int NumeroIngredientesPedidoEscolhido;
    private bool FimDaFase;

    // Start is called before the first frame update
    void Start()
    {   
        FimDaFase = false;
        PontuacaoDuranteJogo = 0;
        PontuacaoFimJogo = 0;
        NumeroDePedidos = -1;
        NumeroIngredientesPedidoEscolhido = -1;
    } 

    void FixedUpdate()
    {   
        TextSegundos.text = Segundos.ToString("00");
        TextMinutos.text = Minutos.ToString("00");
        TextPontuacaoDuranteJogo.text = PontuacaoDuranteJogo.ToString();
        TextPontuacaoFimJogo.text = PontuacaoFimJogo.ToString();

        if(!FimDaFase){
            Segundos -= Time.deltaTime;
            PontuacaoDuranteJogo += 100;
        }

        else{
            PontuacaoFimJogo = PontuacaoDuranteJogo;
        }

        if(Segundos <= 0f && !FimDaFase){
            if(Minutos == 0 || Segundos == 0f){
                Minutos = 0;
                Segundos = 0f;
                FimDaFase = true;
            }

            else{
                Minutos--;
                Segundos = 59.0f;
            }
            
        }


    }

    // Update is called once per frame
    void Update()
    {
        List<Transform> Pedidos = quadroPedidos.GetComponent<QuadroButtonSystem>().getPedidos();
        if(Pedidos != null){
            NumeroDePedidos = Pedidos.Count;
        }

        GameObject PedidoEscolhido = quadroPedidos.GetComponent<QuadroButtonSystem>().getPedidoEscolhido(); 
        if(PedidoEscolhido != null){
            NumeroIngredientesPedidoEscolhido = PedidoEscolhido.GetComponent<InterfacePedidos>().getIngredientes().Count;
        }

        if(Input.GetKeyDown(KeyCode.R)){
            ReiniciarPartida(); 
        }

        if(Input.GetKeyDown(KeyCode.End)){
            SairDoJogo();
        }  

        if((Minutos == 0 && Segundos == 0) || (NumeroDePedidos == 0)){
            TelaDeVitoria();
        }

        if(Input.GetKeyDown(KeyCode.Escape) && !FimDaFase) {
            TelaDePause();
        }
    }

    public void ReiniciarPartida(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //Pega a cena ativa no momento, pega seu nome e carrega essa fase
    }

    public void SairDoJogo(){
        Debug.Log("Saiu do jogo");
        Application.Quit();
    }

    public void TelaDeVitoria(){
        quadroPedidos.gameObject.SetActive(false);
        telaDeFim.gameObject.SetActive(true);
    }

    public void TelaDePause(){
        telaDePause.gameObject.SetActive(true);
    }

    public void FecharTelaDeFim(){
        telaDeFim.gameObject.SetActive(false);
    }

    public void FecharTelaDePause(){
        telaDePause.gameObject.SetActive(false);
    }
}
