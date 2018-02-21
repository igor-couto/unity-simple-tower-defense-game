using UnityEngine;

public class Projetil : MonoBehaviour {

    public GameObject alvo;
    public float velocidade = 10.0f;

    void Update ()
    {
		// Se o alvo não existe mais (chegou no destino ou morreu no caminho) 
        if (alvo == null)
        {
			// Destrua este projetil
            Destroy(gameObject);
        }
        else
        {
			// Se mova em direcão ao alvo
            transform.position = Vector3.MoveTowards(transform.position, alvo.transform.position, velocidade * Time.deltaTime);
            transform.LookAt(alvo.transform.position);
        }
    }
}
