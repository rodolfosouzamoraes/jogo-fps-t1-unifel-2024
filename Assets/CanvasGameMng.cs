using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasGameMng : MonoBehaviour
{
    public static CanvasGameMng Instance;
    
    void Awake(){
        if(Instance == null){
            Instance = this;
            return;
        }
        Destroy(gameObject);
    }

    public TextMeshProUGUI txtVida;
    public TextMeshProUGUI txtMunicao;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
