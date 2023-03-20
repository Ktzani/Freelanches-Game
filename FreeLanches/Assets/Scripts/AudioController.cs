using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioController : MonoBehaviour
{   
    public AudioSource audioSourceMusicaDeFundo;
    public AudioSource audioSourceSFX; 
    public AudioClip[] musicasDeFundo;
    // Start is called before the first frame update
    void Start()
    {
        // Create a temporary reference to the current scene.
        Scene currentScene = SceneManager.GetActiveScene();

        // Retrieve the name of t$$anonymous$$s scene.
        string sceneName = currentScene.name;

        if (sceneName == "Tela Inicial" || sceneName == "Tela Fases") 
        {   
            int IndexDaMusicaDeFundo = Random.Range(0, 2);  
            audioSourceMusicaDeFundo.clip = musicasDeFundo[IndexDaMusicaDeFundo];
            audioSourceMusicaDeFundo.loop = true;
            audioSourceMusicaDeFundo.Play();
        }
        else if (sceneName == "Fase 1")
        {
            audioSourceMusicaDeFundo.clip = musicasDeFundo[2];
            audioSourceMusicaDeFundo.loop = true;
            audioSourceMusicaDeFundo.Play();
        }
        else if (sceneName == "Fase 2")
        {
            audioSourceMusicaDeFundo.clip = musicasDeFundo[3];
            audioSourceMusicaDeFundo.loop = true;
            audioSourceMusicaDeFundo.Play();
        }
        else if (sceneName == "Fase 3")
        {
            audioSourceMusicaDeFundo.clip = musicasDeFundo[4];
            audioSourceMusicaDeFundo.loop = true;
            audioSourceMusicaDeFundo.Play();
        }
    }

    public void ToqueSFX(AudioClip clip){
        audioSourceSFX.clip = clip;
        audioSourceSFX.Play();
    }
}
