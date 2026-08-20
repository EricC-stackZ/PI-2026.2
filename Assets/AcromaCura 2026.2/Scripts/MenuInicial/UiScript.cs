using UnityEngine;

public class UiScript : MonoBehaviour
{
    [SerializeField] // Vetor de objetos vazios para manipulação da UI no canvas...
    GameObject[] menuInicial;

    public void IniciarJogo() // Método para desativar os botões do menu e a imagem de fundo...
    {
        menuInicial[0].SetActive(false);

        menuInicial[1].SetActive(false);
    }

    public void MostrarCreditos() // Método para mostrar a tela de créditos e desativar os botões do menu...
    {
        menuInicial[2].SetActive(true);

        menuInicial[0].SetActive(false);
    }

    public void FecharCreditos() // Método para fechar a tela de créditos e reativar os botões do menu...
    {
        menuInicial[2].SetActive(false);

        menuInicial[0].SetActive(true);
    }

    public void MostrarTutorial() // Método para mostrar a tela de tutorial e desativar os botões do menu...
    {
        menuInicial[3].SetActive(true);

        menuInicial[0].SetActive(false);
    }

    public void FecharTutorial() // Método para fechar a tela de tutorial e reativar os botões do menu...
    {
        menuInicial[3].SetActive(false);

        menuInicial[0].SetActive(true);
    }

    public void FecharJogo() // Método para fechar/sair do jogo...
    {
        Application.Quit();

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Fechar o editor da Unity (Teste de Saída do Game)
        #endif
    }
}
