using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemMedkit : MonoBehaviour
{
    private bool houveColisao = false;
    private void OnTriggerEnter(Collider colisor){
        if(colisor.gameObject.tag == "Player" && houveColisao == false){
            houveColisao = true;
            CanvasGameMng.Instance.IncrementarVidaJogador();
            Destroy(gameObject);
        }
    }
}
