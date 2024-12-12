using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverParaFrente : MonoBehaviour
{
    public float velocidade;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * velocidade * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Dano ao jogador
        }
        Destroy(gameObject);
    }
}
