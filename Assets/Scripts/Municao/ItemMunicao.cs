using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemMunicao : MonoBehaviour
{
    public GameObject pentePistola;
    public GameObject penteFuzil;
    public TextMeshProUGUI txtQtdMunicao;
    private int municaoParaPistola;
    private int municaoParaFuzil;
    private int idArma;
    // Start is called before the first frame update
    void Start()
    {
        idArma = new System.Random().Next(1,3);
        switch(idArma){
            case 1:
                municaoParaPistola = new System.Random().Next(5,21);
                txtQtdMunicao.text = $"x{municaoParaPistola}";
                penteFuzil.SetActive(false);
            break;
            case 2:
                municaoParaFuzil = new System.Random().Next(15,51);
                txtQtdMunicao.text = $"x{municaoParaFuzil}";
                pentePistola.SetActive(false);
            break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Faz o objeto olhar para o jogador
        txtQtdMunicao.transform.LookAt(
            new Vector3(
                PlayerMng.Instance.transform.position.x,
                txtQtdMunicao.transform.position.y,
                PlayerMng.Instance.transform.position.z
            )
        );
    }

    private void OnTriggerEnter(Collider other){
        //Verificar se o jogador colidiu com o item
        if(other.gameObject.tag.Equals("Player")){
            switch(idArma){
                case 1:
                PlayerMng.disparoPlayer.IncrementarMunicaoPistola(municaoParaPistola);//Incrementar munição para pistola
                break;
                case 2:
                PlayerMng.disparoPlayer.IncrementarMunicaoFuzil(municaoParaFuzil);//Incremnetar munição para fuzil
                break;
            }
            Destroy(gameObject);
        }
    }
}
