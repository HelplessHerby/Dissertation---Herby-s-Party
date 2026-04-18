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
            yield return new WaitForSeconds(Random.Range(1, 5));

            PickNewDir();
        }
    }
    void Update()
    {
        if(!isMoving) return;

        transform.Translate(direction * speed * Time.deltaTime, Space.World);

        KeepInsideBounds();
    }

    void PickNewDir()
    {
        direction = new Vector3(Random.Range(-1f,1f), 0f, Random.Range(-1f,1f)).normalized;
        transform.forward = direction;
    }

    void KeepInsideBounds()
    {
        Vector3 pos = transform.position;

        //Clamp
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.z = Mathf.Clamp(pos.z, minZ, maxZ);

        transform.position = pos;

        if (pos.x <= minX && pos.x >= maxX) 
        {
            direction.x *= -1;
        }

        if(pos.z <= minZ && pos.z >= maxZ)
        {
            direction.z *= -1;
        }
    }

}
