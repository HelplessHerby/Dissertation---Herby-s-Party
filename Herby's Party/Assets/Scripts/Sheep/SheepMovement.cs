using System.Collections;
using UnityEngine;

public class SheepMovement : MonoBehaviour
{
    public float speed = 5f;
    public Vector3 direction;
    bool isMoving;
    public float minX = -8f;
    public float maxX = 8f;
    public float minZ = 0f;
    public float maxZ = 30f;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        PickNewDir();
        StartCoroutine(moveRoutine());
    }
    
    IEnumerator moveRoutine()
    {
        while (true)
        {
            isMoving = true;
            yield return new WaitForSeconds(Random.Range(1, 2));

            isMoving = false;
            rb.linearVelocity = Vector3.zero;
            yield return new WaitForSeconds(Random.Range(1, 5));

            PickNewDir();
        }
    }
    void FixedUpdate()
    {
        if(!isMoving) return;

        rb.linearVelocity = direction * speed;

        KeepInsideBounds();
    }

    void PickNewDir()
    {
        direction = new Vector3(Random.Range(-1f,1f), 0f, Random.Range(-1f,1f)).normalized;
        transform.forward = direction;
    }

    void KeepInsideBounds()
    {
        Vector3 pos = rb.position;
        float buffer = 0.1f;
        if (pos.x <= minX)
        {
            direction.x = Mathf.Abs(direction.x);
            pos.x = minX + buffer;
            rb.position = pos;
        }
        else if (pos.x >= maxX) {
            direction.x = -Mathf.Abs(direction.x);
            pos.x = maxX - buffer;
            rb.position = pos;
        }

        if (pos.z <= minZ)
        {
            direction.z = Mathf.Abs(direction.z);
            pos.z = minZ + buffer;
            rb.position = pos;
        }
        else if (pos.z >= maxZ) { 
            direction.z = -Mathf.Abs(direction.z);
            pos.z = maxZ - buffer;
            rb.position = pos;
        }
    }

}
