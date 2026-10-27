using UnityEngine;
using UnityEngine.InputSystem;

public class CursorController : MonoBehaviour
{
    // Imagens para alterar no cursor são do tipo Texture no Script.
    [SerializeField]
    Texture2D cursorAberto; 
    
    [SerializeField]
    Texture2D cursorFechado;

    private void Start() // Inicia o game com o cursor alterado para mão aberta.
    {
        Cursor.SetCursor(cursorAberto, Vector2.zero, CursorMode.Auto); // Parâmetro 1: Imagem/Textura | Parâmetro 2: Posição do Clique | Parâmetro 3: Tipo de Renderização do Cursor
    }

    private void Update() // Verificação onde, se o botão esquerdo do mouse for pressionado muda a imagem do cursor para mão fechada. E, se não estiver pressionado, deixa o cursor com a imagem de mão aberta.
    {
        if(Mouse.current.leftButton.wasPressedThisFrame == true)
        {
            Cursor.SetCursor(cursorFechado, Vector2.zero, CursorMode.Auto);
        }

        if(Mouse.current.leftButton.wasReleasedThisFrame == true)
        {
            Cursor.SetCursor(cursorAberto, Vector2.zero, CursorMode.Auto);
        }
    }
}
