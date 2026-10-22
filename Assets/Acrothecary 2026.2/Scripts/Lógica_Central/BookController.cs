using UnityEngine;

public class BookController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] livro; // índice 0 = Botão para abrir o Livro || índice 1 = LivroPG1 || Índice 2 = LivroPG2

    public void AbrirLivro()
    {
        livro[0].SetActive(false); livro[1].SetActive(true);
    }

    public void AvancarPG1()
    {
        livro[1].SetActive(false); livro[2].SetActive(true);
    }

    public void VoltarPG2()
    {
        livro[2].SetActive(false); livro[1].SetActive(true);
    }

    public void FecharLivro()
    {
        livro[0].SetActive(true); livro[1].SetActive(false);
    }
}
