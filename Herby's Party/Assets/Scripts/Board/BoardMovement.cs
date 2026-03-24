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
    public bool canMove = true;

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
        if (agent.remainingDistance <= 1)
        {
            dr.canRoll = true;
        }
    }

    public IEnumerator UpdatePlayer(int moveAmount)
    {
        dr.canRoll = false;
        int ind = moveAmount + curTileIndex;
        for (int i = curTileIndex; i < ind; i++)
        {
            if (canMove)
            {
                agent.destination = tileArr[i].transform.position;
                Debug.Log(tileArr[i].name);
                yield return new WaitForSeconds(.5f);
                Debug.Log("i = " + i +" curTile = " +curTileIndex);
                if (tileArr[i].gameObject.GetComponent<TileBehaviour>().shouldBreak && i == curTileIndex-1)
                {
                    Debug.Log("break");
                    yield return new WaitForSeconds(2f);
                    TileBehaviour tB = tileArr[i].gameObject.GetComponent<TileBehaviour>();
                    tB.activate();
                    canMove = false;
                }
                curTileIndex = ind;
            }

        }
    }
}
