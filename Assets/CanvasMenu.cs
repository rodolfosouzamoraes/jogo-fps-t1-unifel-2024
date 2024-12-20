using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasMenu : MonoBehaviour
{
    public GameObject pnlMenu;
    public GameObject pnlVolume;
    public Slider sldVFX;
    public Slider sldMusica;
    private Volume volumes;
    
    void Start(){
        ExibirMenu();
        pnlVolume.SetActive(false);
        ConfigurarPainelConfiguracoes();
        AudioMng.Instance.PlayAudioMenu();
        CanvasLoading.Instance.OcultarTelaDeCarregamento();
    }
    public void Jogar(){
        CanvasLoading.Instance.ExibirTelaDeCarregamento();
        SceneManager.LoadScene(1);
    }

    public void Fechar(){
        Application.Quit();
    }

    private void ConfigurarPainelConfiguracoes(){
        volumes = DBMng.ObterVolumes();
        sldVFX.value = volumes.vfx;
        sldMusica.value = volumes.musica;
        AudioMng.Instance.MudarVolumes(volumes);
    }
    public void MudarVolumeVFX(){
        DBMng.SalvarVolume(sldVFX.value, volumes.musica);
        AtualizarVolumes();
    }

    public void MudarVolumeMusica(){
        DBMng.SalvarVolume(volumes.vfx, sldMusica.value);
        AtualizarVolumes();
    }

    private void AtualizarVolumes(){
        volumes = DBMng.ObterVolumes();
        AudioMng.Instance.MudarVolumes(volumes);
    }

    public void ExibirMenu(){
        pnlMenu.SetActive(true);
        pnlVolume.SetActive(false);
    }
    public void ExibirVolume(){
        pnlMenu.SetActive(false);
        pnlVolume.SetActive(true);
    }
}
