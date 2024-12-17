using UnityEngine;

public class VisaoCamera : MonoBehaviour
{
    private GameObject alvo;
    public string tagAlvo;
    public RaycastHit hitAlvo;
    public GameObject AlvoVisto{
        get {return alvo;}
        private set{
            alvo = value;
            tagAlvo = alvo.tag;
        }
    }
    private GameObject ultimoInimigoVisto;
    // Start is called before the first frame update
    void Start()
    {
        alvo = null;
        ultimoInimigoVisto = null;
    }

    // Update is called once per frame
    void Update()
    {
        RayCastCamera();
    }

    private void RayCastCamera(){
        //Criar a linha apartir da camera do jogador
        Ray raio = Camera.main.ViewportPointToRay(new Vector3(0.5f,0.5f,0));
        RaycastHit hit;
        //Verificar se a linha encontrou algo
        if(Physics.Raycast(raio, out hit, Mathf.Infinity)){
            //Desenhar a linha no modo editor do unity
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward)* hit.distance, Color.red);
            AlvoVisto = hit.transform.gameObject;
            hitAlvo = hit;
            if(tagAlvo == "Inimigo")
            {
                if(ultimoInimigoVisto != hit.transform.gameObject && ultimoInimigoVisto!=null)
                {
                    ultimoInimigoVisto.GetComponent<InimigoControlador>().OcultarBarraDeVida();
                }
                AlvoVisto.GetComponent<InimigoControlador>().ExibirBarraDeVida();
                ultimoInimigoVisto = hit.transform.gameObject;
            }
            else
            {
                if(ultimoInimigoVisto != null)
                {
                    ultimoInimigoVisto.GetComponent<InimigoControlador>().OcultarBarraDeVida();
                    ultimoInimigoVisto = null;
                }
            }
        }
        else{
            tagAlvo = "";
            alvo = null;
        }
    }
}
