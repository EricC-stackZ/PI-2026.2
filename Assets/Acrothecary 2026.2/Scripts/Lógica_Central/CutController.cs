using UnityEngine;
using UnityEngine.SceneManagement;

public class CutController : MonoBehaviour
{
    private int tela = 0;

    [SerializeField]
    private GameObject[] telasIniciais;

    public void AvancarCut1()
    {
        tela++;

        switch (tela)
        {
            case 1:

                telasIniciais[0].SetActive(false);

                break;

            case 2:

                telasIniciais[1].SetActive(false);

                break;

            case 3:

                telasIniciais[2].SetActive(false);

                break;

            case 4:

                SceneManager.LoadScene(1);

                break;
        }
    }

    public void PularCut1()
    {
        SceneManager.LoadScene(1);
    }
}
