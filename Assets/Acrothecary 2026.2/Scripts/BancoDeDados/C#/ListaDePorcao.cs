using UnityEngine;
using System.Collections.Generic;

// criação do menu para o banco de receitas e sistema de porções
[CreateAssetMenu(fileName = "BancoDeReceitas", menuName = "Sistema de Pocoes/Banco Central")]

public class ListaDePorcao : ScriptableObject
{
    // Lista de receitas
    public List<MoldeReceita> todasAsReceitas = new List<MoldeReceita>();
}