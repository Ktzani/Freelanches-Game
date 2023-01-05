using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cozinheiro : MonoBehaviour
{
    // Start is called before the first frame update
    CharacterController cc;

    [SerializeField] private float velocidadeAplicada = 3f;

    public float gravity = -9.81f;
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
       MovimentoDoJogador();
    }

    public void MovimentoDoJogador(){
        float movimentoEixoX = Input.GetAxis("Horizontal");
        float movimentoEixoZ = Input.GetAxis("Vertical");
        Vector3 direcao = new Vector3(movimentoEixoX, gravity, movimentoEixoZ);

        cc.Move(direcao * velocidadeAplicada * Time.deltaTime);
    }

    // private void OnControllerColliderHit(ControllerColliderHit hit)
    // {
    //     if(hit.gameObject.CompareTag("ChuvaAcida")){
    //         Destroy(this.gameObject);
    //     }
    // }
}
