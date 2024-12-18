using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemChave : MonoBehaviour
{
    private bool houveColisao = false;
    private void OnTriggerEnter(Collider colisor){
        if(colisor.gameObject.tag == "Player" && houveColisao == false){
            houveColisao = true;
            CanvasGameMng.Instance.IncrementarChave();
            Destroy(gameObject);
        }
    }
}
