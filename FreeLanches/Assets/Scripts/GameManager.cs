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
    public int Pontuacao;
    public TextMeshProUGUI TextPontuacao;
    public int NumeroDePedidos;
    public bool FimDaFase;

    // Start is called before the first frame update
    void Start()
    {
        NumeroDePedidos = GameObject.FindGameObjectsWithTag("Pedido").Length;
        FimDaFase = false;
        Pontuacao = 0;
    } 

    void FixedUpdate()
    {   
        TextSegundos.text = Segundos.ToString("00");
        TextMinutos.text = Minutos.ToString("00");
        TextPontuacao.text = Pontuacao.ToString();

        if(!FimDaFase){
            Segundos -= Time.deltaTime;
            Pontuacao += 100;
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
        if(Input.GetKeyDown(KeyCode.R)){
            ReiniciarPartida(); 
        }

        if(Input.GetKeyDown(KeyCode.End)){
            SairDoJogo();
        }  

        // if(NumeroDePedidos == 0 || (Minutos == 0 || Segundos == 0)){
        //     TelaDeVitoria();
        // }

        if(Input.GetKeyDown(KeyCode.Escape)) {
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
