using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMng : MonoBehaviour
{
    public static PlayerMng Instance;
    public static VisaoCamera visaoCamera;
    public static DisparoPlayer disparoPlayer;
    public static MovimentarPlayer movimentarPlayer;

    private void Awake()
    {
        if(Instance == null)
        {
            visaoCamera = GetComponentInChildren<VisaoCamera>();
            disparoPlayer = GetComponent<DisparoPlayer>();
            movimentarPlayer = GetComponent<MovimentarPlayer>();
            Instance = this;
            return;
        }
        Destroy(gameObject);
    } 
}
