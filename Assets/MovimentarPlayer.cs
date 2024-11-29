using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MovimentarPlayer : MonoBehaviour
{
    public Camera playerCamera;
    public float velocidadeCaminhada = 6f;
    public float velocidadeCorrida = 12f;
    public float forcaPulo = 7f;
    public float forcaGravidade = 10f;
    public float velocidadeCamera = 2f;
    public float limiteCameraX = 45f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
