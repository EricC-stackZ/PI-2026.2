using UnityEngine;
using UnityEngine.InputSystem;

// Observação: Adicionar Box Collider2D nos objetos com esse script de arrasto.
public class DraggableObject : MonoBehaviour
{
    private bool estaArrastando;

    private Vector3 offset;

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

        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            estaArrastando = false;
        }
    }
}
