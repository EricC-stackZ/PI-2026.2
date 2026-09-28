using UnityEngine;
using System.Collections.Generic;

// O CreateAssetMenu fica do lado de fora!
[CreateAssetMenu(fileName = "BancoDeReceitas", menuName = "Sistema de Pocoes/Banco Central")]
public class ListaDePorcao : ScriptableObject
{
    // Lista de receitas
    public List<MoldeReceita> allRecipes = new List<MoldeReceita>();
}