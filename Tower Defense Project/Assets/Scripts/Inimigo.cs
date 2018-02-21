using UnityEngine;

public class Inimigo : MonoBehaviour
{
    #region Variaveis

    public float velocidade;
    public int vida;
    public int dinheiroGerado;

    GameObject caminho;
    GameObject pontoDestino;
    int numeroPontoDestino = 0;

    #endregion

    void Start ()
    {
        caminho = GameObject.Find("_Caminho");
        pontoDestino = caminho.transform.GetChild(numeroPontoDestino).gameObject;
    }
	
	void Update ()
    {
        if ( transform.position == pontoDestino.transform.position )
        {
            if (numeroPontoDestino == caminho.transform.childCount)
            {
                GerenciadorJogo.instancia.DecrementaSaude();
                Destroy(gameObject);
            }
            else
            {
                pontoDestino = caminho.transform.GetChild(numeroPontoDestino).gameObject;
                numeroPontoDestino++;
            }
        }

        transform.position = Vector3.MoveTowards ( transform.position, pontoDestino.transform.position, velocidade * Time.deltaTime);
        transform.LookAt ( pontoDestino.transform.position );
    }

    void OnTriggerEnter( Collider info )
    {
        if ( info.gameObject.tag == "Projetil" )
        {
            Destroy(info.gameObject);
            vida = vida - 1;
            if ( vida == 0 )
            {
                GerenciadorJogo.instancia.IncrementaDinheiro(dinheiroGerado);
                Destroy(gameObject);
            }
        }
    }
}