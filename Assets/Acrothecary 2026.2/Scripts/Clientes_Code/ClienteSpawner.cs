using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ClienteSpawner : MonoBehaviour
{
    [Header("Configuração de Spawn")]
    public GameObject[] npcsPrefabs; //Adiciona as prefabs dos clientes
    public Transform[] posicoesBalcao; //Adiciona onde os clientes vão nascer

    [Header("Regras da Fase")]
    public int totalClientesDaFase = 4; //Total de clientes da fase
    public float tempoEntreClientes = 5f; //Tempo para aparecer um novo cliente
    
    [Header("Catálogo de Receitas desta Fase")]
    public Sprite[] receitasDestaFase; //Remedios que os clientes vão pedir

    private Queue<GameObject> filaDeEspera = new Queue<GameObject>(); //Fila dos clientes

    void Start()
    {
        // --- PARA ERIC ---
        // Aqui o jogo começa. pode adicionar a meta de clientes a ser atendidos aqui

        PrepararFilaDeClientes();
        StartCoroutine(RotinaDeSpawn());
    }

    private void PrepararFilaDeClientes() //Sorteio do npc que vai aparecer pelo indice
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

            if (ObterPosicaoVazia() != null && filaDeEspera.Count > 0) //Enquanto não tiver clientes, chama mais 1
            {
                ChamarProximoCliente();
            }
        }
    }

    private void ChamarProximoCliente() //Metodo para chamar cliente
    {
        Transform posicaoVazia = ObterPosicaoVazia();
        if (posicaoVazia == null) return; 

        GameObject prefabDoProximo = filaDeEspera.Dequeue();
        GameObject novoCliente = Instantiate(prefabDoProximo, posicaoVazia);

        Sprite receitaSorteada = receitasDestaFase[Random.Range(0, receitasDestaFase.Length)];
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