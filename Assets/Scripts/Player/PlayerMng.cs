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

    public void MatarJogador(){
        estaMorto = true;
        CanvasGameMng.Instance.fimDeJogo = true;
        Destroy(GetComponent<CapsuleCollider>());
        disparoPlayer.DesabilitarArmas();
    }
}
