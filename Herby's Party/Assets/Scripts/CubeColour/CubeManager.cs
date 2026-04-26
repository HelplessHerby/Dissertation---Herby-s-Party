using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CubeManager : MonoBehaviour
{
    public List<Cube> cubes = new List<Cube>();
    public List<GameObject> players = new List<GameObject>();
    private HashSet<GameObject> alivePlayers = new HashSet<GameObject>();
    public int playerCount;

    public float roundTime = 5f;

    private Cube.TileColour curColour;

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
        int colourCount = System.Enum.GetValues(typeof(Cube.TileColour)).Length;
        curColour = (Cube.TileColour)Random.Range(0, colourCount);

        Debug.Log($"Safe Colour is: {curColour}");
    }
    void UpdateTiles()
    {
        foreach (var cube in cubes) 
        { 
            bool isSafe = cube.tc == curColour;
            cube.SetActive(isSafe);
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
            }
        }else if(alivePlayers.Count == 0)
        {
            Debug.Log("No winners");
            EndGame();
        }
    }
    void EndGame()
    {
        SceneManager.LoadScene("Board");
    }
}
