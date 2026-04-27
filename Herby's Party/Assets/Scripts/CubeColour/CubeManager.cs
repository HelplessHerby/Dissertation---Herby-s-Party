using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CubeManager : MonoBehaviour
{
    public List<Cube> cubes = new List<Cube>();
    public List<GameObject> players = new List<GameObject>();
    private HashSet<GameObject> alivePlayers = new HashSet<GameObject>();
    public int playerCount;
    public TextMeshProUGUI startText;
    public float roundTime = 5f;
    public float countdownTimer = 5f;
    public GameObject countdown;
    private Cube.TileColour curColour;
    public bool finished;
    public MeshRenderer sign;

    public Material[] colours;
    
    
    
    private void Start()
    {
        StartCoroutine(GameLoop());
        foreach(var p in players)
        {
            alivePlayers.Add(p);
        }
    }

    IEnumerator GameLoop()
    {
        foreach (var p in players)
        {
            p.GetComponent<CubeMovement>().canMove = false;
        }
        countdown.SetActive(true);
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
        countdown.SetActive(false);

        foreach (var p in players)
        {
            p.GetComponent<CubeMovement>().canMove = true;
        }
        while (true)
        {
            PickNewColour();

            yield return new WaitForSeconds(roundTime);

            UpdateTiles();

            yield return new WaitForSeconds(3f);
            roundTime -= .3f;
            ResetTiles();

        }
    }

    void PickNewColour()
    {
        float rand = Random.Range(0, 6);
        curColour = (Cube.TileColour)rand;
        sign.material = colours[(int)rand];
        Debug.Log($"Safe Colour is: {curColour}");
    }
    void UpdateTiles()
    {
        if (!finished)
        {
            foreach (var cube in cubes)
            {
                bool isSafe = cube.tc == curColour;
                cube.SetActive(isSafe);
            }
        }

    }
    void ResetTiles()
    {
        foreach (var cube in cubes)
        {
            cube.SetActive(true);
        }
    }

    public void PlayerDeath(GameObject player)
    {
        alivePlayers.Remove(player);
        Debug.Log($"{player.name} has died");
        CheckWin();
    }
    void CheckWin()
    {
        if (alivePlayers.Count == 1)
        {
            foreach (var player in alivePlayers)
            {
                Debug.Log($"{player.name} Wins!!");
                countdown.SetActive(true);
                startText.text = player.name + " wins!!";
                StartCoroutine(EndGame());
            }
        }
        else if(alivePlayers.Count == 0)
        {
            Debug.Log("No winners");
            StartCoroutine(EndGame());
        }
    }

    IEnumerator EndGame()
    {
        foreach (var p in players)
        {
            p.GetComponent<CubeMovement>().canMove = false;
        }
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Board");
    }
}
