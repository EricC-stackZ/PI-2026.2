using UnityEngine;
using UnityEngine.UI;
using TMPro; // Biblioteca essencial para usar o TextMeshPro (O texto do Unity)
using System.Collections;

public class PoteIngrediente : MonoBehaviour
{
    [Header("Configuração do Pote")]
    public IngredientType tipoDesteIngrediente; 
    public TabuaManager tabuaManager; 

    [Header("Estoque (Mecânica 1)")] 
    public int estoqueMaximo = 10;
    public int estoqueAtual = 10;
    private int quantidadePorRecarga = 3; 

    [Header("Interface Visual")]
    [Tooltip("Arraste o texto (TextMeshPro) que fica em cima da bolinha aqui")]
    public TextMeshProUGUI textoContador; // O texto que vai mostrar "10", "9" ou "3", "2", "1"

    [Header("Botões")]
    public Button botaoUsar; 
    public Button botaoRecarregar; 

    private bool emRecarga = false;

    void Start()
    {
        AtualizarTextoEstoque();
    }

    // --- FUNÇÃO 1: USAR O INGREDIENTE ---
    public void UsarIngrediente()
    {
        if (emRecarga) return;

        if (estoqueAtual > 0)
        {
            tabuaManager.AdicionarIngrediente(tipoDesteIngrediente);
            estoqueAtual--; 
            
            AtualizarTextoEstoque(); 
            Debug.Log($"[VISUAL] Usou 1 {tipoDesteIngrediente}. Restam: {estoqueAtual}");
        }
        else
        {
            Debug.Log("[VISUAL] Estoque Vazio! Você precisa recarregar.");
        }
    }

    // --- FUNÇÃO 2: INICIAR A RECARGA ---
    public void IniciarRecarga()
    {
        if (estoqueAtual == estoqueMaximo || emRecarga) return;

        StartCoroutine(RotinaDeRecarga());
    }

    // --- FUNÇÃO 3: O TIMER INVISÍVEL E VISUAL (A Coroutine) ---
    private IEnumerator RotinaDeRecarga()
    {
        emRecarga = true; 
        
        if(botaoUsar != null) botaoUsar.interactable = false; 
        if(botaoRecarregar != null) botaoRecarregar.interactable = false;
        
        if (textoContador != null)
        { 
            // O Unity escreve os números do timer automaticamente
            textoContador.text = "3";
            yield return new WaitForSeconds(1f);
            
            textoContador.text = "2";
            yield return new WaitForSeconds(1f);
            
            textoContador.text = "1";
            yield return new WaitForSeconds(1f);
        }
        else
        {
            yield return new WaitForSeconds(3f);
        }
        
        // Matemática da Recarga
        int espacoVazio = estoqueMaximo - estoqueAtual; 
        int totalAAdicionar = Mathf.Min(quantidadePorRecarga, espacoVazio);
        estoqueAtual += totalAAdicionar;

        // Fim da Recarga
        if(botaoUsar != null) botaoUsar.interactable = true; 
        if(botaoRecarregar != null) botaoRecarregar.interactable = true;
        emRecarga = false; 
        
        AtualizarTextoEstoque();
    }

    // --- FUNÇÃO 4: O ATUALIZADOR DO TEXTO ---
    private void AtualizarTextoEstoque()
    {
        if (textoContador != null)
        {
            // O '.ToString()' converte o número da matemática num texto que o Unity consegue ler
            textoContador.text = estoqueAtual.ToString();
        }
    }
}