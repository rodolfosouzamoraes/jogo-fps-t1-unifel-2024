using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleFlash : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke("OcultarMuzzleFlash", 0.1f);
    }
    private void OcultarMuzzleFlash()
    {
        gameObject.SetActive(false);
    }
}
