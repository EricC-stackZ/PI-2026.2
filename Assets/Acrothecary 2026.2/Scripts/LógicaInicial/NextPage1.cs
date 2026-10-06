using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine.UI;

public class NextPage1 : MonoBehaviour
{
    public List<GameObject> cutSceneInicial = new List<GameObject>(); // Lista onde estão as imagens da cutscene inicial.
    public List<GameObject> cutScene1 = new List<GameObject>(); // Lista onde estão as imagens da cutscene1.
    
    private int contador = 0;
    public GameObject avancarCena; public GameObject canvasPrincipal;

    public void AvancarCenaInicial()
    {
        contador++;

        switch (contador)
        {
            case 1:
                cutSceneInicial[0].SetActive(false);
                StartCoroutine(Botao());
            break;

            case 2:
                cutSceneInicial[1].SetActive(false);
                StartCoroutine(Botao());
            break;

            case 3:
                cutSceneInicial[2].SetActive(false);
                StartCoroutine(Botao());
            break;

            case 4:
                cutSceneInicial[3].SetActive(false);
                StartCoroutine(Botao());
            break;

            case 5:
                SceneManager.LoadScene(1);
            break;
        }
    }

    public void AvancarCena1()
    {
        contador++;

        switch (contador)
        {
            case 1:
                cutScene1[0].SetActive(false);
                StartCoroutine(Botao());
            break;

            case 2:
                cutScene1[1].SetActive(false);
                StartCoroutine(Botao());
            break;

            case 3:
                cutScene1[2].SetActive(false);
                StartCoroutine(Botao());
            break;

            case 4:
                cutScene1[3].SetActive(false);
                StartCoroutine(Botao());
            break;

            case 5:
                cutScene1[4].SetActive(false);
                StartCoroutine(Botao());
            break;

            case 6:
                cutScene1[5].SetActive(false); avancarCena.SetActive(false);
                canvasPrincipal.SetActive(true);
            break;
        }
    }

    IEnumerator Botao() // Desativa o botão Avançar, espera 5 segundos, e depois ativa novamente.
    {
        avancarCena.SetActive(false);

        yield return new WaitForSeconds(5f);

        avancarCena.SetActive(true);
    }
}
