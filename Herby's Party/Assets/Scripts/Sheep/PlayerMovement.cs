using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 6f;

    private Vector2 moveInput;
    public Rigidbody rb;
    public bool canMove;

    public SheepMinigame minigame;

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
        if (canMove)
        {
            Vector3 move = new Vector3(moveInput.x , 0f, moveInput.y);

            if (move.magnitude > 1f)
            {
                move.Normalize();
            }
            rb.linearVelocity = move * moveSpeed;


        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FinishLine"))
        {
            StartCoroutine(minigame.PlayerFinished(this));
        }
    }
}