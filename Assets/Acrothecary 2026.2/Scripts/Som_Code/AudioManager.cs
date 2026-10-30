using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance; // Instância global (Singleton). Permite que qualquer script do jogo chame o áudio sem precisar procurar este objeto.

    [Header("Fontes de Áudio (Caixas de Som)")]
    public AudioSource musicaSource; // A "caixa de som" dedicada apenas para a música de fundo
    public AudioSource efeitosSource; // A "caixa de som" que vai cuspir os efeitos rápidos (cliques, poções, erros)

    [Header("Trilha Sonora")]
    public AudioClip musicaFase; // O arquivo de áudio da música que vai ficar tocando na fase

    [Header("Sons de Gameplay (Tábua e Poção)")]
    public AudioClip somIngrediente; // Som de clicar no pote e o ingrediente cair na mesa
    public AudioClip somDesfazer;    // Som de atirar a poção/ingredientes no lixo
    public AudioClip somPocaoFeita;  // Som de sucesso quando a mistura dos 3 ingredientes dá certo
    public AudioClip somRecarga;     // Som de reabastecer o estoque do pote (os 3 segundos)
    public AudioClip somErroMistura; // Som de quando a receita não existe (mistura errada na mesa)

    [Header("Sons do Cliente (NPC)")]
    public AudioClip somAcerto; // Som de quando entregou a poção certa pro cliente (Sucesso)
    public AudioClip somErro;   // Som de quando entregou a poção errada ou o tempo acabou

    [Header("Sons de Ação (Livro)")]
    public AudioClip somLivro;  // Som de virar a página ou abrir/fechar o livro do Avô

    private void Awake() // Roda antes mesmo do jogo começar oficialmente
    {
        // Sistema de proteção do Singleton: Garante que só exista UM AudioManager na cena inteira
        if (instance == null) 
        {
            instance = this; // Assume a coroa de gerente de áudio
        }
        else 
        {
            Destroy(gameObject); // Se já existir outro gerente (ex: veio de outra cena), ele se mata pra não dar eco/bug
        }
    }

    private void Start()
    {
        // Prepara a fita cassete da música de fundo, mas não dá o play ainda
        if (musicaFase != null && musicaSource != null)
        {
            musicaSource.clip = musicaFase;
            musicaSource.loop = true; // Deixa a música em loop infinito
        }
    }

    // Função que o Spawner chama quando o jogador aperta Espaço para começar a fase
    public void IniciarMusicaFundo() 
    {
        if (musicaSource != null && !musicaSource.isPlaying)
        {
            musicaSource.Play(); // Dá o play na música de fundo
        }
    }

    // A função principal que todos os outros scripts usam para tocar barulhos rápidos
    // --- PARA ERIC: Nos seus scripts de Vidas e Score, basta chamar "AudioManager.instance.TocarSFX(AudioManager.instance.seuSomAqui);" ---
    public void TocarSFX(AudioClip clipSonoro)
    {
        // Só tenta tocar se o som existir e a caixa de som estiver configurada
        if (clipSonoro != null && efeitosSource != null) 
        {
            // O PlayOneShot é mágico: ele toca o som por cima dos outros sem cortar o que já estava tocando
            efeitosSource.PlayOneShot(clipSonoro); 
        }
    }

    // Função atalho (Criei isso pra gente conseguir puxar a função direto no evento "On Click()" do botão do Livro lá no Canvas)
    public void TocarSomLivro() 
    { 
        TocarSFX(somLivro); 
    }
}