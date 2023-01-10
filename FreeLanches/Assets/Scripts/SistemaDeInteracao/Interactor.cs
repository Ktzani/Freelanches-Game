using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform InteractionPoint;
    [SerializeField] private float InteractionPointRadius = 0.5f;
    [SerializeField] private LayerMask InteractableMask;  
    [SerializeField] private InteractionPromptUI InteractionPromptUI;
    private readonly Collider[] Colliders = new Collider[3]; // Quantidade de objetos que iremos procurar em um espaço. Assim que esse 
                                                             // collider estiver cheio, nao procuraremos por mais nada
    [SerializeField] private int NumCollidersFound; //Numero de colliders que encontraremos na InteractableMask 
    private InterfaceInteractable Interactable;

    void Update()
    {
        //Isso encontrará cada objeto interativo e com um InteractablerMask (layermask) que está na posicao e no raio da esfera de colisao do personagem
        //e preenchera esse array de colliders com o que encontrar, retornando assim a quantidade de objetos que foi encontrado
        NumCollidersFound = Physics.OverlapSphereNonAlloc(InteractionPoint.position, InteractionPointRadius, Colliders, InteractableMask);

        if(NumCollidersFound > 0) {
            //Aqui pegamos o objeto interativo monobehavior que esta implementando a interface de interativos e é o primeiro a colidir 
            //com o raio de interacao do jogador
            Interactable = Colliders[0].GetComponent<InterfaceInteractable>(); 

            if(Interactable != null){
                //Aqui se a UI nao estiver sendo mostrada nesse momento, nos vamos pegar o nome (prompt) do objeto interativo que esta a nossa 
                //frente e coloca-lo na UI para ser mostrado  
                if(!InteractionPromptUI.IsDisplayed) InteractionPromptUI.SetUp(Interactable.InteractionPrompt);
                
                //Aqui se o usuario pressionara tecla E e existir um objeto interativo na sua frente, iremos chamar a funcao Interect()
                //responsavel por executar uma acao de acordo com o objeto que estamos interagindo 
                //Lembrar: Nós somos o Interactor que está interagindo com esse Interactable a nossa frente
                if(Input.GetKeyDown(KeyCode.E)) Interactable.Interact(this);
            }
        }

        else{
            //Aqui se nao encontrarmos nada a nossa frente, vamos apenas tornar o objeto interativo como null, se ele nao for null, e 
            //em seguida fechar o display
            if(Interactable != null) Interactable = null;
            if(InteractionPromptUI.IsDisplayed) InteractionPromptUI.Close();
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(InteractionPoint.position, InteractionPointRadius);
    }
}
  