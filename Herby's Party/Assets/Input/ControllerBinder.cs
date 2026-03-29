using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class ControllerBinder : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BindControllers();
    }

    void BindControllers()
    {
        var players = GameSession.instance.players;
        var gamepads = Gamepad.all;
        Debug.LogError($"Only have {gamepads.Count} +  controllers connected");
        

        for (int i = 0; i < players.Length; i++)
        {
            var p = players[i];
            var pad = gamepads[i];

            //Clear
            
            p.user.UnpairDevices();

            //Assign
            InputUser.PerformPairingWithDevice(pad, p.user);


            Debug.Log($"Player {i + 1} assigned to {pad.displayName}");
        }
    }
}
