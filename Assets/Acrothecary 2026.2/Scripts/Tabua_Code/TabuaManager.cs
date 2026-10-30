using UnityEngine;
using System.Collections.Generic;

public class TabuaManager : MonoBehaviour
{
    [Header("Configurações")]
    public ListaDePorcao bancoDeReceitas; // Lista com o catálogo de todas as porções do jogo

    [Header("A Grande Ponte (Módulo 2)")]
    public Transform localDeSpawnDaPocao; // Posição exata onde a poção pronta aparece fisicamente na bancada

    [Header("UI Visual (As Vasilhas no Balcão)")]
    public GameObject[] imagensVasilhas; // Lista com os ícones visuais dos potes enchendo na mesa

    private Stack<IngredientType> ingredientesNaTabua = new Stack<IngredientType>(); // Pilha que guarda a ordem dos ingredientes que o jogador clicou

    private GameObject porcaoPronta; // Variável para armazenar o objeto da poção que acabou de ser criada

    // Função principal chamada pelos potes quando o jogador clica neles
    public bool AdicionarIngrediente(IngredientType novoIngrediente)
    {
        // Se já existe uma poção pronta na mesa, bloqueia novos cliques até o jogador recolher
        if (porcaoPronta != null) 
        {
            Debug.Log("[VISUAL ERRO] Porção já criada. Limpe a mesa antes de adicionar novos ingredientes.");
            return false; 
        }

        // Se a mesa ainda não tem 3 ingredientes, aceita o novo item
        if (ingredientesNaTabua.Count < 3) 
        {
            ingredientesNaTabua.Push(novoIngrediente); // Empilha o ingrediente na memória

            // Toca o som de adicionar ingrediente na tábua
            AudioManager.instance.TocarSFX(AudioManager.instance.somIngrediente);

            // Liga a imagem visual correspondente na interface (o pote enchendo)
            imagensVasilhas[ingredientesNaTabua.Count - 1].SetActive(true); 
            
            Debug.Log($"[VISUAL] Vasilha {ingredientesNaTabua.Count} encheu com: {novoIngrediente}");

            // Se completou os 3 ingredientes, chegou a hora da verdade: vamos verificar a receita!
            if (ingredientesNaTabua.Count == 3) 
            {
                VerificarReceita(); 
            }

            return true; // Informa ao pote que o ingrediente foi aceito com sucesso
        }
        
        return false; // Retorna falso se a tábua já estiver cheia (com 3 itens)
    }

    // Compara os ingredientes da mesa com o banco de dados de receitas
    private void VerificarReceita() 
    {
        bool receitaEncontrada = false;

        // Varre todas as receitas cadastradas no ScriptableObject
        foreach (MoldeReceita receita in bancoDeReceitas.todasAsReceitas) 
        {
            if (ReceitaBate(receita)) // Se a função retornar true, os ingredientes batem certinho!
            {
                Debug.Log($"[VISUAL SUCESSO] Receita Certa! Criando a poção: {receita.nomePorcao}");
                receitaEncontrada = true;
                
                // Se o prefab e o local de spawn estiverem configurados, instancia a poção pronta na bancada
                if (receita.prefabDestaPocao != null && localDeSpawnDaPocao != null) 
                {
                    porcaoPronta = Instantiate(receita.prefabDestaPocao, localDeSpawnDaPocao.position, Quaternion.identity);
                    
                    // Toca o som de sucesso ao finalizar a poção
                    AudioManager.instance.TocarSFX(AudioManager.instance.somPocaoFeita);
                }
                else
                {
                    Debug.LogWarning("Atenção: O Prefab da poção ou o Local de Spawn não estão configurados no Inspector!");
                }
                
                LimparMesa(); // Limpa os dados da tábua para a próxima mistura
                break;
            }
        }

        // Se correu todas as receitas e nenhuma bateu, o jogador errou a mistura
        if (!receitaEncontrada)
        {
            Debug.Log("[VISUAL ERRO] Mistura errada. Mesa limpa.");
            LimparMesa(); // Limpa a mesa de qualquer forma
            
            // Toca o som de erro de mistura que criamos agora há pouco
            AudioManager.instance.TocarSFX(AudioManager.instance.somErroMistura);
        }
    }

    // Função matemática que confere se os ingredientes da mesa batem com os requisitos da receita
    private bool ReceitaBate(MoldeReceita receita) 
    {
        // Cria uma cópia da lista de requisitos para manipular sem estragar a original
        List<IngredientType> copiaRequisitos = new List<IngredientType>(receita.requisitosDeIngredientes); 
        
        foreach (IngredientType ing in ingredientesNaTabua)
        {
            // Se o ingrediente da mesa existe nos requisitos, remove da cópia
            if (copiaRequisitos.Contains(ing)) 
            {
                copiaRequisitos.Remove(ing); 
            }
            else 
            {
                return false; // Se achar algo intruso, a receita é falsa
            }
        }
        
        return copiaRequisitos.Count == 0; // Se a lista ficou zerada, significa que todos os 3 batem perfeitamente!
    }

    // Função do botão da lixeira para remover o último ingrediente adicionado
    public void RemoverUltimoIngrediente()
    {
        if(porcaoPronta != null) 
        {
            Debug.Log("[VISUAL ERRO] Porção já criada. Limpe a mesa antes de adicionar novos ingredientes.");
            return;
        }

        if (ingredientesNaTabua.Count > 0)
        {
            // Desliga o ícone visual do último pote que tinha acendido
            imagensVasilhas[ingredientesNaTabua.Count - 1].SetActive(false); 
            
            // Remove o ingrediente do topo da pilha
            IngredientType removido = ingredientesNaTabua.Pop(); 
            
            // Toca o som de desfazer/lixeira
            AudioManager.instance.TocarSFX(AudioManager.instance.somDesfazer);
            
            Debug.Log($"[VISUAL] Lixeira: {removido} removido.");
        }
    }

    // Função interna para apagar os visuais e esvaziar a pilha da memória
    private void LimparMesa() 
    {
        foreach (GameObject vasilha in imagensVasilhas)
        {
            if (vasilha != null) vasilha.SetActive(false);
        }
        ingredientesNaTabua.Clear();
    }
}