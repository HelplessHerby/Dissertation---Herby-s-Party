using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    public static GameSession instance;

    public PlayerInput[] players;
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
}
