using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ClienteSpawner : MonoBehaviour
{
    [Header("Configuração de Spawn")]
    public GameObject[] npcsPrefabs; 
    public Transform[] posicoesBalcao; 

    [Header("Regras da Fase")]
    public int totalClientesDaFase = 4; 
    public float tempoEntreClientes = 5f; 
    
    [Header("Catálogo de Receitas desta Fase")]
    [Tooltip("Coloque aqui APENAS as poções liberadas nesta fase específica.")]
    public Sprite[] receitasDestaFase; // NOVO: A lista agora fica no Spawner!

    private Queue<GameObject> filaDeEspera = new Queue<GameObject>();

    void Start()
    {
        PrepararFilaDeClientes();
        StartCoroutine(RotinaDeSpawn());
    }

    private void PrepararFilaDeClientes()
    {
        for (int i = 0; i < totalClientesDaFase; i++)
        {
            int indexSorteado = Random.Range(0, npcsPrefabs.Length);
            filaDeEspera.Enqueue(npcsPrefabs[indexSorteado]); 
        }
    }

    private IEnumerator RotinaDeSpawn()
    {
        while (filaDeEspera.Count > 0 || ClientesNoBalcao() > 0) 
        {
            yield return new WaitForSeconds(tempoEntreClientes);

            if (ObterPosicaoVazia() != null && filaDeEspera.Count > 0)
            {
                ChamarProximoCliente();
            }
        }
        Debug.Log("[SPAWNER] Expediente encerrado!");
    }

    private void ChamarProximoCliente()
    {
        Transform posicaoVazia = ObterPosicaoVazia();
        if (posicaoVazia == null) return; 

        // 1. Instancia o NPC
        GameObject prefabDoProximo = filaDeEspera.Dequeue();
        GameObject novoCliente = Instantiate(prefabDoProximo, posicaoVazia);

        // 2. Sorteia a receita baseada na lista DESTA FASE
        Sprite receitaSorteada = receitasDestaFase[Random.Range(0, receitasDestaFase.Length)];

        // 3. Envia a receita sorteada para o script do NPC exibir no balão
        novoCliente.GetComponent<Npcs_Code>().ConfigurarPedido(receitaSorteada);
    }

    private int ClientesNoBalcao()
    {
        int ocupados = 0;
        foreach (Transform pos in posicoesBalcao)
        {
            if (pos.childCount > 0) ocupados++;
        }
        return ocupados;
    }

    private Transform ObterPosicaoVazia()
    {
        foreach (Transform pos in posicoesBalcao)
        {
            if (pos.childCount == 0) return pos; 
        }
        return null; 
    }
}