using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Video;

public class Npcs_Code : MonoBehaviour
{
    [Header("Expressões do Rosto")]
    public Sprite spriteDoente; // Carinha normal de quem acabou de chegar na farmácia
    public Sprite spriteFeliz;  // Carinha curada de quando ganha a poção certa
    public Sprite spriteRaiva;  // Carinha de quando o tempo acaba ou ganha poção errada
    
    [Header("Interface do Pedido")]
    public Image imagemNPC; // O espaço na tela onde a foto do rosto do NPC vai aparecer
    public Image barraPaciencia; // A barra (geralmente verde) de tempo caindo
    public Image iconeReceita; // O balãozinho mostrando o desenho da poção que ele quer
    
    [Header("Regras de Tempo")]
    public float tempoMaximoEspera = 25f; // Tempo base (O Spawner sobrescreve isso logo que ele nasce)

    [Header("Efeito de Alerta (Piscar)")]
    public float tempoParaAlerta = 5f; // Quando faltar 5 segundos, a barra entra em modo de desespero
    public Color corNormal = Color.white; // Cor tranquila da barra
    public Color corAlerta = Color.red; // Cor de perigo (pisca pra essa cor)
    public float velocidadePiscar = 5f; // A rapidez do pisca-pisca

    private bool foiAtendido = false; // Trava de segurança para saber se ele já pegou o remédio ou não
    private Sprite receitaEscolhida; // Guarda a imagem do remédio sorteado pra ele

    void Start()
    {
        // O NPC já nasce com a cara de doente
        MudarExpressao(spriteDoente);
        
        // Se a barra de tempo dele existir no prefab, pinta ela com a cor normal e dá o play no relógio
        if (barraPaciencia != null)
        {
            barraPaciencia.color = corNormal; 
            StartCoroutine(CronometroDePaciencia());
        }
    }

    // Função que o Spawner usa para injetar a imagem do remédio sorteado neste NPC
    public void ConfigurarPedido(Sprite receitaSorteada)
    {
        receitaEscolhida = receitaSorteada;
        
        // Atualiza o balãozinho de pensamento dele com a imagem certa
        if (iconeReceita != null)
        {
            iconeReceita.sprite = receitaEscolhida;
        }
    }

    // Função atalho para trocar a imagem do rosto dele rapidinho sem repetir código
    private void MudarExpressao(Sprite novaExpressao)
    {
        if (imagemNPC != null && novaExpressao != null)
        {
            imagemNPC.sprite = novaExpressao;
        }
    }

    // O Spawner manda o tempo exato da fase (ex: 10s na Áustria) e atualiza aqui instantaneamente
    public void DefinirPaciencia(float tempoRecebido)
    {
        tempoMaximoEspera = tempoRecebido; 
    }

    // O relógio invisível que faz a barra ir secando com o tempo
    private IEnumerator CronometroDePaciencia()
    {
        float tempoRestante = tempoMaximoEspera;

        // O relógio roda em loop enquanto tiver tempo na barra E enquanto ele não tiver sido atendido
        while (tempoRestante > 0f && !foiAtendido)
        {
            tempoRestante -= Time.deltaTime; // Vai engolindo o tempo real do jogo
            
            // Seca a barrinha visualmente (numa escala de 1 até 0)
            barraPaciencia.fillAmount = tempoRestante / tempoMaximoEspera; 

            // Se o tempo estiver acabando, ativa o desespero e faz a barra piscar
            if (tempoRestante <= tempoParaAlerta)
            {
                // Matemática do Unity para fazer a cor ir e voltar parecendo uma sirene
                float t = Mathf.PingPong(Time.time * velocidadePiscar, 1f);
                barraPaciencia.color = Color.Lerp(corNormal, corAlerta, t);
            }

            yield return null; // Espera a tela atualizar para rodar o loop de novo
        }

        // Se o loop acima acabou e ele NÃO foi atendido (ou seja, o cronômetro zerou)
        if (!foiAtendido)
        {
            MudarExpressao(spriteRaiva); // Fica pistola
            
            // --- PARA ERIC ---
            // O jogador ignorou o cliente e o tempo esgotou. Faça a verificação de perder vida aqui!
            TransitionController.instance.PerderVida();
            
            Destroy(gameObject, 2f); // O NPC vai embora (destrói) depois de 2 segundos
        }
    }

    // O script da Tábua/Bancada chama essa função se o remédio entregue estiver CERTO
    public void RecebeuRemedioCerto()
    {
        foiAtendido = true; // Trava o cronômetro pra barra parar de descer
        MudarExpressao(spriteFeliz); // O cliente fica curado e sorridente
        
        // --- PARA ERIC ---
        // Acerto! Chame a função de somar a pontuação ou atualizar a meta de clientes da fase aqui.

        TransitionController.instance.GanharClientes();

        AudioManager.instance.TocarSFX(AudioManager.instance.somAcerto); // Toca aquele sonzinho de sucesso

        Destroy(gameObject, 2f); // O NPC vai embora feliz depois de 2 segundos
    }

    // O script da Tábua/Bancada chama essa função se o remédio entregue estiver ERRADO
    public void RecebeuRemedioErrado()
    {
        foiAtendido = true; // Trava o cronômetro pra barra parar de descer
        MudarExpressao(spriteRaiva); // Fica revoltado por ter tomado veneno
        
        // --- PARA ERIC ---
        // Erro fatal! O jogador entregou a poção errada. Faça a verificação de perder vida aqui!
        TransitionController.instance.PerderVida();

        AudioManager.instance.TocarSFX(AudioManager.instance.somErro); // Toca o som de fracasso

        Destroy(gameObject, 2f); // O NPC sai xingando e some depois de 2 segundos
    }
}