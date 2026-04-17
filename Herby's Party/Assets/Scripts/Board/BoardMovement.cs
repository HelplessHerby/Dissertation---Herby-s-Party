using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class BoardMovement : MonoBehaviour
{
    private DiceRoll dr;
    public GameObject[] tileArr;
    public GameObject tilePar;
    public NavMeshAgent agent;
    public int curTileIndex = 0;
    public bool isMoving = false;
    public bool waitingForEvent = false;
    public bool turnFinished;
    public void Start()
    {
        tileArr = new GameObject[tilePar.transform.childCount];
        for(int i = 0; i < tileArr.Length; i++)
        {
            tileArr[i] = tilePar.transform.GetChild(i).gameObject;
        }
        dr = GetComponent<DiceRoll>();
    }
    private void Update()
    {
    }

    public IEnumerator UpdatePlayer(int moveAmount)
    {
        turnFinished = false;
        dr.canRoll = false; // disable while moving
        isMoving = true;
        int targetIndex = moveAmount + curTileIndex;

        //clamp
        if (targetIndex >= tileArr.Length) { 
            targetIndex = tileArr.Length - 1;
        }


        for (int i = curTileIndex + 1; i <= targetIndex; i++)
        {
            

            GameObject nextTile = tileArr[i];
            agent.destination = nextTile.transform.position;
            Debug.Log($"Moving to {nextTile.name}");

            while (agent.pathPending || agent.remainingDistance > 0.05f)
            {
                yield return null;
            }

            curTileIndex = i;
            //Special Tiles
            TileBehaviour tb = tileArr[i].GetComponent<TileBehaviour>();
            if (tb.shouldBreak)
            {
                tb.activate();
                waitingForEvent = true;
                isMoving = false;
                dr.canRoll = true;
                yield break;
            }
        }
        isMoving = false;
        turnFinished = true;

    }
}
