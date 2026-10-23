using UnityEngine;

public class CutController : MonoBehaviour
{
    [SerializeField]
    private Transform canvasSecundario;

    [SerializeField]
    private GameObject[] mudarTela;

    public static class GameData // Classe estática que guarda o valor do int, sem recriar a variável para não perder o seu valor sempre que mudar de cena.
    {
        public static int avancarCut = 1;
    }

    public void AvancarTela()
    {
        GameData.avancarCut++;

        switch (GameData.avancarCut)
        {
            case 2:

                GameObject tela2 = Instantiate(mudarTela[0], canvasSecundario);

                tela2.transform.localPosition = Vector3.zero;

                break;

            case 3:
            
                GameObject tela3  = Instantiate(mudarTela[1], canvasSecundario);

                tela3.transform.localPosition = Vector3.zero;

                break;

            case 4:

                break;

            case 5:

                break;

        }
    }
}
