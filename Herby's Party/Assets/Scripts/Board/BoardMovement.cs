using System.Collections;
using UnityEngine;

public class BoardMovement : MonoBehaviour
{
    public GameObject[] tileArr;
    public int curTileIndex = 0;
    public IEnumerator UpdatePlayer(int moveAmount)
    {
        int ind = moveAmount + curTileIndex;
        for (int i = curTileIndex; i < ind; i++)
        {
            transform.position = tileArr[i].transform.position;
            yield return new WaitForSeconds(.5f);
            curTileIndex = ind;
        }
    }
}
