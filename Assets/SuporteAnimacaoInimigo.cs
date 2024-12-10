using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuporteAnimacaoInimigo : MonoBehaviour
{
    private Animator animator;
    private InimigoControlador controlador;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        controlador = GetComponentInParent<InimigoControlador>();
    }

    public void PlayIdle()
    {
        animator.SetBool("run", false);
        animator.SetBool("idle", true);
        animator.SetBool("attack", false);
    }
    public void PlayRun()
    {
        animator.SetBool("run", true);
        animator.SetBool("idle", false);
        animator.SetBool("attack", false);
    }
    public void PlayAttack()
    {
        animator.SetBool("run", false);
        animator.SetBool("idle", false);
        animator.SetBool("attack", true);
    }
    public void PlayDeath()
    {
        animator.SetTrigger("death");
    }

    public void DanoAoPlayer()
    {
        controlador.DanoAoPlayer();
    }
}
