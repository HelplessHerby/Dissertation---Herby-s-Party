using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class BoardMovement : MonoBehaviour
{
    public DiceRoll dr;
    public GameObject[] tileArr;
    public GameObject tilePar;
    public NavMeshAgent agent;
    public int curTileIndex = 0;
    public bool canMove = false;
    public bool finished;
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
        //Playtesting purposes
        dr.canRoll = canMove;
        if(finished) canMove = false;
    }

    public IEnumerator UpdatePlayer(int moveAmount)
    {
        dr.canRoll = false; // disable while moving

        int targetIndex = moveAmount + curTileIndex;

        //clamp
        if (targetIndex >= tileArr.Length) { 
            targetIndex = tileArr.Length - 1;
        }


        for (int i = curTileIndex; i <= targetIndex; i++)
        {

            if (!canMove) yield break;

            GameObject nextTile = tileArr[i];
            agent.destination = nextTile.transform.position;
            Debug.Log($"Moving to {nextTile.name}");

            curTileIndex = i;
            //Special Tiles
            TileBehaviour tb = tileArr[i].GetComponent<TileBehaviour>();
            if (tb.shouldBreak)
            {
                tb.activate();
                canMove = false;
            }

            //check if reached end
            if (curTileIndex == targetIndex) { 
                finished = true;
                canMove = false;
            }
        }
    }
}
