using UnityEngine;
using UnityEngine.InputSystem;

public class GameBehaviour : MonoBehaviour
{
    [Header("Players")]
    public PlayerInput[] inputs;
    public BoardMovement[] players;
    public GameObject[] cams;

    private int curPlay = 0;
    private bool turnActive = false;

    private void Start()
    {
        StartTurn();
    }
    private void Update()
    {
        if (!turnActive) return;

        BoardMovement curPlayer = players[curPlay];

        if (curPlayer.turnFinished && !curPlayer.waitingForEvent)
        {
            EndTurn();
        }
    }

    void StartTurn()
    {
        Debug.Log($"Player {curPlay + 1} Turn has Started");

        turnActive = true;

        for (int i = 0; i < inputs.Length; i++)
        {
            inputs[i].enabled = (i == curPlay);
            cams[i].SetActive(i == curPlay);
        }

        players[curPlay].turnFinished = false;
    }
    void EndTurn()
    {
        Debug.Log($"Player {curPlay + 1} turn End");
        turnActive = false;

        curPlay++;

        if(curPlay >= players.Length)
        {
            curPlay = 0;
        }

        StartTurn();
    }

}
