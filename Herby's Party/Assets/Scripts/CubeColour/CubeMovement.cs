using UnityEngine;
using UnityEngine.InputSystem;
public class CubeMovement : MonoBehaviour
{
    public float moveSpeed = 6f;

    private Vector2 moveInput;
    public Rigidbody rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void OnMove(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Vector3 move = new Vector3(moveInput.x, 0f, moveInput.y);

        if (move.magnitude > 1f)
        {
            move.Normalize();
        }
        rb.linearVelocity = move * moveSpeed;

    }

}