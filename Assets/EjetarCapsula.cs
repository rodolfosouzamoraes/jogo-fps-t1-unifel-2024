using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EjetarCapsula : MonoBehaviour
{
    public Rigidbody myRigidbody;
    // Start is called before the first frame update
    public void Ejetar()
    {
        //Debug.DrawRay(transform.position, transform.TransformDirection(new Vector3(1,0,-1)) * 100, Color.red);
        gameObject.transform.SetParent(null);
        myRigidbody.AddForce(transform.TransformDirection(new Vector3(100, 0, 0)));
        myRigidbody.AddTorque(transform.right * 0.5f);

        //myRigidbody.velocity = transform.TransformDirection(new Vector3(1, 0, -1)*3);        
        Destroy(gameObject,3f);
    }

    private void FixedUpdate()
    {
    }
}
