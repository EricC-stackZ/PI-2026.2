using UnityEngine;
using UnityEngine.Video;
public class Screen1 : MonoBehaviour
{
    [SerializeField] 
    private VideoPlayer videoPlayer;

    [SerializeField] 
    private GameObject telaTransicao;

    private void Start()
    {
        if(videoPlayer != null)
        {
            videoPlayer.loopPointReached += AoTerminarVideo;
        }
    }

    private void AoTerminarVideo(VideoPlayer vp)
    {
        if(telaTransicao != null)
        {
            telaTransicao.SetActive(false);
        }
    }
}
