using UnityEngine;
using UnityEngine.InputSystem;

// Observação: Adicionar Box Collider2D nos objetos com esse script de arrasto E nos NPCs!
public class DraggableObject : MonoBehaviour
{
    [Header("Identidade da Poção")]
    [Tooltip("Coloque aqui a mesma imagem do frasco que aparece no balão do NPC")]
    public Sprite spriteDestaPocao;

    private bool estaArrastando;
    private Vector3 offset; 
    private Vector3 pos;

    private void Start()
    {
        pos = transform.position;
    }

    private void Update()
    {
        Vector3 posicaoMouse = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        posicaoMouse.z = 0f;

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Collider2D hit = Physics2D.OverlapPoint(posicaoMouse);

            if(hit != null && hit.gameObject == gameObject)
            {
                estaArrastando = true;
                offset = transform.position - posicaoMouse;
            }
        }

        if (estaArrastando)
        {
            transform.position = posicaoMouse + offset;
        }

        // Quando soltamos o clique, acontece a comparação
        if (Mouse.current.leftButton.wasReleasedThisFrame && estaArrastando)
        {
            estaArrastando = false;
            VerificarEntregaNoNPC(posicaoMouse);
        }
    }

    private void VerificarEntregaNoNPC(Vector3 posicaoMouse)
    {
        // Dispara um "raio" invisível para ver todos os objetos que estão debaixo do mouse
        Collider2D[] objetosAtingidos = Physics2D.OverlapPointAll(posicaoMouse);

        foreach (Collider2D hit in objetosAtingidos)
        {
            // Tenta pegar o script do NPC no objeto que o mouse encostou
            Npcs_Code clienteNPC = hit.GetComponent<Npcs_Code>();

            if (clienteNPC != null)
            {
                // Achou um cliente! Compara a imagem desta poção com o ícone do balão dele
                if (this.spriteDestaPocao == clienteNPC.iconeReceita.sprite)
                {
                    clienteNPC.RecebeuRemedioCerto();
                }
                else
                {
                    clienteNPC.RecebeuRemedioErrado();
                }

                // Destrói a poção da mão do jogador após entregar
                Destroy(gameObject);
                return; // Sai da função para não executar o código abaixo
            }
        }

        // Se não soltou em cima de um NPC (ou errou a mira), a poção volta pra mesa
        transform.position = pos;
    }
}