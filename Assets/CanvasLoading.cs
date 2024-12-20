using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasLoading : MonoBehaviour
{
    public static CanvasLoading Instance;
    void Awake(){
        if(Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        Destroy(gameObject);
    }

    public GameObject pnlLoading;

    public void ExibirTelaDeCarregamento(){
        pnlLoading.SetActive(true);
    }

    public void OcultarTelaDeCarregamento(){
        pnlLoading.SetActive(false);
    }
}
