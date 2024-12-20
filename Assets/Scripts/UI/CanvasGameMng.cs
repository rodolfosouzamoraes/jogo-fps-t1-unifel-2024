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
    
    public GameObject pnlTopo;
    public GameObject[] iconesChaves;
    public TextMeshProUGUI txtTempo;
    public TextMeshProUGUI txtObjetivo;
    private float totalTempo;
    private int tempoFinal;

    public GameObject pnlFimDeJogo;
    public TextMeshProUGUI txtTempoFinal;

    private int maxChave;
    private int totalChavesColetadas;
    public bool fimDeJogo;

    public TextMeshProUGUI txtTotalZumbisMortos;
    private int totalZumbisMortos;

    void Start(){
        txtVida.text = $"+{vidaJogador}";
        maxChave = FindObjectsOfType<ItemChave>().Length;
        totalChavesColetadas = 0;
        fimDeJogo = false;
        txtTempo.text = "0";
        txtObjetivo.text = "Colete as 7 chaves!";
        totalZumbisMortos = 0;
        AudioMng.Instance.PlayAudioGame();
        CanvasLoading.Instance.OcultarTelaDeCarregamento();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            VoltarParaMenu();
        }

        if(fimDeJogo == true) return;
        ContarTempo();
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
        if(fimDeJogo == true) return;

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

    public void IncrementarVidaJogador(){
        vidaJogador +=25;
        if(vidaJogador >100){
            vidaJogador = 100;
        }
        txtVida.text = $"+{vidaJogador}";
    }

    public void ReiniciarJogo(){
        CanvasLoading.Instance.ExibirTelaDeCarregamento();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void IncrementarChave(){
        totalChavesColetadas++;
        iconesChaves[totalChavesColetadas].SetActive(true);
        if(ColetouTodasAsChaves() == true){
            txtObjetivo.text = "Encontre o portão final!";
        }
    }

    public bool ColetouTodasAsChaves(){
        return totalChavesColetadas == maxChave ? true : false;
    }

    private void ContarTempo(){
        totalTempo += Time.deltaTime;
        txtTempo.text = $"{(int)totalTempo}";
    }

    public void ExibirTelaFinal(){
        fimDeJogo = true;
        tempoFinal = (int) totalTempo;
        txtTempoFinal.text = $"{tempoFinal}s";
        txtTotalZumbisMortos.text = $"{totalZumbisMortos}";
        DBMng.SalvarDados(totalZumbisMortos,tempoFinal);
        pnlFimDeJogo.SetActive(true);
        pnlStatusPlayer.SetActive(false);
        pnlTopo.SetActive(false);
        DesbloquearMouse();
        PlayerMng.disparoPlayer.DesabilitarArmas();
    }

    public void VoltarParaMenu(){
        DesbloquearMouse();
        CanvasLoading.Instance.ExibirTelaDeCarregamento();
        SceneManager.LoadScene(0);
    }

    public void IncrementarMortesZumbi(){
        totalZumbisMortos++;
    }

    public void DesbloquearMouse(){
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
