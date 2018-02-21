using UnityEngine;

public class FonteInimigos : MonoBehaviour
{
    float cronometro = 0.0f;
    int ondasRealizadas = 0;
    Onda ondaAtual;

    public GameObject inimigo;
    public float tempoEntreOndas;
    public int quantidadeDeOndas;
    public int inimigosPorOnda;

    void Update()
    {
		// Se não há mais ondas para realizar e não esta acontecendo nenhuma onda
		if (ondasRealizadas == quantidadeDeOndas && ondaAtual == null) {
			// Avisamos para o gerenciador de jogo que esta fase está completa e desativamos este script
            GerenciadorJogo.instancia.FaseCompleta();
            gameObject.SetActive(false);
        }

		// Se não es		tamos realizando uma onda no momento
        if (ondaAtual == null)
        {
			// Incrementa o cronometro
            cronometro = cronometro + Time.deltaTime;
        } 

		// Se esta na hora de iniciar uma nova onda
        if (cronometro >= tempoEntreOndas)
        {
			// Adicionamos o script de onda neste GameObject
            ondaAtual = gameObject.AddComponent<Onda>();
			// Inicializamos suas variaveis publicas
            ondaAtual.numeroInimigos = inimigosPorOnda;
            ondaAtual.inimigo = inimigo;
			// Contamos esta onda como realizada e zeramos o cronometro
            ondasRealizadas = ondasRealizadas + 1;
            cronometro = 0.0f;
        }
    }
}