using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransitionController : MonoBehaviour
{
    [SerializeField]
    private Transform canvasSecundario; 

    [SerializeField]
    private GameObject[] transiTela; // Índice 1 = Tela de Vitória || Índice 2 = Tela de Derrota;
    
    public Image referenciaImage;
    public Sprite[] imagemNova;
    private int vidasAtuais = 3;
    private int clientesAtendidos;

    public TextMeshProUGUI textCliente;

    public ClienteSpawner clienteSpawner;
    public static TransitionController instance;
    
    public static class GameData // Classe estática que guarda o valor do int, sem recriar a variável para não perder o seu valor sempre que mudar de cena.
    {
        public static int avancarCena = 1;

        public static int faseAtual = 1;
    }

    private void Awake()
    {
        // Ao iniciar o jogo, ele define que a "instance" global é este exato script
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update() // Código de Teste
    {
        if(Keyboard.current == null) return;

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            GanharClientes();
        }

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            PerderVida();
        }
    }

    public void GanharClientes()
    {
        clientesAtendidos++;
        int clientesNecessarios = 0;

        // Define qual é a meta dependendo da fase
        if (GameData.faseAtual == 1) clientesNecessarios = 4;
        else if (GameData.faseAtual == 2) clientesNecessarios = 6;
        else if (GameData.faseAtual == 3) clientesNecessarios = 10;
        else if (GameData.faseAtual == 4) clientesNecessarios = 13;

        textCliente.text = "Clientes Atendidos : " + clientesAtendidos + "/" + clientesNecessarios; // Atualiza o texto na tela uma única vez

        if (clientesAtendidos >= clientesNecessarios) // Verifica se o jogador atingiu a meta da fase
        {
            vidasAtuais = 6;

            TelaVitoria(); clienteSpawner.OnWhil = false;
        
            if (GameData.faseAtual < 4) // Só avança de fase se não estiver na última (Fase 4)
            {
                GameData.faseAtual++;
                clientesAtendidos = 0; // Zera para a próxima fase
            }
        }
    }

    public void PerderVida()
    {
        vidasAtuais--;

        switch (vidasAtuais)
        {
            case 2:
                    referenciaImage.sprite = imagemNova[0];
                break;

            case 1:
                    referenciaImage.sprite = imagemNova[1];
                break;
            
            case 0:
                    TelaDerrota(); clienteSpawner.OnWhil = false;
                break;
        }
    }

    public void TelaVitoria() // Instancia a Tela de Vitória na posição (0,0,0) no Canvas Secundário.
    {
        GameObject novoElemento = Instantiate(transiTela[0], canvasSecundario);

        novoElemento.transform.localPosition = Vector3.zero;
    }

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
