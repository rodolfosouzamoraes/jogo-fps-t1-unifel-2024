using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        AtualizaMunicaoUI();
    }

    private void AtualizaMunicaoUI()
    {
        int pente = PlayerMng.disparoPlayer.ArmaAtiva.Pente;
        int municao = PlayerMng.disparoPlayer.ArmaAtiva.MunicaoAtual;
        txtMunicao.text = $"{(pente < 10 ? $"0{pente}" : pente)}/{(municao < 10 ? $"0{municao}" : municao)}";
    }
}
