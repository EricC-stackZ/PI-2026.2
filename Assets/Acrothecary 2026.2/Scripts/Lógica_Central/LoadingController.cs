using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class LoadingController : MonoBehaviour
{
    [SerializeField]
    private TMP_Text textoPorcentagem;

    void Start()
    {
        int proximaCena = TransitionController.GameData.avancarCena;

        StartCoroutine(CarregarCena(proximaCena));
    }

    private IEnumerator CarregarCena(int nomeCena)
    {
        AsyncOperation operacao = SceneManager.LoadSceneAsync(nomeCena);

        // Enquanto o carregamento não terminar
        while (!operacao.isDone)
        {
            // Converte o progresso de (0 a 0.9) para (0 a 1)
            float progresso = Mathf.Clamp01(operacao.progress / 0.9f);

            // Atualiza o texto na tela (ex: "Carregando 45%")
            textoPorcentagem.text = $"Carregando {progresso * 100f:F0}%";

            // Espera até o próximo frame antes de continuar o loop
            yield return null;
        }
    }   
}
