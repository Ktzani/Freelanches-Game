using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public Canvas telaDeFim;
    public Canvas telaDePause;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R)){
            ReiniciarPartida(); 
        }

        if(Input.GetKeyDown(KeyCode.Escape)){
            SairDoJogo();
        }  

        if(Input.GetKeyDown(KeyCode.F)) {
            TelaDeVitoria();
        }

        if(Input.GetKeyDown(KeyCode.P)) {
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
