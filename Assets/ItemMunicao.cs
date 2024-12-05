using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemMunicao : MonoBehaviour
{
    public int municaoParaPistola;
    public int municaoParaFuzil;
    public int idArma;
    public GameObject pentePistola;
    public GameObject penteFuzil;
    public TextMeshProUGUI txtQtdMunicao;
    // Start is called before the first frame update
    void Start()
    {
        idArma = new System.Random().Next(1, 3);
        switch (idArma)
        {
            case 1:
                municaoParaPistola = Random.Range(5, 20);
                txtQtdMunicao.text = $"x{municaoParaPistola}";
                penteFuzil.SetActive(false);
                break;
            case 2:
                municaoParaFuzil = Random.Range(15, 50);
                txtQtdMunicao.text = $"x{municaoParaFuzil}";
                pentePistola.SetActive(false);
                break;
        }
    }

    private void Update()
    {
        txtQtdMunicao.transform.LookAt(
            new Vector3(
                PlayerMng.Instance.gameObject.transform.position.x,
                txtQtdMunicao.transform.position.y,
                PlayerMng.Instance.gameObject.transform.position.z)
            );
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            switch (idArma)
            {
                case 1:
                    PlayerMng.disparoPlayer.IncrememtarMunicaoPistola(municaoParaPistola);
                    break;
                case 2:
                    PlayerMng.disparoPlayer.IncrememtarMunicaoFuzil(municaoParaFuzil);
                    break;
            }
            Destroy(gameObject);
        }
    }
}
