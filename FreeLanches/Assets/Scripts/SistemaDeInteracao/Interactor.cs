using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
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
    [SerializeField] public Transform PickUpPoint;
    private InterfaceInteractable Interactable;
    private GameObject ForwardItem = null;
    private GameObject CarriableItem = null;
    [SerializeField] private bool Segurando = false;
    public bool PodeMover = true;
    public SimpleSampleCharacterControl scriptCozinheiro;
    public bool PedidoPego = false;
    public bool PedidoFeito = false;
    public bool PedidoEntregue = false;
    void start() {
    }

    void Update()
    {
        //Isso encontrará cada objeto interativo e com um InteractablerMask (layermask) que está na posicao e no raio da esfera de colisao do personagem
        //e preenchera esse array de colliders com o que encontrar, retornando assim a quantidade de objetos que foi encontrado
        NumCollidersFound = Physics.OverlapSphereNonAlloc(InteractionPoint.position, InteractionPointRadius, Colliders, InteractableMask);
        if(Segurando && NumCollidersFound == 0 && !PedidoFeito){
            if(InteractionPromptUI.IsDisplayed()) InteractionPromptUI.Close();
            InteractionPromptUI.SetUp("Largar item (F)");
            if(Input.GetKeyDown(KeyCode.F)){
                DropItem(CarriableItem);
                Segurando = false;
                CarriableItem = null;
            }
        }

        else if (NumCollidersFound > 0) {
            //Aqui pegamos o objeto interativo monobehavior que esta implementando a interface de interativos e é o primeiro a colidir 
            //com o raio de interacao do jogador\
            if(NumCollidersFound == 1){
                ForwardItem = Colliders[0].gameObject; 
                Interactable = Colliders[0].GetComponent<InterfaceInteractable>(); 
            }

            else{
                ForwardItem = Colliders[1].gameObject; 
                Interactable = Colliders[1].GetComponent<InterfaceInteractable>(); 
            }

            Comidas comida = ForwardItem.GetComponent<Comidas>();
            if(Interactable != null){
                //Aqui se a UI nao estiver sendo mostrada nesse momento, nos vamos pegar o nome (prompt) do objeto interativo que esta a nossa 
                //frente e coloca-lo na UI para ser mostrado  
                if(InteractionPromptUI.IsDisplayed()) InteractionPromptUI.Close(); 

                InteractionPromptUI.SetUp(Interactable.InteractionPrompt);

                if((ForwardItem.CompareTag("Bancada") || ForwardItem.CompareTag("BancadaEntrega") || ForwardItem.CompareTag("PratoMontagem")) && !Segurando){
                    InteractionPromptUI.Close(); 
                }

                if(ForwardItem.CompareTag("PratoMontagem") && Segurando && !PedidoPego){
                    InteractionPromptUI.Close(); 
                }

                if(ForwardItem.CompareTag("BancadaEntrega") && Segurando && !PedidoFeito){
                    InteractionPromptUI.Close(); 
                }

                if(ForwardItem.CompareTag("QuadroDePedidos") && Segurando && PedidoFeito){
                    InteractionPromptUI.Close(); 
                }
                
                if(ForwardItem.CompareTag("Comida") && !comida.Grounded){
                    InteractionPromptUI.Close(); 
                }
                
                //Aqui se o usuario pressionara tecla E e existir um objeto interativo na sua frente, iremos chamar a funcao Interect()
                //responsavel por executar uma acao de acordo com o objeto que estamos interagindo 
                //Lembrar: Nós somos o Interactor que está interagindo com esse Interactable a nossa frente
                if(Input.GetKeyDown(KeyCode.E) && ForwardItem.CompareTag("QuadroDePedidos") && !PedidoFeito) {
                    Interactable.Interact(this);
                    PodeMover = !PodeMover;
                    scriptCozinheiro.enabled = PodeMover;
                }
                else if(Input.GetKeyDown(KeyCode.Space) && ForwardItem.CompareTag("Comida") && !Segurando && comida.Grounded) {
                    Segurando = true;
                    Interactable.Interact(this);
                    CarriableItem = ForwardItem;
                }
                else if(Input.GetKeyDown(KeyCode.Space) && ForwardItem.CompareTag("Bancada") && Segurando){
                    Segurando = false;
                    Interactable.Interact(this, CarriableItem);
                    CarriableItem = null;
                }

                else if(Input.GetKeyDown(KeyCode.Space) && ForwardItem.CompareTag("PratoMontagem") && Segurando){
                    if(Interactable.Interact(this, CarriableItem)){ 
                        Segurando = false;
                        CarriableItem = null;
                    }
                }

                else if(Input.GetKeyDown(KeyCode.Space) && ForwardItem.CompareTag("BancadaEntrega") && Segurando && PedidoFeito){
                    Segurando = false;
                    Interactable.Interact(this, CarriableItem);
                    CarriableItem = null;
                }
            }
            
        }

        else{
            //Aqui se nao encontrarmos nada a nossa frente, vamos apenas tornar o objeto interativo como null, se ele nao for null, e 
            //em seguida fechar o display
            if(Interactable != null) Interactable = null;
            if(InteractionPromptUI.IsDisplayed()) InteractionPromptUI.Close();
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(InteractionPoint.position, InteractionPointRadius);
    }

    public void DropItem(GameObject carriableItem){
        Comidas comida = carriableItem.GetComponent<Comidas>();
        if(comida != null){
            if(comida.itemIsPicked == true) {
                comida.rb.constraints &= ~RigidbodyConstraints.FreezePositionY;
                comida.rb.constraints &= ~RigidbodyConstraints.FreezePositionX;
                comida.rb.constraints &= ~RigidbodyConstraints.FreezePositionZ;
                comida.rb.AddForce(this.transform.forward * 500f);
                comida.transform.parent = null;
                carriableItem.GetComponent<Rigidbody>().useGravity = true;
                carriableItem.GetComponent<BoxCollider>().enabled = true;
                comida.itemIsPicked = false;
                // comida.rb.constraints &= ~RigidbodyConstraints.FreezePositionY;
            }
        }
    }

    public void setCarriableItem(GameObject carriableItem){
        this.CarriableItem = carriableItem;
    }

    public GameObject getCarriableItem(){
        return this.CarriableItem;
    }

    public void setSegurando(bool segurando){
        this.Segurando = segurando;
    }
}
  