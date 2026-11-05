using UnityEngine;
using UnityEngine.SceneManagement;

public class UiScript : MonoBehaviour
{
    [SerializeField] // Vetor de objetos vazios para manipulação da UI no canvas...
    private GameObject[] menuInicial; // Índice 0 = Botões do Menu || Índice 1 = Canvas Principal do Menu || Índice 2 = Mostrar Créditos

    [SerializeField]
    private GameObject[] cenasIniciais; // Índice 0 = Canvas Secundário || Índice 1 = Cena1 || Índice 2 = Cena2 || Índice 3 = Cena3 || Índice 4 = Cena4

    private int contagem = 1; // Usada para desativação de objetos na cena.

    public void IniciarJogo() // Método para desativar os botões do menu e a imagem de fundo...
    {
        menuInicial[1].SetActive(false); cenasIniciais[0].SetActive(true);
    }

    public void MostrarCreditos() // Método para mostrar a tela de créditos e desativar os botões do menu...
    {
        menuInicial[2].SetActive(true); menuInicial[0].SetActive(false);
    }

    public void FecharCreditos() // Método para fechar a tela de créditos e reativar os botões do menu...
    {
        menuInicial[2].SetActive(false); menuInicial[0].SetActive(true);
    }

    public void TelasIniciais() // Método para desativar as imagens iniciais uma por uma pelo clique do botão, usando a variável (contagem) como índice para desativar o elemento correto do vetor.
    {
        if(contagem < cenasIniciais.Length - 1)
        {
            cenasIniciais[contagem].SetActive(false);
        }

        else // Se a variável (contagem) for maior ou igual a 4 (tamanho do vetor [5] - 1) significa que chegou a tela final, e o próximo clique vai mudar de cena.
        {
            SceneManager.LoadScene(6);
        }

        contagem++;
    }

    public void FecharJogo() // Método para fechar/sair do jogo...
    {
        Application.Quit();

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Fechar o editor da Unity (Teste de Saída do Game)
        #endif
    }
}
