using UnityEngine;
using UnityEngine.UI;
using TMPro; // Biblioteca essencial para usar o TextMeshPro (O texto do Unity)
using System.Collections;

public class PoteIngrediente : MonoBehaviour
{
    [Header("Configuração do Pote")]
    public IngredientType tipoDesteIngrediente; // Qual é o ingrediente que este pote guarda
    public TabuaManager tabuaManager; // Referência da mesa/tábua para mandar o ingrediente

    [Header("Estoque (Mecânica 1)")] 
    public int estoqueMaximo = 10; // Capacidade máxima que o pote aguenta
    public int estoqueAtual = 10;  // Quantidade de ingredientes disponíveis no momento
    private int quantidadePorRecarga = 3; // Quantos itens ganha a cada recarga

    [Header("Interface Visual")]
    [Tooltip("Arraste o texto (TextMeshPro) que fica em cima da bolinha aqui")]
    public TextMeshProUGUI textoContador; // O texto que vai mostrar "10", "9" ou a contagem da recarga ("3", "2", "1")

    [Header("Botões")]
    public Button botaoUsar;        // O botão de clicar no pote para gastar o ingrediente
    public Button botaoRecarregar;  // O botão de pedir mais ingredientes quando esvazia

    private bool emRecarga = false; // Trava de segurança para o jogador não bugar o pote enquanto ele recarrega

    void Start()
    {
        // Assim que o jogo começa, atualiza o número no visor para mostrar o estoque cheio
        AtualizarTextoEstoque();
    }

    // --- FUNÇÃO 1: USAR O INGREDIENTE ---
    public void UsarIngrediente()
    {
        // Se o pote estiver no meio da recarga, ignora o clique
        if (emRecarga) return;

        // Se ainda tem estoque, gasta um e manda pra tábua
        if (estoqueAtual > 0)
        {
            // Tenta adicionar o ingrediente na tábua e guarda se foi aceito ou não
            bool ingredienteFoiAceito = tabuaManager.AdicionarIngrediente(tipoDesteIngrediente); 

            if (ingredienteFoiAceito) // Se a tábua aceitou, desconta do estoque
            {
                estoqueAtual--;
                AtualizarTextoEstoque(); // Atualiza o número na tela
                Debug.Log($"[VISUAL] Usou 1 {tipoDesteIngrediente}. Restam: {estoqueAtual}");
            }
        }
        else
        {
            Debug.Log("[VISUAL] Estoque Vazio! Você precisa recarregar.");
        }
    }

    // --- FUNÇÃO 2: INICIAR A RECARGA ---
    public void IniciarRecarga()
    {
        // Se o pote já estiver cheio OU já estiver recarregando, não faz nada
        if (estoqueAtual == estoqueMaximo || emRecarga) return;

        // Dispara o cronômetro visual e invisível da recarga
        StartCoroutine(RotinaDeRecarga());
    }

    // --- FUNÇÃO 3: O TIMER INVISÍVEL E VISUAL (A Coroutine) ---
    private IEnumerator RotinaDeRecarga()
    {
        emRecarga = true; // Tranca o pote

        // Toca o som de despejar/recarregar ingredientes que configuramos no AudioManager
        AudioManager.instance.TocarSFX(AudioManager.instance.somRecarga);
        
        // Desativa os botões para o jogador não conseguir clicar enquanto espera
        if(botaoUsar != null) botaoUsar.interactable = false; 
        if(botaoRecarregar != null) botaoRecarregar.interactable = false;
        
        // Se o texto existir, faz a contagem regressiva na tela
        if (textoContador != null)
        { 
            textoContador.text = "3";
            yield return new WaitForSeconds(1f);
            
            textoContador.text = "2";
            yield return new WaitForSeconds(1f);
            
            textoContador.text = "1";
            yield return new WaitForSeconds(1f);
        }
        else
        {
            // Fallback de segurança caso o texto suma: espera 3 segundos em silêncio
            yield return new WaitForSeconds(3f);
        }
        
        // --- MATEMÁTICA DA RECARGA ---
        int espacoVazio = estoqueMaximo - estoqueAtual; // Calcula quanto falta para encher
        int totalAAdicionar = Mathf.Min(quantidadePorRecarga, espacoVazio); // Pega 3 ou o resto que falta
        estoqueAtual += totalAAdicionar; // Soma no estoque atual

        // --- FIM DA RECARGA ---
        // Destranca os botões para o jogador poder usar o pote de novo
        if(botaoUsar != null) botaoUsar.interactable = true; 
        if(botaoRecarregar != null) botaoRecarregar.interactable = true;
        emRecarga = false; 
        
        AtualizarTextoEstoque(); // Atualiza o visor com o novo valor cheio
    }

    // --- FUNÇÃO 4: O ATUALIZADOR DO TEXTO ---
    private void AtualizarTextoEstoque()
    {
        if (textoContador != null)
        {
            // Converte o número inteiro do estoque em texto legível para o TextMeshPro
            textoContador.text = estoqueAtual.ToString();
        }
    }
}