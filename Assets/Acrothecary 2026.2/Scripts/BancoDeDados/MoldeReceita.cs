using UnityEngine;

// A etiqueta fica aqui fora, imediatamente acima da classe!
[CreateAssetMenu(fileName = "NovaReceita", menuName = "Sistema de Pocoes/Receita")]

public class MoldeReceita : ScriptableObject
{
    [Header("Informações Básicas")]
    public string recipeName; 
    public Sprite potionSprite; 
    
    [Header("Ingredientes Necessários (Exatamente 3)")]
    public IngredientType[] requiredIngredients = new IngredientType[3];

    [Header("Fase de Liberação")]
    [Tooltip("Em qual fase essa poção começa a aparecer? (Ajuda no Módulo 4)")]
    public int unlockedInPhase = 1; 
}//Muito util para cirar os remedios e seus ingredientes (combinações)