using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EjetarCapsula : MonoBehaviour
{
    public Rigidbody rigidBody;
    
    public void Ejetar(){
        //a capsula deve ser parente da cena
        gameObject.transform.SetParent(null);
        //Aplicar uma força para ejetar
        rigidBody.AddForce(transform.TransformDirection(new Vector3(100,0,0)));
        //Aplicar uma força na rotação
        rigidBody.AddTorque(transform.right * 1.5f);
        //Destruir a capsula depois de um tempo
        Destroy(gameObject,3f);
    }
}
