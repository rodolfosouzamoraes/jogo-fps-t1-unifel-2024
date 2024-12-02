using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoPlayer : MonoBehaviour
{
    public ArmaController pistolaController;
    public GameObject impactoBala;
    public GameObject impactoBalaInimigo;

    // Update is called once per frame
    void Update()
    {
        //Atirar
        if (Input.GetKey(KeyCode.Mouse0))
        {
            pistolaController.Disparar();
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0)) 
        {
            pistolaController.CancelarDisparo();
        }

        //Recarregar
        if (Input.GetKeyDown(KeyCode.R)) {
            Debug.Log("Recarregar");
            pistolaController.RecarregarArma();
        }
    }

    public void DanoAoObjeto()
    {
        if(PlayerMng.visaoCamera.AlvoVisto != null)
        {
            Quaternion rotacaoDoImpacto = Quaternion.FromToRotation(Vector3.forward, PlayerMng.visaoCamera.hitAlvo.normal);
            if (PlayerMng.visaoCamera.tagAlvo == "Inimigo")
            {
                
                Instantiate(impactoBalaInimigo, PlayerMng.visaoCamera.hitAlvo.point, rotacaoDoImpacto);
            }
            else
            {
                Instantiate(impactoBala, PlayerMng.visaoCamera.hitAlvo.point, rotacaoDoImpacto);
            }
        }
    }
}
