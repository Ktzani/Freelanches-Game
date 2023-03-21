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
    public bool PauseGame;
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
    [SerializeField] private int TotalNumeroDePedidos;
    [SerializeField] private int NumeroIngredientesPedidoEscolhido;
    [SerializeField] private int TotalNumeroIngredientes;
    [SerializeField] private bool FimDaFase;
    private Interactor interactor;
    [SerializeField] private MontaPedidoUI montaPedidoUI;
    [SerializeField] private EntregaPedidoUI entregaPedidoUI;
    [SerializeField] private GameObject resumoPedidoUI;

    public bool PrimeiraVezAbrindoQuadro;

    [SerializeField] private GameObject LeftStar;
    [SerializeField] private GameObject MiddleStar;
    [SerializeField] private GameObject RightStar;

    public SimpleSampleCharacterControl scriptCozinheiro;
    void Start()
    {   
        FimDaFase = false;
        PauseGame = false;
        PontuacaoDuranteJogo = 0;
        PontuacaoFimJogo = 0;
        NumeroDePedidos = -1;
        TotalNumeroDePedidos = 0;
        NumeroIngredientesPedidoEscolhido = -1;
        TotalNumeroIngredientes = 0;
        PrimeiraVezAbrindoQuadro = true;
        interactor = FindObjectOfType<Interactor>();
    } 

    void FixedUpdate()
    {   
        
        TextSegundos.text = Segundos.ToString("00");
        TextMinutos.text = Minutos.ToString("00");
        TextPontuacaoDuranteJogo.text = PontuacaoDuranteJogo.ToString();
        TextPontuacaoFimJogo.text = PontuacaoFimJogo.ToString();

        if(!PauseGame && !PrimeiraVezAbrindoQuadro){
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
    }

    // Update is called once per frame
    void Update()
    {   
        if(!PauseGame){
            if(PrimeiraVezAbrindoQuadro){
                Pedidos = quadroPedidos.GetComponent<QuadroButtonSystem>().getPedidos();
            }   

            if(Pedidos != null && PrimeiraVezAbrindoQuadro){
                NumeroDePedidos = Pedidos.Count;
                TotalNumeroDePedidos += Pedidos.Count;
                MontaTempo();
            }

            GameObject PedidoEscolhido = quadroPedidos.GetComponent<QuadroButtonSystem>().getPedidoEscolhido(); 

            if(PedidoEscolhido != null){
                NumeroIngredientesPedidoEscolhido = PedidoEscolhido.GetComponent<InterfacePedidos>().getIngredientes().Count;
                TotalNumeroIngredientes += NumeroIngredientesPedidoEscolhido;
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

        if(Minutos == 0 && Segundos == 0){
            TelaDeVitoria();
        }

        if(NumeroDePedidos == 0){
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

    public void VoltarParaMenuPrincipal(){
        SceneManager.LoadScene(0);
    }

    public void CarregaFase2(){
        SceneManager.LoadScene(3);
    }

     public void CarregaFase3(){
        SceneManager.LoadScene(4);
    }

    public void CarregaMenuFases(){
        SceneManager.LoadScene(1);
    }


    public void TelaDeVitoria(){
        float pontuacaoEsperada = TotalNumeroIngredientes * 11f;
        float taxa = pontuacaoEsperada / 2f;

        TextPontuacaoFimJogo.text = PontuacaoFimJogo.ToString();

        if(PontuacaoFimJogo > 0 && PontuacaoFimJogo < taxa){
            LeftStar.SetActive(true);
        }

        else if(PontuacaoFimJogo >= taxa && PontuacaoFimJogo < pontuacaoEsperada){
            LeftStar.SetActive(true);
            MiddleStar.SetActive(true);
        }

        else if(PontuacaoFimJogo >= pontuacaoEsperada){
            LeftStar.SetActive(true);
            MiddleStar.SetActive(true);
            RightStar.SetActive(true);
        }

        quadroPedidos.gameObject.SetActive(false);
        telaDeFim.gameObject.SetActive(true);
    }

    public void TelaDePause(){
        if(!PauseGame){
            telaDePause.gameObject.SetActive(true); 
            PauseGame = true;
            scriptCozinheiro.enabled = false;
        }
        else{
            telaDePause.gameObject.SetActive(false); 
            PauseGame = false;
            scriptCozinheiro.enabled = true;
        }
    }

    public void MontaPontuacao(){
        PontuacaoDuranteJogo += NumeroIngredientesPedidoEscolhido*110;
        
        FindObjectOfType<Interactor>().PedidoEntregue = false;        
    }

    public void TerminouTempoPedido(GameObject Pedido, GameObject PedidoEscolhido){
        if(Pedido == PedidoEscolhido){
            if(Pedido.GetComponent<HamburguerTradicional>().EstePedidoFoiFeito){
                interactor.setSegurando(false);
                Destroy(interactor.getCarriableItem());   
                interactor.setCarriableItem(null);
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
        float caminhada = 15f;
        foreach(Transform pedido in Pedidos){
            int quantidadeIngredientesPedido = pedido.GetComponent<InterfacePedidos>().getIngredientes().Count;
            float tempo = quantidadeIngredientesPedido*11;
            
            pedido.gameObject.GetComponent<HamburguerTradicional>().TempoMaximoPedido += (i + tempo);
            pedido.gameObject.GetComponent<HamburguerTradicional>().TempoPedido += (i + tempo);
            i += tempo + caminhada;
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
