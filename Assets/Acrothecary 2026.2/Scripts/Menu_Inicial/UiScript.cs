using UnityEngine;

public class UiScript : MonoBehaviour
{
    [SerializeField] // Vetor de objetos vazios para manipulação da UI no canvas...
    GameObject[] menuInicial; // Índice 0 = Botões do Menu || Índice 1 = Canvas Principal do Menu || Índice 2 = Mostrar Créditos

    public void IniciarJogo() // Método para desativar os botões do menu e a imagem de fundo...
    {
        menuInicial[1].SetActive(false);
    }

    public void MostrarCreditos() // Método para mostrar a tela de créditos e desativar os botões do menu...
    {
        menuInicial[2].SetActive(true); menuInicial[0].SetActive(false);
    }

    public void FecharCreditos() // Método para fechar a tela de créditos e reativar os botões do menu...
    {
        menuInicial[2].SetActive(false); menuInicial[0].SetActive(true);
    }

    public void FecharJogo() // Método para fechar/sair do jogo...
    {
        Application.Quit();

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Fechar o editor da Unity (Teste de Saída do Game)
        #endif
    }
}
