using UnityEngine;

//Menu para registrar nova receita
[CreateAssetMenu(fileName = "NovaReceita", menuName = "Sistema de Pocoes/Receita")]

public class MoldeReceita : ScriptableObject
{
    [Header("Informações Básicas")]
    public string nomePorcao; 
    public Sprite porcaoSprite; 
    
    [Header("Ingredientes Necessários (Exatamente 3)")]
    public IngredientType[] requisitosDeIngredientes = new IngredientType[3];

    [Header("Fase de Liberação")]
    [Tooltip("Em qual fase essa poção começa a aparecer? (Ajuda no Módulo 4)")]
    public int faseDesbloqueada = 1; 
}//Muito util para cirar os remedios e seus ingredientes (combinações)