using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance; //Instância global do script pra chamar em qualquer lugar (Singleton)

    [Header("Fontes de Áudio (Caixas de Som)")]
    public AudioSource musicaSource; //Caixa de som da música de fundo
    public AudioSource efeitosSource; //Caixa de som dos efeitos rápidos (cliques, poções)

    [Header("Trilha Sonora")]
    public AudioClip musicaFase; //A música que vai ficar tocando na fase

    [Header("Sons de Gameplay (Tábua e Poção)")]
    public AudioClip somIngrediente; //Som de clicar e adicionar ingrediente
    public AudioClip somDesfazer;    //Som de tirar ingrediente da tábua (lixeira)
    public AudioClip somPocaoFeita;  //Som quando a poção fica pronta na mesa
    public AudioClip somRecarga;     //Som de reabastecer o estoque do pote

    [Header("Sons do Cliente (NPC)")]
    public AudioClip somAcerto; //Som de quando entregou a poção certa pro cliente
    public AudioClip somErro;   //Som de quando entregou a poção errada (perdeu vida)

    [Header("Sons de Ação (Livro)")]
    public AudioClip somLivro;  //Som do clique para abrir/fechar o livro

    private void Awake() //Inicia antes de tudo
    {
        if (instance == null) instance = this; //Se a vaga estiver livre, ele assume como o gerenciador principal
        else Destroy(gameObject); //Destrói a cópia se já existir um AudioManager na cena pra não dar bug
    }

    private void Start()
    {
        if (musicaFase != null && musicaSource != null)
        {
            musicaSource.clip = musicaFase;
            musicaSource.loop = true;
        }
    }

    public void IniciarMusicaFundo() //Função pra iniciar a música de fundo, chamada no início da fase
    {
        if (musicaSource != null && !musicaSource.isPlaying)
        {
            musicaSource.Play();
        }
    }

    // Função para tocar os efeitos sonoros rápidos que os outros scripts chamam
    public void TocarSFX(AudioClip clipSonoro)
    {
        if (clipSonoro != null && efeitosSource != null) //Verifica se o audio e a caixa de efeitos existem
        {
            efeitosSource.PlayOneShot(clipSonoro); //Toca o áudio uma vez sem cortar outro que já esteja tocando junto
        }
    }

    // Função facilitadora pra colocar direto no On Click do botão do livro no Unity
    public void TocarSomLivro() 
    { 
        TocarSFX(somLivro); //Chama a função de tocar áudio passando a variável do som do livro
    }
}