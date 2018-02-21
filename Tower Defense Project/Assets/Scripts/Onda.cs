using UnityEngine;

public class Onda : MonoBehaviour {

    float cronometro = 0.0f;
    float tempoEntreInimigos = 1.0f;
    int inimigosInstanciados = 0;

    public GameObject inimigo;
    public int numeroInimigos;

    void Update ()
    {
		// Se já instanciou todos os inimigos que deveria
        if ( inimigosInstanciados == numeroInimigos)
        {
			// Destrua este script
            Destroy(this);
        }

		// Se está na hora de fazer o spaw de um inimigo
        if (cronometro >= tempoEntreInimigos)
        {
			// Instancia o inimigo, conta este inimigo e zera o cronometro
            Instantiate(inimigo, transform.position, Quaternion.identity);
            inimigosInstanciados = inimigosInstanciados + 1;
            cronometro = 0.0f;
        }
		// Incrementa o cronômetro
        cronometro = cronometro + Time.deltaTime;	
	}
}
