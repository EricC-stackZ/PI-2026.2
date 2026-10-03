using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InitialCut : MonoBehaviour
{
    public List<GameObject> cutSceneInicial = new List<GameObject>(); // Lista onde estão as imagens da cutscene inicial.
    public List<GameObject> cutScene1 = new List<GameObject>(); // Lista onde estão as imagens da cutscene1.

    private int contador = 0;
    public GameObject avancarCut; public GameObject canvasPrincipal;

    public void AvancarCena0()
    {
        if(contador < cutSceneInicial.Count)
        {
            cutSceneInicial[contador].SetActive(false);

            StartCoroutine(Botao());

            Debug.Log("Desabilitei o índice: " + contador); // Linha de teste.

            contador++;

            if(contador >= 5)
            {
                SceneManager.LoadScene(1);
            }
        }
    }

    public void AvancarCena1()
    {
        if(contador < cutScene1.Count)
        {
            cutScene1[contador].SetActive(false);

            StartCoroutine(Botao());

            Debug.Log("Desabilitei o índice: " + contador); // Linha de teste.

            contador++;

            if(contador >= 6)
            {
                canvasPrincipal.SetActive(true);

                avancarCut.SetActive(false);
            }
        }
    }

    IEnumerator Botao() // Desativa o botão Avançar, espera 5 segundos, e depois ativa novamente.
    {
        avancarCut.SetActive(false);

        yield return new WaitForSeconds(5f);

        avancarCut.SetActive(true);
    }
}
