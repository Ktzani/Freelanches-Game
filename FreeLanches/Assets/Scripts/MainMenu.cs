using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void iniciarJogo() {
        SceneManager.LoadScene(1);
    }

    public void voltaTelaInicial() {
        SceneManager.LoadScene(0);
    }

    public void abreFase01() {
        SceneManager.LoadScene(2);
    }

    public void abreFase02() {
        SceneManager.LoadScene(3);
    }

    public void abreFase03() {
        SceneManager.LoadScene(4);
    }

    public void SairDoJogo(){
        Debug.Log("Saiu do jogo");
        Application.Quit();
    }
}
