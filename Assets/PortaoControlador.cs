using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortaoControlador : MonoBehaviour
{
    private bool houveColisao = false;
     private void OnTriggerEnter(Collider colisor){
        if(CanvasGameMng.Instance.ColetouTodasAsChaves() == false) return;
        if(colisor.gameObject.tag == "Player" && houveColisao == false ){
            houveColisao = true;
            CanvasGameMng.Instance.ExibirTelaFinal();
        }
    }
}
