using UnityEngine;
using System.Collections.Generic;

// criação do menu para o banco de receitas e sistema de porções
[CreateAssetMenu(fileName = "BancoDeReceitas", menuName = "Sistema de Pocoes/Banco Central")] //Criação da lista de dados

public class ListaDePorcao : ScriptableObject
{
    // Lista de receitas
    public List<MoldeReceita> todasAsReceitas = new List<MoldeReceita>(); //Lista das 20 porções armazenadas
}