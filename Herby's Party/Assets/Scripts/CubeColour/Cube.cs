using System.Collections;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public enum TileColour 
    {
        Red,
        Blue,
        Green,
        Yellow,
        Cyan,
        Pink,
        White
    }
    public TileColour tc;

    public float loweredY = -10f;
    public float raisedY = 0f;
    public float moveSpeed = 4f;

    private Coroutine moveRoutine;

    public void SetActive(bool active)
    {
        if (moveRoutine != null)
        {
            StopCoroutine(moveRoutine);
        }
        float targetY = active ? raisedY : loweredY;
        moveRoutine = StartCoroutine(Move(targetY));
    }

    IEnumerator Move(float targetY)
    {
        Vector3 start = transform.position;
        Vector3 end = new Vector3(start.x,targetY,start.z);

        float t = 0f;

        while(t < 1f)
        {
            t+= Time.deltaTime * moveSpeed;
            transform.position = Vector3.Lerp(start, end, t);
            yield return null;
        }
    }
}
