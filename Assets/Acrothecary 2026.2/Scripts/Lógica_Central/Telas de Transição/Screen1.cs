using UnityEngine;
using UnityEngine.Video;
public class Screen1 : MonoBehaviour
{
    [SerializeField] 
    private VideoPlayer videoPlayer; // Referência ao componente de vídeo.

    [SerializeField] 
    private GameObject telaTransicao; // Objeto da UI que contém o RawImage.

    public ClienteSpawner clienteSpawner; // Referência ao script que inicia o spawn de NPCs do jogo

    private void Start()
    {
        if(videoPlayer != null) // Se o VideoPlayer estiver atribuído, inscreve o método no evento de fim de vídeo
        {
            videoPlayer.loopPointReached += AoTerminarVideo;
        }
    }

    private void AoTerminarVideo(VideoPlayer vp) // Função chamada automaticamente pela Unity quando o vídeo termina.
    {
        if(telaTransicao != null)
        {
            telaTransicao.SetActive(false); // Esconde a tela de transição.
            
            clienteSpawner.AtivarJogo(); // Inicia o spawn de NPCs na cena.
        }
    }
}
