using UnityEngine;
using System.Collections.Generic;

public class TabuaManager : MonoBehaviour
{
    [Header("Configurações")]//adiciona o banco de receitas
    public ListaDePorcao bancoDeReceitas; 
    
    [Header("Ingredientes Atuais (Apenas visualização)")] //Apenas visualizar as porções
    public List<IngredientType> ingredientesNaTabua = new List<IngredientType>();

    // --- FUNÇÃO 1: RECEBER DO POTE ---
    public void AdicionarIngrediente(IngredientType novoIngrediente)
    {
        if (ingredientesNaTabua.Count < 3) //Verifica se tem itens a adicionar ainda
        {
            ingredientesNaTabua.Add(novoIngrediente);
            //aqui é pra quando adicionar um ingrediente na tábua
            Debug.Log("Tábua recebeu: " + novoIngrediente);

            if (ingredientesNaTabua.Count == 3)
            {
                VerificarReceita(); //Verificador de receitas
            }
        }
    }

    // --- FUNÇÃO 2: A LIXEIRA (Desfazer) ---
    public void RemoverUltimoIngrediente()
    {
        if (ingredientesNaTabua.Count > 0)//Verifica se a lista está vazia
        {
            ingredientesNaTabua.RemoveAt(ingredientesNaTabua.Count - 1);//remove o ultimo adicionado pelo indice
            //aqui, remove a imagem do ingrediente
            Debug.Log("Último ingrediente removido.");
        }
    }

    // --- FUNÇÃO 3: VERIFICADOR DE RECEITA ---
    private void VerificarReceita()
    {
        bool receitaEncontrada = false;//verifica se tem.

        // Atualizado: allRecipes virou todasAsReceitas
        foreach (MoldeReceita receita in bancoDeReceitas.todasAsReceitas)//Varre a lista de porções
        {
            if (ReceitaBate(receita))//caso exista porção com a receita registrada
            {
                //coloca imagem do remedio feito aparecendo
                Debug.Log("SUCESSO! Você criou: " + receita.nomePorcao);
                receitaEncontrada = true;
                // TODO: Aqui vamos instanciar a Poção Pronta (Módulo 2 do Eric)
                
                ingredientesNaTabua.Clear(); //limpa a lista de ingredientes, para se montar novamente
                break;
            }
        }

        if (!receitaEncontrada)//se não existir, limpa a mesa e perde os ingredientes
        {
            Debug.Log("ERRO! Essa mistura não formou nada.");
            ingredientesNaTabua.Clear(); 
        }
    }

    private bool ReceitaBate(MoldeReceita receita)//Checklist de ingredientes booleana
    {
        
        List<IngredientType> copiaRequisitos = new List<IngredientType>(receita.requisitosDeIngredientes);
        
        foreach (IngredientType ing in ingredientesNaTabua)//verifica ingrediente por ingrediente
        {
            if (copiaRequisitos.Contains(ing))//caso tenha algum da tábua, remove.
            {
                copiaRequisitos.Remove(ing);
            }
            else
            {
                return false; 
            }
        }
        return copiaRequisitos.Count == 0; //retorna verdadeiro se tiver os três items.
    }
}