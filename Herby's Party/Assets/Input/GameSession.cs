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
    public int ID;
    public int tileIndex;
    public Vector3 pos;

}


public class GameSession : MonoBehaviour
{
    public static GameSession instance;

    public PlayerInput[] players;

    private Dictionary<int,int> playerToDevice = new Dictionary<int,int>();
    private bool playersInit = false;

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

        if (!playersInit)
        {
            initControls();
            playersInit = true;
        }
        else
        {
            RebindControls();
        }

        if (scene.name == "Board")
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
        }
        else Debug.LogWarning("P1 not found");

        if (p2 != null)
        {
            players[1] = p2.GetComponent<PlayerInput>();
        }
        else Debug.LogWarning("P2 not found");
    }

    void initControls()
    {
        var gamepads = Gamepad.all;

        for (int i = 0; i < players.Length; i++) {
            var player = players[i];
            if (player == null) continue;

            if (i < gamepads.Count)
            {
                var pad = gamepads[i];

                player.user.UnpairDevices();
                InputUser.PerformPairingWithDevice(pad, player.user);

                playerToDevice[i] = pad.deviceId;
            }
        }
    }


    void RebindControls()
    {
        foreach (var pTD in playerToDevice)
        {
            int playerIndex = pTD.Key;
            int deviceId = pTD.Value;

            var player = players[playerIndex];
            if (player == null) continue;

            var device = InputSystem.GetDeviceById(deviceId);

            if (device != null)
            {
                player.user.UnpairDevices();
                InputUser.PerformPairingWithDevice(device, player.user);
            }
        }
    }
    public void SaveBoard()
    {
        savedPlayers.Clear();

        for (int i = 0; i < players.Length; i++) 
        { 
            var playerInput = players[i];
            if(playerInput == null) continue;

            var board = playerInput.GetComponent<BoardMovement>();
            if (board == null) continue;

            PlayerSaveData data = new PlayerSaveData
            {
                ID = i,
                tileIndex = board.curTileIndex,
                pos = playerInput.gameObject.transform.position,
            };

            savedPlayers.Add(data);
        }

        Debug.Log("Board Data Saved");
    }

    public void LoadBoard()
    {
        if(savedPlayers.Count == 0) return;
        
        foreach(var data in savedPlayers)
        {
            if(data.ID >= players.Length) continue;

            var playerInput = players[data.ID];
            if (playerInput == null) continue;

            var board = playerInput.GetComponent<BoardMovement>();
            if (board == null) continue;

            board.curTileIndex = data.tileIndex;

            Vector3 targetPos = board.tileArr[data.tileIndex].transform.position;

            board.agent.Warp(targetPos);
        }


        Debug.Log("Board Loaded");
    }

    public IEnumerator DelayLoad()
    {
        yield return null;
        LoadBoard();
    }

}
