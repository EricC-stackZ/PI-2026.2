using System.Collections;
using TMPro;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class InteractionControl : MonoBehaviour
{
    [SerializeField]
    private GameObject[] todasTelas; // Índice 0 = Tutorial 

    public TextMeshProUGUI[] textos; private int contagemTextos;

    public void FecharTutorial()
    {
        todasTelas[0].SetActive(false);
    }

    public void TelaDialogoNPC1()
    {
        contagemTextos++;

        switch (contagemTextos)
        {
            case 1:

                textos[0].text = "Boticário viajante? Eu não tinha decidido nenhum nome ainda… Mas foi uma boa ideia.";

                textos[1].text = "...";

                break;

            case 2:

                textos[0].text = "Mas a dosagem que eu passei não tinha sido suficiente?";

                break;

            case 3:

                textos[1].text = "Ela foi sim, minha dor de cabeça passou por um tempo.";

                textos[0].text = "...";

                break;

            case 4:

                textos[1].text = "O problema é que minha esposa começou a sentir as mesmas coisas que eu.";

                break;

            case 5:

                textos[1].text = "Precisei te procurar pra que ela pudesse melhorar também.";

                break;

            case 6:

                textos[0].text = "Certo, desejo melhoras a vocês!";

                textos[1].text = "...";

                break;

            case 7:

                textos[1].text = "Obrigado, viajante!";

                textos[0].text = "...";

                break;
        }
    }

    private void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            SceneManager.LoadScene(2);
        }
    }
}
