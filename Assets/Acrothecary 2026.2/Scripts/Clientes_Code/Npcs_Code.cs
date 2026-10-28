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

    private bool foiAtendido = false;
    private Sprite receitaEscolhida; 

    void Start()
    {
        MudarExpressao(spriteDoente);
        
        if (barraPaciencia != null)
        {
            StartCoroutine(CronometroDePaciencia());
        }
    }

    // O Spawner chama esta função assim que o cliente senta!
    public void ConfigurarPedido(Sprite receitaSorteada)
    {
        receitaEscolhida = receitaSorteada;
        if (iconeReceita != null)
        {
            iconeReceita.sprite = receitaEscolhida;
        }
        Debug.Log($"[NPC] Cliente sentou e pediu: {receitaEscolhida.name}");
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
            yield return null; 
        }

        if (!foiAtendido)
        {
            MudarExpressao(spriteRaiva);
            Destroy(gameObject, 2f); 
        }
    }

    public void RecebeuRemedioCerto()
    {
        foiAtendido = true;
        MudarExpressao(spriteFeliz);
        Destroy(gameObject, 2f); 
    }

    public void RecebeuRemedioErrado()
    {
        foiAtendido = true;
        MudarExpressao(spriteRaiva);
        Destroy(gameObject, 2f);
    }
}