using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class InimigoControlador : MonoBehaviour
{
    private NavMeshAgent agent;
    private SuporteAnimacaoInimigo suporteAnimacao;
    public float vida;
    public Slider sldVida;
    public float velocidade;
    public float diatanciaDoPlayer;
    public float distanciaPerseguicao;
    public bool estaPerseguindo = false;
    private CapsuleCollider capsuleCollider;
    private bool estaMorto = false;
    public LayerMask layerMask;
    private bool estaVendoPlayer;
    // Start is called before the first frame update
    void Start()
    {
        estaVendoPlayer = false;
        sldVida.maxValue = vida;
        sldVida.value = vida;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = velocidade;
        suporteAnimacao = GetComponentInChildren<SuporteAnimacaoInimigo>();
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (estaMorto == true) return;
        VisaoInimigo();

        sldVida.transform.LookAt(
            new Vector3(
                PlayerMng.Instance.transform.position.x,
                sldVida.transform.position.y,
                PlayerMng.Instance.transform.position.z
            )
        );

        float distancia = Vector3.Distance(transform.position,PlayerMng.Instance.transform.position);
        if(distancia < distanciaPerseguicao || estaPerseguindo == true)
        {
            estaPerseguindo = true;
            if (distancia > diatanciaDoPlayer)
            {
                agent.destination = PlayerMng.Instance.transform.position;
                suporteAnimacao.PlayRun();//Animação de andar
            }
            else
            {
                agent.destination = transform.position;
                Vector3 lookAt = new Vector3(PlayerMng.Instance.transform.position.x, transform.position.y, PlayerMng.Instance.transform.position.z);
                transform.LookAt(lookAt);
                suporteAnimacao.PlayAttack();//Animação de atacar
            }
        }
        else
        {
            agent.destination = transform.position;
            suporteAnimacao.PlayIdle();
        }
        
    }

    public void DecrementarVida(float dano){
        vida-=dano;
        estaPerseguindo = true;
        if (vida<=0){
            estaMorto = true;
            agent.destination = transform.position;
            suporteAnimacao.PlayDeath();
            Destroy(capsuleCollider);
            Destroy(sldVida.gameObject);
            Destroy(gameObject,5f);
            return;
        }
        else
        {
            sldVida.value = vida;
        }
    }

    public void VisaoInimigo()
    {
        RaycastHit hit;
        Vector3 posicaoVisibilidade = new Vector3(transform.position.x,1, transform.position.z);
        if (Physics.Raycast(posicaoVisibilidade, transform.TransformDirection(Vector3.forward), out hit, 10, layerMask))
        {
            Debug.DrawRay(posicaoVisibilidade, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            estaVendoPlayer = true;
        }
        else
        {
            Debug.DrawRay(posicaoVisibilidade, transform.TransformDirection(Vector3.forward) * 10, Color.white);
            estaVendoPlayer = false;
        }
    }

    public void DanoAoPlayer()
    {
        if (estaVendoPlayer == true)
        {
            Debug.Log("Dano ao player");
        }
    }
}
