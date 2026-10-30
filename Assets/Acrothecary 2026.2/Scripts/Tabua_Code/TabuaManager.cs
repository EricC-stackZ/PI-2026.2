using UnityEngine;
using System.Collections.Generic;

public class TabuaManager : MonoBehaviour
{
    [Header("Configurações")]
    public ListaDePorcao bancoDeReceitas; //Lista com as 20 porções

    [Header("A Grande Ponte (Módulo 2)")]
    public Transform localDeSpawnDaPocao; //Pega a posição que o remedio pronto aparece

    [Header("UI Visual (As Vasilhas no Balcão)")]
    public GameObject[] imagensVasilhas; //Imagens do pote cheio etc

    private Stack<IngredientType> ingredientesNaTabua = new Stack<IngredientType>(); //Pilha para receber os ingredientes clicados

    private GameObject porcaoPronta; //Variável para armazenar a porção pronta

    
    public bool AdicionarIngrediente(IngredientType novoIngrediente)//Metodo baseado na lista de ingredientes
    {
        if(porcaoPronta != null) //Verifica se a porção já foi criada
        {
            Debug.Log("[VISUAL ERRO] Porção já criada. Limpe a mesa antes de adicionar novos ingredientes.");
            return false; //Não aceita mais ingredientes se a porção já foi criada
        }

        if (ingredientesNaTabua.Count < 3) //Verifica se a pilha tá vazia
        {
            ingredientesNaTabua.Push(novoIngrediente); //Adiciona o ingrediente clicado

            AudioManager.instance.TocarSFX(AudioManager.instance.somIngrediente);

            imagensVasilhas[ingredientesNaTabua.Count - 1].SetActive(true); //Adiciona a imagem do pote enchendo
            
            Debug.Log($"[VISUAL] Vasilha {ingredientesNaTabua.Count} encheu com: {novoIngrediente}");

            if (ingredientesNaTabua.Count == 3) //se preencher tudo, verifica a receita
            {
                VerificarReceita(); 
            }

            return true; //Retorna true se o ingrediente foi aceito
        }
        return false; //Retorna false se a pilha estiver cheia e não aceitar mais ingredientes
    }

    private void VerificarReceita() //Verifica se a receita bate com os ingredientes da porção
    {
        bool receitaEncontrada = false;

        foreach (MoldeReceita receita in bancoDeReceitas.todasAsReceitas) //Vasculha todas as receitas
        {
            if (ReceitaBate(receita)) //chama o verificador para ver se retorna true ou false
            {
                Debug.Log($"[VISUAL SUCESSO] Receita Certa! Criando a poção: {receita.nomePorcao}");
                receitaEncontrada = true;
                
                if(receita.prefabDestaPocao != null && localDeSpawnDaPocao != null) //Instancia a prefab para que a porção armazenada apareça
                {
                    porcaoPronta = Instantiate(receita.prefabDestaPocao, localDeSpawnDaPocao.position, Quaternion.identity);
                    AudioManager.instance.TocarSFX(AudioManager.instance.somPocaoFeita);
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

    private bool ReceitaBate(MoldeReceita receita) //Função para verificar se os ingredientes bate com os que estão configurados
    {
        List<IngredientType> copiaRequisitos = new List<IngredientType>(receita.requisitosDeIngredientes); //Lista que recebe a copia de ingredientes selecionados
        foreach (IngredientType ing in ingredientesNaTabua)
        {
            if (copiaRequisitos.Contains(ing)) copiaRequisitos.Remove(ing); //Verifica se tem algum requisito que se enquadra
            else return false; //Se não bater, retorna false
        }
        return copiaRequisitos.Count == 0; //Se bater, retorna true e limpa a lista de copia para uma nova verificação
    }

    public void RemoverUltimoIngrediente()//Botão de remover ingrediente
    {
        if(porcaoPronta != null) //Verifica se a porção já foi criada
        {
            Debug.Log("[VISUAL ERRO] Porção já criada. Limpe a mesa antes de adicionar novos ingredientes.");
            return;
        }

        if (ingredientesNaTabua.Count > 0)
        {
            imagensVasilhas[ingredientesNaTabua.Count - 1].SetActive(false); //Troca imagem de pode enchendo pra esvaziando
            IngredientType removido = ingredientesNaTabua.Pop(); //Remove da pilha
            AudioManager.instance.TocarSFX(AudioManager.instance.somDesfazer);
            Debug.Log($"[VISUAL] Lixeira: {removido} removido.");
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
}