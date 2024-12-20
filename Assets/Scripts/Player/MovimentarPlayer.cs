using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MovimentarPlayer : MonoBehaviour
{
    private Camera playerCamera;
    public float velocidadeCaminhada = 6f;
    public float velocidadeCorrida = 12f;
    public float forcaPulo = 7f;
    public float forcaGravidade = 10f;
    public float velocidadeCamera = 2f;
    public float limiteCameraX = 45f;
    private Vector3 direcaoMovimentacao = Vector3.zero;
    private float rotacaoX = 0;
    CharacterController characterController;
    public AudioSource audioSource;
    public AudioClip[] audiosMovimentacao;// 0 - Andar, 1 - Correr
    // Start is called before the first frame update
    void Start()
    {
        playerCamera = Camera.main;
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        audioSource.volume = AudioMng.Instance.volumeVFX;
    }

    // Update is called once per frame
    void Update()
    {
        if(CanvasGameMng.Instance.fimDeJogo == true) return;
        //Virificar se o player pode se movimentar
        if(PlayerMng.Instance.estaMorto == true) return;
        //Pegar a direção do corpo do player em relação a frente e uma das extremidades
        Vector3 frente = transform.TransformDirection(Vector3.forward);
        Vector3 direita = transform.TransformDirection(Vector3.right);
        //Pegar a tecla que faz o player correr
        bool estaCorrendo = Input.GetKey(KeyCode.LeftShift);
        //Calcular a velocidade em X
        float velocidadeX = (estaCorrendo == true ? velocidadeCorrida : velocidadeCaminhada) * Input.GetAxis("Vertical");
        float velocidadeY = (estaCorrendo == true ? velocidadeCorrida : velocidadeCaminhada) * Input.GetAxis("Horizontal");
        //Direção inicial do eixo y
        float direcaoEmY = direcaoMovimentacao.y;
        //Definir a direcao do player
        direcaoMovimentacao = (frente * velocidadeX) + (direita * velocidadeY);

        //Verificar se está se movimentando para poder fazer o audio de movimentacao
        if(direcaoMovimentacao != Vector3.zero){
            if(estaCorrendo == true){
                if(audioSource.clip != audiosMovimentacao[1]){
                    audioSource.Stop();
                    audioSource.clip = audiosMovimentacao[1];
                    audioSource.Play();
                }
            }
            else{
                if(audioSource.clip != audiosMovimentacao[0]){
                    audioSource.Stop();
                    audioSource.clip = audiosMovimentacao[0];
                    audioSource.Play();
                }
            }
            if(audioSource.isPlaying == false){
                audioSource.Play();
            }            
        }
        else{
            audioSource.Stop();
        }

        //Verificar se o jogador está no chão para efetuar o pulo
        if(Input.GetButton("Jump") && characterController.isGrounded == true){
            direcaoMovimentacao.y = forcaPulo;
        }
        else{
            direcaoMovimentacao.y = direcaoEmY;
        }

        //Verificar se o jogador não está no chão
        if(characterController.isGrounded == false){
            direcaoMovimentacao.y -= forcaGravidade * Time.deltaTime;
        }

        //Movimentar o personagem
        characterController.Move(direcaoMovimentacao * Time.deltaTime);

        //Controlar a camera do personagem
        rotacaoX += -Input.GetAxis("Mouse Y") * velocidadeCamera;
        rotacaoX = Mathf.Clamp(rotacaoX,-limiteCameraX,limiteCameraX);
        playerCamera.transform.localRotation = Quaternion.Euler(rotacaoX,0,0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * velocidadeCamera,0);
    }
}
