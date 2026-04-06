using UnityEngine;
using UnityEngine.InputSystem;

public class GameBehaviour : MonoBehaviour
{
    [Header("Player 1")]
    public PlayerInput p1Input;
    public GameObject p1cam;
    public BoardMovement p1BM;
    [Header("Player 2")]
    public PlayerInput p2Input;
    public GameObject p2cam;
    public BoardMovement p2BM;
    public PlayerInput p3Input;
    public PlayerInput p4Input;

    public int curPlay;

    private void Update()
    {
        switch (curPlay)
        {
            case 0:
                break;
            case 1:
                p1BM.canMove = true;
                p2cam.SetActive(false);
                p1cam.SetActive(true);
                if (p1BM.finished)
                {
                    curPlay = 2;
                }
                break;
            case 2:
                p1cam.SetActive(false);
                p2cam.SetActive(true);
                p2BM.canMove = true;
                break;
            case 3:
                break;
            case 4:
                break;
        }
    }
}
