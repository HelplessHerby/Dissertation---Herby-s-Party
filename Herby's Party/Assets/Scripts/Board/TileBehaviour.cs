using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TileBehaviour : MonoBehaviour
{
    public int tileType;
    public bool shouldBreak;

    public List<string> minigames;
     
    public void Start()
    {
        switch (tileType)
        {
            case 0: // blank
                GetComponent<Renderer>().material.color = Color.blue;
                shouldBreak = false;
                break;
            case 1: // Move Forward
                GetComponent<Renderer>().material.color = Color.green;
                shouldBreak = true;
                break;
            case 2: // Move Backwards
                GetComponent<Renderer>().material.color = Color.red;
                shouldBreak = true;
                break;
            case 3: // Minigame
                GetComponent<Renderer>().material.color = Color.yellow;
                shouldBreak = true;
                break;
            case 4: // Event
                GetComponent<Renderer>().material.color = Color.black;
                shouldBreak = true;
                break;
        }
    }

    public void activate(BoardMovement bm)
    {
        Debug.Log("Tile Activated");
        switch (tileType) {
            case 1:
                StartCoroutine(bm.UpdatePlayer(5));
                break;
            case 2:
                StartCoroutine(bm.UpdatePlayer(-5));
                break;
            case 3:
                int lvl = Random.Range(0, minigames.Count);
                GameSession.instance.SaveBoard();
                SceneManager.LoadScene(minigames[lvl]);
                break;
            case 4:
                break;
        }
    }
}
