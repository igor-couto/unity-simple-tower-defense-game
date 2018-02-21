using UnityEngine;

public class InicializadorDeFase : MonoBehaviour {

	void Start()
	{
		// Diz para o gerenciador de jogo quem é o canvas desta fase e pede para atualizar a UI
        GerenciadorJogo.instancia.canvas = GameObject.FindGameObjectWithTag("CanvasPrincipal");
        GerenciadorJogo.instancia.AtualizaUI();
    }

	// Comportamento do botão de adicionar uma nova torre
    public void BotaoAdicionarTorre()
    {
        GerenciadorJogo.instancia.BotaoAdicionarTorre( );
    }
}