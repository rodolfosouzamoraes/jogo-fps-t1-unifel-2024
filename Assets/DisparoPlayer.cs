using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoPlayer : MonoBehaviour
{
    public ArmaControlador pistolaControlador;
    public ArmaControlador fuzilControlador;
    public GameObject impactoBalaInimigo;
    public GameObject impactoBala;
    public int idArmaAtiva = 1; // 1 - Pistola, 2 - Fuzil 

    void Start(){
        if(idArmaAtiva == 1){
            pistolaControlador.gameObject.SetActive(true);
            fuzilControlador.gameObject.SetActive(false);
        }
        else{
            pistolaControlador.gameObject.SetActive(false);
            fuzilControlador.gameObject.SetActive(true);
        }
    }
    // Update is called once per frame
    void Update()
    {
        //Verificar qual arma está ativa e vou armazenar numa variavel
        ArmaControlador armaAtiva = idArmaAtiva == 1 ? pistolaControlador : fuzilControlador;

        //Verificar se a armaAtiva é inválida
        if(armaAtiva == null) return;

        //Identificar se o botão de atirar foi clicado
        if(Input.GetKey(KeyCode.Mouse0)){
            armaAtiva.Disparar();
        }
        else if(Input.GetKeyUp(KeyCode.Mouse0)){
            armaAtiva.CancelarDisparo();
        }

        //Tecla para recarregar a arma
        if(Input.GetKeyDown(KeyCode.R)){
            armaAtiva.RecarregarArma();
        }
    }

    public void DanoAoObjeto(){
        //Verificar se a camera está vendo algum objeto
        if(PlayerMng.visaoCamera.AlvoVisto != null){
            //Pegar a rotação inversa da colisão
            Quaternion rotacaoDoImpacto = Quaternion.FromToRotation(Vector3.forward,
            PlayerMng.visaoCamera.hitAlvo.normal);
            //Verificar se é o inimigo que deve emitir a particula
            if(PlayerMng.visaoCamera.tagAlvo == "Inimigo"){
                Instantiate(impactoBalaInimigo,PlayerMng.visaoCamera.hitAlvo.point,
                rotacaoDoImpacto);
            }
            else{
                Instantiate(impactoBala,PlayerMng.visaoCamera.hitAlvo.point,
                rotacaoDoImpacto);
            }
        }
    }
}
