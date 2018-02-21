using UnityEngine;

public class Torre : MonoBehaviour
{
    public float tempoEntreDisparos;
    public float alcance;
    public GameObject projetil;

    GameObject alvoAtual;
    float cronometro = 0.0f;

    void Start()
    {
		// Não podemos procurar o alvo a cada frame. Procurar o alvo não é uma tarefa rapida e iria causar lentidão
		// InvokeRepeating executa repetidamente o medoto "ProcuraAlvo" a cada 0.5 segundo (meio segundo)
        InvokeRepeating ( "ProcuraAlvo", 0.0f, 0.5f );
    }

    void Update ()
    {
		// Se existe um alvo para atirar
        if ( alvoAtual )
        {
			// Olhe para o alvo
            transform.LookAt ( alvoAtual.transform.position );

			// Se podemos atirar novamente, ou seja, o cooldown foi atingido
            if (cronometro >= tempoEntreDisparos)
            {
				// Zera o cronometro
                cronometro = 0.0f;
				// Instancia o projetil e o guarda em uma variavel
                GameObject projetilDisparado = Instantiate(projetil, transform.position, Quaternion.identity);
                // Diz para o projetil quem deve ser o seu alvo
				projetilDisparado.GetComponent<Projetil>().alvo = alvoAtual;
            }
        }
		// Incrementa o cronometro
        cronometro = cronometro + Time.deltaTime;
    }

    void ProcuraAlvo()
    {
		// Pega uma lista de todos os inimigos que estao no campo, buscando pela sua tag
        GameObject[] inimigosNoCampo = GameObject.FindGameObjectsWithTag ( "Inimigo" );

        float distanciaMaisCurta        = Mathf.Infinity;
        float distanciaParaInimigoAtual = Mathf.Infinity;

		// Para cada inimigo do campo
        foreach ( GameObject inimigo in inimigosNoCampo )
        {
            distanciaParaInimigoAtual = Vector3.Distance( transform.position, inimigo.transform.position );

            if ( distanciaParaInimigoAtual < distanciaMaisCurta)
            {
                distanciaMaisCurta = distanciaParaInimigoAtual;
                alvoAtual = inimigo;
            }
        }

		// Se o inimigo encontrado
        if (distanciaParaInimigoAtual > alcance)
        {
            alvoAtual = null;
        }
    }
}