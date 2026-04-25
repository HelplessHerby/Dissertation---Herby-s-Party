using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerSaveData
{
    public int tileIndex;

}


public class GameSession : MonoBehaviour
{
    public static GameSession instance;

    public PlayerInput[] players;

    public List<PlayerSaveData> savedPlayers = new List<PlayerSaveData>();

    private void Awake()
    {
        if (instance != null) { 
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += onSceneLoaded;

    }
    private void onSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        InitPlayers();
        RebindControls();

        if(scene.name == "Board")
        {
            StartCoroutine(DelayLoad());
        }
    }
    void InitPlayers()
    {
        GameObject p1 = GameObject.FindGameObjectWithTag("Player 1");
        GameObject p2 = GameObject.FindGameObjectWithTag("Player 2");

        players = new PlayerInput[2];

        if (p1 != null)
        {
            players[0] = p1.GetComponent<PlayerInput>();
            players[0].DeactivateInput();
        }
        else Debug.LogWarning("P1 not found");

        if (p2 != null)
        {
            players[1] = p2.GetComponent<PlayerInput>();
            players[1].DeactivateInput(); 
        }
        else Debug.LogWarning("P2 not found");
    }

    void RebindControls()
    {
        var gamepads = Gamepad.all;

        foreach (var p in players)
        {
            if (p == null) continue;

            p.DeactivateInput();

            if (p.user.valid)
                p.user.UnpairDevices();
        }

        for (int i = 0; i < players.Length; i++)
        {
            var player = players[i];
            if (player == null) continue;

            if (i < gamepads.Count)
            {
                var pad = gamepads[i];

                player.SwitchCurrentControlScheme(pad);
                Debug.Log($"Player {i + 1} : {pad.displayName}");
            }
            else
            {
                player.DeactivateInput();
            }
        }

        foreach (var p in players)
        {
            if (p != null)
                p.ActivateInput();
        }
    }

    public void SaveBoard()
    {
        BoardMovement[] boardPlayers = FindObjectsOfType<BoardMovement>();

        System.Array.Sort(boardPlayers, (a, b) =>
        a.gameObject.name.CompareTo(b.gameObject.tag));
        savedPlayers.Clear();

        for (int i = 0; i < boardPlayers.Length; i++)
        {
            PlayerSaveData data = new PlayerSaveData();

            data.tileIndex = boardPlayers[i].curTileIndex;

            savedPlayers.Add(data);
        }

        Debug.Log("Board Data Saved");
    }

    public void LoadBoard()
    {
        if(savedPlayers.Count == 0) return;
        BoardMovement[] boardPlayers = FindObjectsOfType<BoardMovement>();

        System.Array.Sort(boardPlayers, (a, b) =>
        a.gameObject.name.CompareTo(b.gameObject.tag));
        //savedPlayers.Clear();

        for (int i = 0;i < savedPlayers.Count; i++)
        {
            var data = savedPlayers[i]; 
            var player = boardPlayers[i];

            player.curTileIndex = data.tileIndex;

            player.transform.position = player.tileArr[data.tileIndex].transform.position;
        }

        Debug.Log("Board Loaded");
    }

    public IEnumerator DelayLoad()
    {
        yield return null;
        LoadBoard();
    }

}
