using UnityEngine;
using System.Collections.Generic;

public class TabuaManager : MonoBehaviour
{
    [Header("Configurações")]
    public ListaDePorcao bancoDeReceitas; //Lista das porções e seus ingredientes

    [Header("A Grande Ponte (Módulo 2)")]
    public Transform localDeSpawnDaPocao; //Pega a posição que o remedio pronto aparece

    [Header("UI Visual (As Vasilhas no Balcão)")]
    public GameObject[] imagensVasilhas; //Imagens do pote cheio e etc

    private Stack<IngredientType> ingredientesNaTabua = new Stack<IngredientType>(); //Pilha para receber os ingredientes clicados

    public void AdicionarIngrediente(IngredientType novoIngrediente)
    {
        if (ingredientesNaTabua.Count < 3) 
        {
            ingredientesNaTabua.Push(novoIngrediente); 
            imagensVasilhas[ingredientesNaTabua.Count - 1].SetActive(true); //Adiciona a imagem do pote enchendo
            
            Debug.Log($"[VISUAL] Vasilha {ingredientesNaTabua.Count} encheu com: {novoIngrediente}");

            if (ingredientesNaTabua.Count == 3) //se preencher tudo, verifica se tem porção
            {
                VerificarReceita(); 
            }
        }
    }

    public void RemoverUltimoIngrediente()//Botão de remover ingrediente
    {
        if (ingredientesNaTabua.Count > 0)
        {
            imagensVasilhas[ingredientesNaTabua.Count - 1].SetActive(false); //Troca imagem de pode enchendo pra esvaziando
            IngredientType removido = ingredientesNaTabua.Pop(); //Remove da pilha
            Debug.Log($"[VISUAL] Lixeira: {removido} removido.");
        }
    }

    private void VerificarReceita() //Verifica se a receita bate com os ingredientes da porção
    {
        bool receitaEncontrada = false;

        foreach (MoldeReceita receita in bancoDeReceitas.todasAsReceitas)
        {
            if (ReceitaBate(receita)) //chama o verificador para ver se retorna true ou false
            {
                Debug.Log($"[VISUAL SUCESSO] Receita Certa! Criando a poção: {receita.nomePorcao}");
                receitaEncontrada = true;
                
                if(receita.prefabDestaPocao != null && localDeSpawnDaPocao != null) //Instancia a prefab para que a porção armazenada apareça
                {
                    Instantiate(receita.prefabDestaPocao, localDeSpawnDaPocao.position, Quaternion.identity);
                }
                else
                {
                     Debug.LogWarning("Atenção: O Prefab da poção ou o Local de Spawn não estão configurados no Inspector!");
                }
                
                LimparMesa();
                break;
            }
        }

        if (!receitaEncontrada)//caso retorne false
        {
            Debug.Log("[VISUAL ERRO] Mistura errada. Mesa limpa.");
            LimparMesa();
        }
    }

    private void LimparMesa() //função de tirar todas as imagens e ingredientes
    {
        foreach (GameObject vasilha in imagensVasilhas)
        {
            vasilha.SetActive(false);
        }
        ingredientesNaTabua.Clear(); 
    }

    private bool ReceitaBate(MoldeReceita receita) //Função para verificar se os ingredientes bate com os que estão configurados
    {
        List<IngredientType> copiaRequisitos = new List<IngredientType>(receita.requisitosDeIngredientes);
        foreach (IngredientType ing in ingredientesNaTabua)
        {
            if (copiaRequisitos.Contains(ing)) copiaRequisitos.Remove(ing);
            else return false; 
        }
        return copiaRequisitos.Count == 0; 
    }
}