using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TransitionController : MonoBehaviour
{
    [SerializeField]
    private Transform canvasSecundario; 

    [SerializeField]
    private GameObject[] transiTela; // Índice 1 = Tela de Vitória || Índice 2 = Tela de Derrota;

    public static class GameData // Classe estática que guarda o valor do int, sem recriar a variável para não perder o seu valor sempre que mudar de cena.
    {
        public static int avancarCena = 0;
    }

    private void Update() // Código de Teste
    {
        if(Keyboard.current == null) return;

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            TelaVitoria();
        }

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            TelaDerrota();
        }
    }

    // Chame esse método ao vencer o game.
    
    public void TelaVitoria() // Instancia a Tela de Vitória na posição (0,0,0) no Canvas Secundário.
    {
        GameObject novoElemento = Instantiate(transiTela[0], canvasSecundario);

        novoElemento.transform.localPosition = Vector3.zero;
    }

    // Chame esse método ao perder o game.

    public void TelaDerrota() // Instancia a Tela de Derrota na posição (0,0,0) no Canvas Secundário.
    {
        GameObject novoElemento = Instantiate(transiTela[1], canvasSecundario);
        
        novoElemento.transform.localPosition = Vector3.zero;
    }

    public void AvancarCena() // Método chamado ao clicar no botão da (Avançar) da Tela de Vitória, somando a variável estática, e entrando na verificação do case para mudar de cena.
    {
        GameData.avancarCena++;

        switch (GameData.avancarCena)
        {
            case 1:

            SceneManager.LoadScene(1);

                break;

            case 2:

            SceneManager.LoadScene(2);

                break;

            case 3:

            SceneManager.LoadScene(3);

                break;

            case 4:

            SceneManager.LoadScene(4);

                break;
        }
    }

    public void ReiniciarCena() // Método chamado ao clicar no botão (Avançar) que está na Prefab da Tela de Derrota.
    {
        int cenaAtual = SceneManager.GetActiveScene().buildIndex; // Salva o índice da cena atual.

        SceneManager.LoadScene(cenaAtual); // Carrega a cena atual novamente.
    }
}
