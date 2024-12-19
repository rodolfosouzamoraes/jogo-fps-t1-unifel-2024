using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class InimigoControlador : MonoBehaviour
{
    public float vida;
    public Slider sldVida;
    public float velocidade;
    public float distanciaDoPlayer;
    public float distanciaPerseguicao;
    public bool estaPerseguindo = false;
    public LayerMask layerMask;
    public int danoAoPlayer = 10;
    private CapsuleCollider capsuleCollider;
    private bool estaMorto = false;
    private bool estaVendoPlayer = false;
    private NavMeshAgent agent;
    private SuporteAnimacaoInimigo suporteAnimacao;
    public GameObject canvasBarraDeVida;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        sldVida.maxValue = vida;
        sldVida.value = vida;
        estaVendoPlayer = false;
        estaMorto = false;
        agent = GetComponent<NavMeshAgent>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        agent.speed = velocidade;
        suporteAnimacao = GetComponentInChildren<SuporteAnimacaoInimigo>();
        OcultarBarraDeVida();
    }

    // Update is called once per frame
    void Update()
    {
        if(CanvasGameMng.Instance.fimDeJogo == true) return;
        if(estaMorto == true) return;
        VisaoInimigo();

        ControlarBarraDeVida();

        PerseguirJogador();
    }

    private void ControlarBarraDeVida(){
        sldVida.transform.LookAt(
            new Vector3(
                PlayerMng.Instance.transform.position.x,
                sldVida.transform.position.y,
                PlayerMng.Instance.transform.position.z
            )
        );
    }

    private void PerseguirJogador(){
        //Calcular a distancia entre o jogador e o inimigo, se for menor que a tolerancia
        //Seguir o jogador
        float distancia = Vector3.Distance(
            transform.position, 
            PlayerMng.Instance.gameObject.transform.position
        );
        
        if(distancia < distanciaPerseguicao || estaPerseguindo == true){
            estaPerseguindo = true;
            //Verificar se a distancia entre o inimigo e o jogador é maior que a distacia minima para atacar
            if(distancia > distanciaDoPlayer){
                //Fazer o inimigo ir até o jogador
                agent.destination = PlayerMng.Instance.transform.position;
                suporteAnimacao.PlayRun();//Animação de corrida
            }
            else{
                agent.destination = transform.position;
                Vector3 olharParaJogador = new Vector3(
                    PlayerMng.Instance.transform.position.x,
                    transform.position.y,
                    PlayerMng.Instance.transform.position.z
                );
                transform.LookAt(olharParaJogador);
                suporteAnimacao.PlayAttack();//Animação de Ataque
            }
        }
        else{
            agent.destination = transform.position;
            suporteAnimacao.PlayIdle();//Animação de Parado
        }
    }

    public void DecrementarVida(float dano){
        vida-=dano;
        estaPerseguindo = true;
        if(vida<=0){
            estaMorto = true;
            CanvasGameMng.Instance.IncrementarMortesZumbi();
            InstanciarInimigos.Instance.DecrementarQtdInimigosNaFase();
            agent.destination = transform.position;
            suporteAnimacao.PlayDeath();//Ativar a animação de morte
            Destroy(capsuleCollider);
            Destroy(sldVida.gameObject);
            Destroy(gameObject,5f);
            return;
        }
        else{
            sldVida.value = vida;
        }        
    }

    public void VisaoInimigo(){
        RaycastHit hit;
        Vector3 posicaoVisibilidade = new Vector3(
            transform.position.x, 
            1, 
            transform.position.z
        );

        //Vai verificar se está vendo o jogador
        if(Physics.Raycast(
            posicaoVisibilidade,
            transform.TransformDirection(Vector3.forward),
            out hit,
            10,
            layerMask)
        ){
            //Faz uma linha entre o inimigo e o jogador na cor amarelo
            Debug.DrawRay(
                posicaoVisibilidade, 
                transform.TransformDirection(Vector3.forward) * hit.distance,
                Color.yellow
            );
            estaVendoPlayer = true;
        }
        else{
            //Faz uma linha branca na direção onde o inimigo está olhando
            Debug.DrawRay(
                posicaoVisibilidade, 
                transform.TransformDirection(Vector3.forward) * 10,
                Color.white
            );
            estaVendoPlayer = false;
        }
    }

    public void DanoAoPlayer(){
        if(estaVendoPlayer == true){
            CanvasGameMng.Instance.DecrementarVidaJogador(danoAoPlayer);
        }
    }

    public void ExibirBarraDeVida(){
        canvasBarraDeVida.SetActive(true);
    }

    public void OcultarBarraDeVida(){
        canvasBarraDeVida.SetActive(false);
    }

    public void ConfigurarAudio(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }
}
