using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeManager : MonoBehaviour
{
    public List<Cube> cubes = new List<Cube>();

    public float roundTime = 5f;

    private Cube.TileColour curColour;

    private void Start()
    {
        StartCoroutine(GameLoop());
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
}
