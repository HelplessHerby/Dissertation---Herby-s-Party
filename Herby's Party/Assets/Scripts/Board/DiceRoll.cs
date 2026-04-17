using System.Collections;
using TMPro;
using UnityEngine;

public class DiceRoll : MonoBehaviour
{
    public bool canRoll = true;
    public int lowDice = 1;
    public int highDice = 6;
    public int finalNum;
    public TextMeshProUGUI numText;
    private BoardMovement bm;

    private void Start()
    {
        bm = GetComponent<BoardMovement>();
    }
    private void Update()
    {
    }
    public void Roll()
    {
        if (!canRoll) return;
        bm.turnFinished = false;

        StartCoroutine(RollRoutine());

    }
    public int getNum()
    {
        return Random.Range(lowDice, highDice + 1);
    }

    void EventRoll()
    {
        finalNum = getNum();
        StartCoroutine(rollNumber());
        int moveAmount;

        if(finalNum > 4)
        {
            Debug.Log("win");
            moveAmount = finalNum;
        }
        else
        {
            Debug.Log("fail");
            moveAmount = 1;
        }
    }

    IEnumerator RollRoutine()
    {
        if (bm.isMoving) yield break;
        canRoll = false;

        finalNum = getNum();
        yield return StartCoroutine(rollNumber());

        int moveAmount = finalNum;

        if (bm.waitingForEvent)
        {
            EventRoll();
            bm.waitingForEvent = false;
        }

        yield return StartCoroutine(bm.UpdatePlayer(moveAmount));

        canRoll = true;
    }

    IEnumerator rollNumber()
    {
        //Run visually multiple times
        int rollCount = 0;
        Debug.Log("roll num");
        //Visual Numbers
        for (int y = 0; y < 7; y++)
        {
            numText.text = y.ToString();
            yield return new WaitForSeconds(.1f);
            if (rollCount == 3)
            {
                numText.text = finalNum.ToString();
            }
            else
            {
                if (y == 6) { y = 0; }
                rollCount++;
            }
        }
        

    }
}
