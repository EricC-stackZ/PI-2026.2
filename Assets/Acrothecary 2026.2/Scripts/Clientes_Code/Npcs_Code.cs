using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Npcs_Code : MonoBehaviour
{
    [Header("Expressões do Rosto")]
    public Sprite spriteDoente;
    public Sprite spriteFeliz;
    public Sprite spriteRaiva;
    
    [Header("Interface do Pedido")]
    public Image imagemNPC; 
    public Image barraPaciencia;
    public Image iconeReceita;
    
    [Header("Regras de Tempo")]
    public float tempoMaximoEspera = 25f; 

    [Header("Efeito de Alerta (Piscar)")]
    public float tempoParaAlerta = 5f; 
    public Color corNormal = Color.white;
    public Color corAlerta = Color.red;
    public float velocidadePiscar = 5f;

    private bool foiAtendido = false;
    private Sprite receitaEscolhida; 

    void Start()
    {
        MudarExpressao(spriteDoente);
        
        if (barraPaciencia != null)
        {
            barraPaciencia.color = corNormal; 
            StartCoroutine(CronometroDePaciencia());
        }
    }

    public void ConfigurarPedido(Sprite receitaSorteada)
    {
        receitaEscolhida = receitaSorteada;
        if (iconeReceita != null)
        {
            iconeReceita.sprite = receitaEscolhida;
        }
    }

    private void MudarExpressao(Sprite novaExpressao)
    {
        if (imagemNPC != null && novaExpressao != null)
        {
            imagemNPC.sprite = novaExpressao;
        }
    }

    private IEnumerator CronometroDePaciencia()
    {
        float tempoRestante = tempoMaximoEspera;

        while (tempoRestante > 0f && !foiAtendido)
        {
            tempoRestante -= Time.deltaTime; 
            barraPaciencia.fillAmount = tempoRestante / tempoMaximoEspera; 

            if (tempoRestante <= tempoParaAlerta)
            {
                float t = Mathf.PingPong(Time.time * velocidadePiscar, 1f);
                barraPaciencia.color = Color.Lerp(corNormal, corAlerta, t);
            }

            yield return null; 
        }

        if (!foiAtendido)
        {
            MudarExpressao(spriteRaiva);
            
            // --- PARA ERIC ---
            // O tempo do cliente esgotou. Chame a função de perder vida aqui!
            //Serve também para você verificar a derrota, etc
            
            Destroy(gameObject, 2f); 
        }
    }

    public void RecebeuRemedioCerto()
    {
        foiAtendido = true;
        MudarExpressao(spriteFeliz);
        
        // --- PARA ERIC ---
        // O jogador acertou a poção! Chame a função de aumentar os "Clientes Atendidos" aqui!
        //Serve também para você verificar a vitoria, etc

        Destroy(gameObject, 2f); 
    }

    public void RecebeuRemedioErrado()
    {
        foiAtendido = true;
        MudarExpressao(spriteRaiva);
        
        // --- PARA ERIC ---
        // O jogador entregou a poção errada. Chame a função de perder vida aqui!
        //Serve também para você verificar a derrota, etc

        Destroy(gameObject, 2f);
    }
}