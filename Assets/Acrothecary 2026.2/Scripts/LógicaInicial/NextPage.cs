using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class NextPage : MonoBehaviour
{
    public List<GameObject> cutSceneInicial = new List<GameObject>(); // Lista onde estão as imagens da cutscene inicial.
    public List<GameObject> cutScene1 = new List<GameObject>(); // Lista onde estão as imagens da cutscene1.
    public List<GameObject> cutScene2 = new List<GameObject>(); // Lista onde estão as imagens da cutscene2.
    public List<GameObject> cutScene3 = new List<GameObject>(); // Lista onde estão as imagens da cutscene3.
    public List<GameObject> cutScene4 = new List<GameObject>(); // Lista onde estão as imagens da cutscene4.
    
    private int contador = 0;
    public GameObject canvasPrincipal; // Canvas principal onde está o game.

    // Métodos que são chamados ao clicar no botão Avançar em cada cena/fase, desativando o objeto/imagem a partir do índice 0 até que o contador não seja menor que o tamanho da lista.

    public void AvancarCenaInicial()
    {
        if(contador < cutSceneInicial.Count - 1)
        {
            cutSceneInicial[contador].SetActive(false);

            contador++;
        }

        else
        {
            SceneManager.LoadScene(1);
        }
    }

    public void AvancarCena1() 
    {
        if(contador < cutScene1.Count - 1)
        {
            cutScene1[contador].SetActive(false);

            contador++;
        }

        else
        {
            cutScene1[contador].SetActive(false);
            
            canvasPrincipal.SetActive(true);
        }
    }

    public void AvancarCena2()
    {
        if(contador < cutScene2.Count - 1)
        {
            cutScene2[contador].SetActive(false);

            contador++;
        }

        else
        {
            cutScene2[contador].SetActive(false);

            // Falta ativar o canvas principal da fase2 (Aqui)!
        }
    }

    public void AvancarCena3()
    {
        if(contador < cutScene3.Count - 1)
        {
            cutScene3[contador].SetActive(false);

            contador++;
        }

        else
        {
            cutScene3[contador].SetActive(false);
            
            // Falta ativar o canvas principal da fase3 (Aqui)!
        }
    }

    public void AvancarCena4()
    {
        if(contador < cutScene4.Count - 1)
        {
            cutScene4[contador].SetActive(false);

            contador++;
        }

        else
        {
            cutScene4[contador].SetActive(false);

            // Falta ativar o canvas principal da fase4 (Aqui)!
        }
    }
}
