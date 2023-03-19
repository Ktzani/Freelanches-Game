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
    private List<Transform> Pedidos;
    [SerializeField] private int NumeroDePedidos;
    [SerializeField] private int NumeroIngredientesPedidoEscolhido;
    [SerializeField] private bool FimDaFase;

    private Interactor interactor;
    [SerializeField] private MontaPedidoUI montaPedidoUI;
    [SerializeField] private EntregaPedidoUI entregaPedidoUI;
    [SerializeField] private GameObject resumoPedidoUI;

    public bool PrimeiraVezAbrindoQuadro;

    [SerializeField] private GameObject LeftStar;
    [SerializeField] private GameObject MiddleStar;
    [SerializeField] private GameObject RightStar;
    // Start is called before the first frame update
    void Start()
    {   
        FimDaFase = false;
        PontuacaoDuranteJogo = 0;
        PontuacaoFimJogo = 0;
        NumeroDePedidos = -1;
        NumeroIngredientesPedidoEscolhido = -1;
        PrimeiraVezAbrindoQuadro = true;
        interactor = FindObjectOfType<Interactor>();
    } 

    void FixedUpdate()
    {   
        TextSegundos.text = Segundos.ToString("00");
        TextMinutos.text = Minutos.ToString("00");
        TextPontuacaoDuranteJogo.text = PontuacaoDuranteJogo.ToString();
        TextPontuacaoFimJogo.text = PontuacaoFimJogo.ToString();
        
        if(!FimDaFase){
            Segundos -= Time.deltaTime;
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

        if(Pedidos != null && !PrimeiraVezAbrindoQuadro){
            foreach(Transform pedido in Pedidos){
                HamburguerTradicional script = pedido.gameObject.GetComponent<HamburguerTradicional>();
                if(script.TempoPedido >= 0f){
                    script.TempoPedido -= Time.deltaTime;
                    script.TempoFinalizado = ComparaTempoJogoComPedido(script);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(PrimeiraVezAbrindoQuadro){
            Pedidos = quadroPedidos.GetComponent<QuadroButtonSystem>().getPedidos();
        }   

        if(Pedidos != null && PrimeiraVezAbrindoQuadro){
            NumeroDePedidos = Pedidos.Count;
            MontaTempo();
        }

        GameObject PedidoEscolhido = quadroPedidos.GetComponent<QuadroButtonSystem>().getPedidoEscolhido(); 

        if(PedidoEscolhido != null){
            NumeroIngredientesPedidoEscolhido = PedidoEscolhido.GetComponent<InterfacePedidos>().getIngredientes().Count;
        }

        if(Pedidos != null){ 
            foreach(Transform pedido in Pedidos){
                if(!pedido.gameObject.GetComponent<HamburguerTradicional>().EstePedidoFoiDeletado){
                    if(pedido.gameObject.GetComponent<HamburguerTradicional>().TempoFinalizado){
                        TerminouTempoPedido(pedido.gameObject, PedidoEscolhido);
                    }
                }
            }
        }
    
        
        if(FindObjectOfType<Interactor>().PedidoEntregue){
            MontaPontuacao();
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
        if(PontuacaoFimJogo < 3000){
            LeftStar.SetActive(true);
        }

        else if(PontuacaoFimJogo >= 3000 && PontuacaoFimJogo < 8000){
            LeftStar.SetActive(true);
            MiddleStar.SetActive(true);
        }

        else if(PontuacaoFimJogo >= 8000){
            LeftStar.SetActive(true);
            MiddleStar.SetActive(true);
            RightStar.SetActive(true);
        }

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

    public void MontaPontuacao(){
        if(NumeroIngredientesPedidoEscolhido > 0 && NumeroIngredientesPedidoEscolhido < 3){
            PontuacaoDuranteJogo += 1000;
        }
        else if(NumeroIngredientesPedidoEscolhido >= 3 && NumeroIngredientesPedidoEscolhido < 6){
            PontuacaoDuranteJogo += 2000;
        }
        else if(NumeroIngredientesPedidoEscolhido >= 6 && NumeroIngredientesPedidoEscolhido < 9){
            PontuacaoDuranteJogo += 4000;
        }
        else if(NumeroIngredientesPedidoEscolhido >= 9 && NumeroIngredientesPedidoEscolhido < 12){
            PontuacaoDuranteJogo += 10000;
        }
        else if(NumeroIngredientesPedidoEscolhido >= 12){
            PontuacaoDuranteJogo += 15000;
        }
        
        FindObjectOfType<Interactor>().PedidoEntregue = false;        
    }

    public void TerminouTempoPedido(GameObject Pedido, GameObject PedidoEscolhido){
        if(Pedido == PedidoEscolhido){
            if(Pedido.GetComponent<HamburguerTradicional>().EstePedidoFoiFeito){
                interactor.setSegurando(false);
                interactor.setCarriableItem(null);
                Destroy(interactor.getCarriableItem());
            }

            resumoPedidoUI.SetActive(false);
            montaPedidoUI.Close();
            entregaPedidoUI.Close();
            interactor.PedidoFeito = false;
            interactor.PedidoPego = false;
            interactor.PedidoEntregue = false;
            Pedido.gameObject.GetComponent<HamburguerTradicional>().EstePedidoFoiDeletado = true;
            Pedido.SetActive(false);
            Pedido = null;
        }

        else{
            Pedido.SetActive(false);
            Pedido.gameObject.GetComponent<HamburguerTradicional>().EstePedidoFoiDeletado = true;
            Pedido = null;
        }
        
        decrementaNumeroPedidos();
    }

    public void MontaTempo(){
        float i = 0f;
        foreach(Transform pedido in Pedidos){
            int quantidadeIngredientesPedido = pedido.GetComponent<InterfacePedidos>().getIngredientes().Count;
            float tempo = 0;
            if(quantidadeIngredientesPedido > 0 && quantidadeIngredientesPedido < 3){
                tempo = 10f;
            }
            else if(quantidadeIngredientesPedido >= 3 && quantidadeIngredientesPedido < 6){
                tempo = 15f;
            }
            else if(quantidadeIngredientesPedido >= 6 && quantidadeIngredientesPedido < 9){
                tempo = 20f;
            }
            else if(quantidadeIngredientesPedido >= 9 && quantidadeIngredientesPedido < 12){
                tempo = 25f;
            }
            else if(quantidadeIngredientesPedido >= 12){
                tempo = 30f;
            }
            pedido.gameObject.GetComponent<HamburguerTradicional>().TempoMaximoPedido += (i + tempo);
            pedido.gameObject.GetComponent<HamburguerTradicional>().TempoPedido += (i + tempo);
            i += 20;
        }
        PrimeiraVezAbrindoQuadro = false;
    }

    public bool ComparaTempoJogoComPedido(HamburguerTradicional script){
        if(script.TempoPedido <= 0f && !script.EstePedidoFoiEntregue){
            script.TempoPedido = 0;
            return true;
        }

        return false;
    
    }

    public void decrementaNumeroPedidos(){
        this.NumeroDePedidos--;
    }


}
