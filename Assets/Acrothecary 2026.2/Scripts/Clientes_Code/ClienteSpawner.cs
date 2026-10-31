using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class ClienteSpawner : MonoBehaviour
{
    [Header("Configuração de Spawn")]
    public GameObject[] npcsPrefabs; // Lista com os "corpos" dos clientes pra sortear
    public Transform[] posicoesBalcao; // Cadeiras vazias onde o cliente vai sentar/aparecer

    [Header("Regras da Fase")]
    public int totalClientesDaFase = 4; // (Por enquanto o código ignora isso e roda infinito pros testes)
    public float tempoEntreClientes = 5f; // Segundos que o jogo espera pra chamar o próximo
    
    [Header("Catálogo de Receitas desta Fase")]
    public Sprite[] receitasDestaFase; // Lista de imagens das poções que eles podem pedir aqui

    [Header("Dificuldade da Fase")]
    public float pacienciaDaFase = 20f; // Tempo (em segundos) que a barra do NPC vai durar nesta fase

    [Header("Controle do Jogo")]
    public bool Onjogo = false;
    public bool OnWhil = true;

    void Start()
    {
        // Dá a largada no cronômetro invisível que fica verificando se pode chamar o próximo
        StartCoroutine(RotinaDeSpawn());
    }

    void Update()
    {
        // --- PARA ERIC ---
        // Faça a verificação contínua aqui: cheque se o jogador atingiu a meta de clientes atendidos (Vitória) 
        // ou se ele perdeu as 3 vidas (Derrota), e chame as telas correspondentes.
    }

    public void AtivarJogo()
    {
        Onjogo = true;
        Debug.Log("Jogo Ativado");
        AudioManager.instance.IniciarMusicaFundo(); // Chama a música de fundo lá do AudioManager
    }

    private IEnumerator RotinaDeSpawn()
    {
        // O jogo fica congelado aqui até a variável Onjogo virar true (quando aperta espaço)
        yield return new WaitUntil(() => Onjogo == true); 

        // Loop infinito: vai rodar pra sempre até a cena ser reiniciada
        // --- PARA ERIC: Quando a sua condição de vitória ou derrota for atingida, lembre de interromper a geração de clientes aqui ---
        while (OnWhil) 
        {
            // Espera o tempo definido (ex: 3, 4 ou 5 segundos) antes de tentar chamar o próximo
            yield return new WaitForSeconds(tempoEntreClientes);

            // Só verifica se tem alguma cadeira vazia. Se tiver, manda entrar na hora!
            if (ObterPosicaoVazia() != null) 
            {
                ChamarProximoCliente();
            }
        }
    }

    private void ChamarProximoCliente() 
    {
        // Busca qual das posições do balcão está vazia
        Transform posicaoVazia = ObterPosicaoVazia();
        
        // Se estiver lotado, aborta a missão e não nasce ninguém
        if (posicaoVazia == null) return; 

        // Sorteia a aparência (prefab) do NPC na hora que ele vai nascer
        GameObject prefabSorteado = npcsPrefabs[Random.Range(0, npcsPrefabs.Length)];
        
        // Faz o corpo dele aparecer de fato no balcão, na cadeira vazia que achamos
        GameObject novoCliente = Instantiate(prefabSorteado, posicaoVazia);

        // --- INJEÇÃO DE DADOS NO NPC ---
        // 1. Manda o tempo de paciência dessa fase pra barra dele
        novoCliente.GetComponent<Npcs_Code>().DefinirPaciencia(pacienciaDaFase);

        // 2. Sorteia um dos remédios disponíveis nesta fase
        Sprite receitaSorteada = receitasDestaFase[Random.Range(0, receitasDestaFase.Length)];
        
        // 3. Cola a imagem do remédio no balãozinho do NPC
        novoCliente.GetComponent<Npcs_Code>().ConfigurarPedido(receitaSorteada);
    }

    // Função matemática que conta quantas pessoas estão ocupando cadeiras
    private int ClientesNoBalcao()
    {
        int ocupados = 0;
        foreach (Transform pos in posicoesBalcao)
        {
            // Se tiver um "filho" (um NPC) dentro desse ponto, conta +1
            if (pos.childCount > 0) ocupados++;
        }
        return ocupados; // Retorna o total lotado
    }

    // Função que procura a primeira cadeira livre e devolve o lugar exato
    private Transform ObterPosicaoVazia()
    {
        foreach (Transform pos in posicoesBalcao)
        {
            // Se o ponto não tiver nenhum "filho", achamos o lugar vazio!
            if (pos.childCount == 0) return pos; 
        }
        return null; // Se bateu em todos e não achou, retorna nulo (balcão lotado)
    }
}