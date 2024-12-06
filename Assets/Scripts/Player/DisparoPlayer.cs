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
    private ArmaControlador armaAtiva;

    public ArmaControlador ArmaAtiva{
        get {return armaAtiva;}
    }

    void Start(){
        if(idArmaAtiva == 1){
            AtivarPistola();
        }
        else if(idArmaAtiva == 2){
            AtivarFuzil();
        }
    }
    // Update is called once per frame
    void Update()
    {
        SelecionarArma();
        DispararArma();
        RecarregarArma();
    }

    private void SelecionarArma(){
        //Selecionar qual arma usar
        if(Input.GetKeyDown(KeyCode.Alpha1)){
            idArmaAtiva = 1;
            AtivarPistola();
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2)){
            idArmaAtiva = 2;
            AtivarFuzil();
        }
    }

    private void DispararArma(){
        //Verificar se a armaAtiva é inválida
        if(armaAtiva == null) return;

        //Identificar se o botão de atirar foi clicado
        if(Input.GetKey(KeyCode.Mouse0)){
            armaAtiva.Disparar();
        }
        else if(Input.GetKeyUp(KeyCode.Mouse0)){
            armaAtiva.CancelarDisparo();
        }
    }

    private void RecarregarArma(){
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
                //Pega o código do inimigo no GameObject e tira o dano dele
                InimigoControlador inimigo = PlayerMng.visaoCamera.AlvoVisto.GetComponent<InimigoControlador>();
                inimigo.DecrementarVida(armaAtiva.danoInimigo);
            }
            else{
                Instantiate(impactoBala,PlayerMng.visaoCamera.hitAlvo.point,
                rotacaoDoImpacto);
            }
        }
    }

    private void AtivarPistola(){
        pistolaControlador.gameObject.SetActive(true);
        fuzilControlador.gameObject.SetActive(false);
        armaAtiva = pistolaControlador;
    }

    private void AtivarFuzil(){
        pistolaControlador.gameObject.SetActive(false);
        fuzilControlador.gameObject.SetActive(true);
        armaAtiva = fuzilControlador;
    }

    public void IncrementarMunicaoPistola(int municao){
        pistolaControlador.IncrementarMunicao(municao);
    }
    public void IncrementarMunicaoFuzil(int municao){
        fuzilControlador.IncrementarMunicao(municao);
    }
}
