using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasGameMng : MonoBehaviour
{
    public static CanvasGameMng Instance;
    
    void Awake(){
        if(Instance == null){
            Instance = this;
            return;
        }
        Destroy(gameObject);
    }

    public TextMeshProUGUI txtVida;
    public TextMeshProUGUI txtMunicao;
    public int vidaJogador;
    public GameObject pnlGameOver;
    public GameObject pnlStatusPlayer;

    void Start(){
        txtVida.text = $"+{vidaJogador}";
    }

    // Update is called once per frame
    void Update()
    {
        AtualizarMunicaoUI();
    }

    private void AtualizarMunicaoUI(){
        int pente = PlayerMng.disparoPlayer.ArmaAtiva.Pente;
        int municao = PlayerMng.disparoPlayer.ArmaAtiva.MunicaoAtual;
        //Se o pente ou a munição for inferior a 10, colocar o 0 na frente do numero
        string valorPente = pente < 10 ? $"0{pente}" : $"{pente}";
        string valorMunicao = municao < 10 ? $"0{municao}" : $"{municao}";
        txtMunicao.text = $"{valorPente}/{valorMunicao}";
    }

    public void DecrementarVidaJogador(int dano){
        vidaJogador -= dano;
        if(vidaJogador <= 0){
            vidaJogador = 0;
            PlayerMng.Instance.MatarJogador();
            pnlStatusPlayer.SetActive(false);
            pnlGameOver.SetActive(true);
            Invoke("ReiniciarJogo",4.5f);
        }
        txtVida.text = $"+{vidaJogador}";
    }

    private void ReiniciarJogo(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
