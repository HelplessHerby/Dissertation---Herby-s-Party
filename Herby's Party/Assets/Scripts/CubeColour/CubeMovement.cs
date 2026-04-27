using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
public class CubeMovement : MonoBehaviour
{
    public float moveSpeed = 6f;
    public bool canMove;
    public Animator anim;
    private Vector2 moveInput;
    public Rigidbody rb;

    public CubeManager cubeManager;
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
            Vector3 move = new Vector3(moveInput.x, 0f, moveInput.y);

            if (move.magnitude > 1f)
            {
                move.Normalize();
            }
            rb.linearVelocity = move * moveSpeed;
            if (move != Vector3.zero)
            {
                Quaternion targetRot = Quaternion.LookRotation(move);
                rb.MoveRotation(targetRot);
            }
        }
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y - 3f, rb.linearVelocity.z);
        anim.SetBool("isWalking", moveInput != Vector2.zero && canMove);

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Lava"))
        {
            cubeManager.PlayerDeath(this.gameObject);
        }
    }


}