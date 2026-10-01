using UnityEngine;

public class PoteIngrediente : MonoBehaviour
{
    [Header("Configuração do Pote")]
    [Tooltip("Escolha qual é o ingrediente que este pote vai deitar na tábua.")]
    public IngredientType tipoDesteIngrediente; 
    
    [Tooltip("Arraste o objeto TabuaManager da sua cena para aqui.")]
    public TabuaManager tabuaManager; 

    [Header("Estoque (Mecânica 1)")]
    public int estoqueAtual = 10;

    // Esta função será chamada quando o jogador clicar no botão deste pote
    public void ClicarNoPote()
    {
        // 1. Verifica se ainda há ingredientes no frasco
        if (estoqueAtual > 0)
        {
            // 2. Envia a informação para o cérebro da tábua
            tabuaManager.AdicionarIngrediente(tipoDesteIngrediente);
            
            // 3. Reduz o stock em 1 unidade
            estoqueAtual--;
            
            Debug.Log("Restam " + estoqueAtual + " de " + tipoDesteIngrediente);
        }
        else
        {
            // Se chegar a zero, bloqueia o clique (a mecânica do timer de 3s farei depois)
            Debug.Log("Estoque Vazio! Aguarde a recarga.");
        }
    }
}