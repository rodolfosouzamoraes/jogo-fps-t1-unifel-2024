using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMng : MonoBehaviour
{
    public static PlayerMng Instance;
    public static VisaoCamera visaoCamera;
    public static MovimentarPlayer movimentarPlayer;
    public static DisparoPlayer disparoPlayer;

    void Awake(){
        if(Instance == null){
            visaoCamera = GetComponentInChildren<VisaoCamera>();
            movimentarPlayer = GetComponent<MovimentarPlayer>();
            disparoPlayer = GetComponent<DisparoPlayer>();
            Instance = this;
            
            return;
        }
        Destroy(gameObject);
    }

    public bool estaMorto;

    void Start(){
        InstanciarPlayerAleatoriamente();
    }

    public void MatarJogador(){
        estaMorto = true;
        Destroy(GetComponent<CapsuleCollider>());
        disparoPlayer.DesabilitarArmas();
    }

    private void InstanciarPlayerAleatoriamente(){
        float posicaoZ = Random.Range(-95,95);
        float posicaoX = Random.Range(-95,95);
        NavMeshHit hit;
        NavMesh.SamplePosition(
            new Vector3(posicaoX, 0, posicaoZ),
            out hit,
            Mathf.Infinity,
            1
        );
        transform.position = hit.position;
    }
}
