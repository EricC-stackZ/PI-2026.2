using UnityEngine;
using UnityEngine.InputSystem;

// Observação: Adicionar Box Collider2D nos objetos com esse script de arrasto E nos NPCs!
public class DraggableObject : MonoBehaviour
{
    [Header("Identidade da Poção")]
    [Tooltip("Coloque aqui a mesma imagem do frasco que aparece no balão do NPC")]
    public Sprite spriteDestaPocao;

    private bool estaArrastando; // Controla o estado: o jogador está segurando esta poção agora?
    private Vector3 offset; // Guarda a distância entre o centro do objeto e onde o mouse clicou.
    private Vector3 pos; // Posição inicial (original) da poção na bancada.

    private void Start()
    {
        pos = transform.position; // Salva a posição inicial na bancada ao carregar a cena
    }

    private void Update()
    {
        // 1. Converte a posição do mouse de pixels da tela para coordenadas do mundo 2D.
        Vector3 posicaoMouse = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        posicaoMouse.z = 0f; // Mantém o Z zerado para não deslocar a poção na profundidade 3D.

        if (Mouse.current.leftButton.wasPressedThisFrame) // 2. Quando o jogador clica com o botão esquerdo.
        {
            Collider2D hit = Physics2D.OverlapPoint(posicaoMouse); // Checa se o ponto onde o mouse clicou encosta em algum Collider2D.

            if(hit != null && hit.gameObject == gameObject) // Verifica se o objeto clicado foi ESTE exato objeto.
            {
                estaArrastando = true;
                offset = transform.position - posicaoMouse; // Calcula o offset para a poção não "pular" para o centro do cursor.
            }
        }

        if (estaArrastando) // 3. Enquanto estiver arrastando, atualiza a posição da poção junto com o mouse.
        {
            transform.position = posicaoMouse + offset;
        }
        
        if (Mouse.current.leftButton.wasReleasedThisFrame && estaArrastando) // 4. Quando o jogador solta o botão do mouse.
        {
            estaArrastando = false;
            VerificarEntregaNoNPC(posicaoMouse); // Executa a verificação para checar se a poção foi entregue em um NPC.
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