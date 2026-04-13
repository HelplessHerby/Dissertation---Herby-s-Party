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
        if (canRoll)
        {
        }

    }
    public void Roll()
    {
        if (canRoll)
        {
            Debug.Log("Space");
            //Get Actual Number
            finalNum = getNum();
            //Visualise Dice Roll
            StartCoroutine(rollNumber());
            StartCoroutine(bm.UpdatePlayer(finalNum));
        }

    }
    public int getNum()
    {
        return Random.Range(lowDice, highDice);
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
