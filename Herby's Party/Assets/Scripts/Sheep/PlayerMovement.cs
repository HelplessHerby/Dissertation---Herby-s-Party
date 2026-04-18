using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 6f;

    private Vector2 moveInput;
    private CharacterController cc;

    private void Start()
    {
        cc = GetComponent<CharacterController>();
    }
    public void OnMove(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
    }

    private void Update()
    {
        Vector3 move = new Vector3(moveInput.x, 0f, moveInput.y);

        if(move.magnitude > 1f)
        {
            move.Normalize();
        }
        cc.Move(move * moveSpeed * Time.deltaTime);
    }
}