using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmaController : MonoBehaviour
{
    private Animator animator;
    public int pente;
    public int municaoPorPente = 10;
    public int municaoMaxima = 90;
    public int municaoTotal;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        pente = municaoPorPente;
        municaoTotal = municaoMaxima;
    }

    public void Disparar()
    {
        if(pente > 0)
        {
            PlayDisparo();
        }
        
    }
    public void CancelarDisparo()
    {
        PlayCancelarDisparo();
    }
    //Animação aciona esse método
    private void InstanciarBala()
    {
        PlayerMng.disparoPlayer.DanoAoObjeto();
        pente--;
        if (pente <= 0)
        {
            PlaySemMunicao();
            pente = 0;
        }
        
    }
    public void RecarregarArma()
    {
        if(municaoTotal > 0 && pente < municaoPorPente)
        {
            int diferenca = municaoPorPente - pente;
            if (diferenca < municaoTotal)
            {
                pente += diferenca;
                municaoTotal -= diferenca;
            }
            else
            {
                pente += municaoTotal;
                municaoTotal = 0;
            }
            PlayRecarregar();
        }
        
    }
    private void PlayDisparo()
    {
        animator.SetBool("Fire", true);
        animator.SetBool("Idle", true);
        animator.SetBool("Reload", false);
    }
    private void PlayCancelarDisparo()
    {
        animator.SetBool("Fire", false);
        animator.SetBool("Idle", true);
        animator.SetBool("Reload", false);
    }
    private void PlaySemMunicao()
    {
        animator.SetBool("Fire", false);
        animator.SetBool("Idle", false);
        animator.SetBool("Empty", true);
    }
    private void PlayRecarregar()
    {
        animator.SetBool("Reload", true);
        animator.SetBool("Idle", true);
        animator.SetBool("Empty", false);
    }

    private void PlayParado()
    {
        animator.SetBool("Fire", false);
        animator.SetBool("Idle", true);
        animator.SetBool("Reload", false);
    }
}
