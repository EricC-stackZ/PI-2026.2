using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextPage2 : MonoBehaviour
{
    public List<GameObject> cutScene2 = new List<GameObject>();

    private int contador = 0;
    public GameObject avancarCena;

    public void AvancarCena2()
    {
        contador++;

        switch (contador)
        {
            case 1:
                cutScene2[0].SetActive(false);
                StartCoroutine(Botao());
            break;

            case 2:
                cutScene2[1].SetActive(false);
                StartCoroutine(Botao());
            break;

            case 3:
                cutScene2[2].SetActive(false);
                StartCoroutine(Botao());
            break;

            case 4:
                cutScene2[3].SetActive(false);
                StartCoroutine(Botao());
            break;

            case 5:
                    // Falta, gameplay fase 2 (Aqui)!
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
