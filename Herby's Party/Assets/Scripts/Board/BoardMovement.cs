using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class BoardMovement : MonoBehaviour
{
    public GameObject[] tileArr;
    public NavMeshAgent agent;
    public int curTileIndex = 0;
    public IEnumerator UpdatePlayer(int moveAmount)
    {
        int ind = moveAmount + curTileIndex;
        for (int i = curTileIndex; i < ind; i++)
        {
            agent.destination = tileArr[i].transform.position;
            yield return new WaitForSeconds(.5f);
            curTileIndex = ind;
        }
    }
}
