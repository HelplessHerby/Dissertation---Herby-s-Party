using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using System.Collections;

public class ControllerBinder : MonoBehaviour
{
    IEnumerator Start()
    {
        // Wait until GameSession singleton exists
        while (GameSession.instance == null || GameSession.instance.players == null)
            yield return null;

        var players = GameSession.instance.players;
        var gamepads = Gamepad.all;

        // Unpair all devices first to avoid multiple players sharing the same controller
        foreach (var p in players)
        {
            if (p != null && p.user.valid)
                p.user.UnpairDevices();
        }

        // Assign controllers
        for (int i = 0; i < players.Length; i++)
        {
            var player = players[i];

            if (player == null)
            {
                Debug.LogWarning($"Player {i + 1} is null, skipping");
                continue;
            }

            if (i < gamepads.Count)
            {
                var pad = gamepads[i];
                InputUser.PerformPairingWithDevice(pad, player.user);
                Debug.Log($"Player {i + 1} ← {pad.displayName}");
            }
            else
            {
                // No controller → disable player input
                player.DeactivateInput();
                Debug.Log($"Player {i + 1} has no controller (disabled)");
            }
        }
    }
}