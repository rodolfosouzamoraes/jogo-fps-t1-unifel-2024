using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InimigoControlador : MonoBehaviour
{
    public float vida;
    public Slider sldVida;
    // Start is called before the first frame update
    void Start()
    {
        sldVida.maxValue = vida;
        sldVida.value = vida;
    }

    // Update is called once per frame
    void Update()
    {
        sldVida.transform.LookAt(
            new Vector3(
                PlayerMng.Instance.transform.position.x,
                sldVida.transform.position.y,
                PlayerMng.Instance.transform.position.z
            )
        );
    }

    public void DecrementarVida(float dano){
        vida-=dano;
        if(vida<=0){
            Destroy(gameObject);
            return;
        }
        sldVida.value = vida;
    }
}
