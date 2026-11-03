using UnityEngine;
//Configuração das porções feitas
[CreateAssetMenu(fileName = "NovaReceita", menuName = "Sistema de Pocoes/Receita")] //Criação de objeto para as 20 porções armazenadas
public class MoldeReceita : ScriptableObject
{
    [Header("Informações Básicas")]
    public string nomePorcao; //Nome da porção
    
    [Tooltip("Arraste o PREFAB azul desta poção específica para aqui")]
    public GameObject prefabDestaPocao; //Prefabs das porções com collider e codigo de arraste de eric cauã
    
    [Header("Ingredientes Necessários (Exatamente 3)")]
    public IngredientType[] requisitosDeIngredientes = new IngredientType[3]; //Requisito de ingredientes

    [Header("Fase de Liberação")]
    public int faseDesbloqueada = 1; //Fase liberada
}