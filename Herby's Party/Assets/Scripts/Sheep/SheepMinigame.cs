using System.Collections;
using TMPro;
using UnityEngine;

public class SheepMinigame : MonoBehaviour
{
    public PlayerMovement[] players;

    public TextMeshProUGUI startText;

    public float countdownTimer = 5f;
    public bool finished;

    private void Start()
    {
        StartCoroutine(GameLoop());
    }
    IEnumerator GameLoop()
    {
        foreach(var p in players)
        {
            p.canMove = false;
        }

        float timer = countdownTimer;

        while (timer > 0) 
        {
            startText.color = Color.red;
            startText.text = "Starting in..." + timer;
            yield return new WaitForSeconds(1f);
            timer--;
        }

        startText.text = "START";
        startText.color = Color.green;
        yield return new WaitForSeconds(1f);
        startText.text = "";

        foreach (var p in players)
        {
            p.canMove = true;
        }
    }
    public IEnumerator PlayerFinished(PlayerMovement player)
    {
        if (!finished)
        {
            switch (player.tag)
            {
                case "Player 1":
                    startText.text = "Player 1 has Won!";
                    finished = true;
                    break;
                case "Player 2":
                    startText.text = "Player 2 has Won!";
                    finished = true;
                    break;
            }
            foreach (var p in players)
            {
                p.canMove = false;
                yield return new WaitForSeconds(.5f);
                p.rb.linearVelocity = Vector3.zero;
            }
        }
    }
}
