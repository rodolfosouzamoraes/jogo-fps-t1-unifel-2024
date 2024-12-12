using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InstanciarInimigos : MonoBehaviour
{
    public GameObject[] inimigos;
    public float tempoDeEspera;
    private float tempoProximoInimigo;
    // Start is called before the first frame update
    void Start()
    {

        tempoProximoInimigo = Time.time + tempoDeEspera;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > tempoProximoInimigo) 
        { 
            tempoProximoInimigo = Time.time + tempoDeEspera;
            InstanciarNovoInimigo();
        }
    }

    private void InstanciarNovoInimigo()
    {
        float posicaoZ = Random.Range(PlayerMng.Instance.transform.position.z - 20,PlayerMng.Instance.transform.position.z + 20);
        float posicaoX = Random.Range(PlayerMng.Instance.transform.position.x -20,PlayerMng.Instance.transform.position.x + 20);
        NavMeshHit hit;
        NavMesh.SamplePosition(new Vector3(posicaoX, 0, posicaoZ), out hit, Mathf.Infinity, 1);
        int inimigoSorteado = new System.Random().Next(0,inimigos.Length);
        var novoInimigo = Instantiate(inimigos[inimigoSorteado]);
        novoInimigo.transform.position = hit.position;
    }
}
