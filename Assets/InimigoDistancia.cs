using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoDistancia : InimigoControlador
{
    public GameObject projetilZumbi;
    public void AtaqueDistancia()
    {
        var projetil = Instantiate(projetilZumbi);
        projetil.transform.position = transform.position + new Vector3(0,1,0);
        projetil.transform.rotation = transform.rotation;
    }
}
