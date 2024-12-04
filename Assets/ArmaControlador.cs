using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmaControlador : MonoBehaviour
{
    private Animator animator;
    public int pente;//Armazenar a quantidade de balas no pente da arma
    public int municaoPorPente;//Quantidade bala maxima que o pente suporta
    public int municaoMaxima;//Quantidade maxima de munição
    public int municaoAtual;//Quantidade de munição atual da arma

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        pente = municaoPorPente;
        municaoAtual = municaoMaxima;
    }

    //Permitir ativar a animação de tiro
    public void Disparar(){
        //Verificar se tem bala no pente
        if(pente > 0){
            PlayDisparo();
        }
        
    }
    public void CancelarDisparo(){
        PlayCancelarDisparo();
    }
    public void RecarregarArma(){
        //Se arma tem munição e verificar quantidade de muniçao no pente
        if(municaoAtual > 0 && pente < municaoPorPente){
            //Calcular uma diferença entre a munição que eu posso ter no pente com o
            //que eu já tenho no pente
            int diferenca = municaoPorPente - pente;
            //verificar se a diferença é menor que quantidade de munição atual
            if(diferenca < municaoAtual){
                pente += diferenca;
                municaoAtual -= diferenca;
            }else{
                pente += municaoAtual;
                municaoAtual = 0;
            }
            PlayRecarregar();
        }        
    }

    private void InstanciarBala(){
        PlayerMng.disparoPlayer.DanoAoObjeto();//Dar dano ao objeto
        pente--;
        if(pente <=0){
            PlaySemMunicao();
            pente = 0;
        }
    }

    private void PlayDisparo(){
        animator.SetBool("Fire",true);
        animator.SetBool("Idle",true);
        animator.SetBool("Reload",false);
    }
    private void PlayCancelarDisparo(){
        animator.SetBool("Fire",false);
        animator.SetBool("Idle",true);
        animator.SetBool("Reload",false);
    }
    private void PlaySemMunicao(){
        animator.SetBool("Fire",false);
        animator.SetBool("Idle",false);
        animator.SetBool("Empty",true);
    }
    private void PlayRecarregar(){
        animator.SetBool("Reload",true);
        animator.SetBool("Idle",true);
        animator.SetBool("Empty",false);
    }
}
