using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ArmaControlador : MonoBehaviour
{
    private Animator animator;
    private int pente;//Armazenar a quantidade de balas no pente da arma
    public int municaoPorPente;//Quantidade bala maxima que o pente suporta
    public int municaoMaxima;//Quantidade maxima de munição
    private int municaoAtual;//Quantidade de munição atual da arma
    public float danoInimigo;//O valor do dano a vida do inimigo
    public GameObject capsula;
    public Transform posicaoCapsula;
    public GameObject muzzleFlash;
    public AudioClip[] audios; //0 - Tiro, 1 - Recarregamento, 2 - Arma Sem Bala, 3 - Usar arma
    public AudioSource audioSource;

    public int Pente{
        get{return pente;}
    }
    public int MunicaoAtual{
        get{return municaoAtual;}
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        pente = municaoPorPente;
        municaoAtual = municaoMaxima;
        audioSource.volume = AudioMng.Instance.volumeVFX;
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
            audioSource.PlayOneShot(audios[1]);
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
            audioSource.PlayOneShot(audios[2]);
            PlaySemMunicao();
            pente = 0;
        }
        audioSource.PlayOneShot(audios[0]);

        muzzleFlash.SetActive(true);
        //Instanciar a capsula
        GameObject cp = Instantiate(capsula);
        //Posicionar a capsula na posição que vai ser ejetada
        cp.transform.position = posicaoCapsula.position;
        //Posicionar a rotação na mesma rotação de saida da capsula
        cp.transform.rotation = posicaoCapsula.rotation;
        //Chamar a função para ejetar a capsula
        cp.GetComponent<EjetarCapsula>().Ejetar();
    }

    public void IncrementarMunicao(int municao){
        municaoAtual += municao;
        if(municaoAtual > municaoMaxima){
            municaoAtual = municaoMaxima;
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

    public void AtivarAudioSelecaoDeArma(){
        audioSource.volume = AudioMng.Instance.volumeVFX;
        audioSource.PlayOneShot(audios[3]);
    }
}
