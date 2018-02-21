using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GerenciadorJogo : MonoBehaviour
{

    #region Variaveis

    public GameObject torre;

    int vidasIniciais = 3;
    int saudeInicial = 3;
    int dinheiroInicial = 50;

    Text vidasUI;
    Text saudeUI;
    Text dinheiroUI;

    int vidas;
    int saude;
    int dinheiro;

    bool encaixesEstaoVisiveis;
    public GameObject canvas;

    #endregion

	#region Singleton

	public static GerenciadorJogo instancia = null;

    private void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
            Inicializar();
        }
        else if ( instancia != this )
        {
            Destroy ( gameObject );
        }

        DontDestroyOnLoad( gameObject );
    }

	#endregion

    void Update ()
    {
		// A cada frame, verifique se houve um toque na tela
		if ( Input.touchCount > 0 )
		{
			Touch toque = Input.GetTouch(0);

			Ray raio = Camera.main.ScreenPointToRay ( toque.position );
			RaycastHit objetoAcertado;

			// Se o toque foi valido e acertou em algo
			if (Physics.Raycast(raio, out objetoAcertado))
			{
				// Trata o que foi tocado
				AcoesToque(objetoAcertado.transform.gameObject);
			}
		}
    }

    void AcoesToque(GameObject acertado)
    {
		// Se tocou em um slot (encaixe) para colocar uma torre
        if (acertado.tag == "Encaixe")
        {
			// Se tem dinheiro suficiente
            if (dinheiro >= 50)
            {
                // Só estou fazendo as duas linhas a seguir pois a origem está no meio de nossa torre
                Vector3 posicao = acertado.transform.position;
                posicao.y = 0.5f;

				// Coloca a torre no lugar, decrementa o dinheiro, atualiza a UI e destroy o slot
                Instantiate(torre, posicao, Quaternion.identity);
                dinheiro = dinheiro - 50;
                AtualizaUI();
                Destroy(acertado);
            }

        }
		// Some com os encaixes
        EncaixesVisiveis(false);
    }

    void EncaixesVisiveis(bool visivel)
    {
        encaixesEstaoVisiveis = visivel;
        GameObject.FindGameObjectWithTag("EncaixesTorres").transform.GetChild(0).gameObject.SetActive(visivel);
    }

    public void BotaoAdicionarTorre()
    {
        EncaixesVisiveis(!encaixesEstaoVisiveis);
    }

    void Inicializar()
    {
        vidas       = vidasIniciais;
        saude       = saudeInicial;
        dinheiro    = dinheiroInicial;

        GerenciadorJogo.instancia.canvas = GameObject.FindGameObjectWithTag("CanvasPrincipal");
        LocalizaUI();
        AtualizaUI();
    }

    void LocalizaUI()
    {
        vidasUI     =    canvas.transform.Find("VidasText").GetComponent<Text>();
        saudeUI     =    canvas.transform.Find("SaudeText").GetComponent<Text>();
        dinheiroUI  =    canvas.transform.Find("DinheiroText").GetComponent<Text>();
    }

    public void AtualizaUI()
    {
        LocalizaUI();

        vidasUI.text = "Vidas: " + vidas.ToString();
        saudeUI.text = "Saude: " + saude.ToString();
        dinheiroUI.text = "$ " + dinheiro.ToString();
    }

    public void DecrementaSaude ()
    {
        saude = saude - 1;
        AtualizaUI();
        if ( saude == 0 )
        {
            vidas = vidas - 1;
            AtualizaUI();
            if (vidas > 0)
            {
                saude = saudeInicial;
                AtualizaUI();
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else
            {
                // GAME OVER :(
                Application.Quit();
            }
        }
    }

    public void IncrementaDinheiro ( int dinheiroGanho )
    {
        dinheiro = dinheiro + dinheiroGanho;
        AtualizaUI();
    }

    public void FaseCompleta()
    {
        int proximaFase = SceneManager.GetActiveScene().buildIndex + 1;
        if (proximaFase <= SceneManager.sceneCount)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            // VOCE VENCEU TODAS AS FASES! COLOCAR AQUI UMA TELA DE FIM DE JOGO
        }
        
    }
}
