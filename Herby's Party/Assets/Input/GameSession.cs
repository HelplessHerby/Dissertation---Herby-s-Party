using UnityEngine;
using UnityEngine.InputSystem;

public class GameSession : MonoBehaviour
{
    public static GameSession instance;

    public PlayerInput[] players;

    private void Start()
    {
        players[0] = GameObject.FindGameObjectWithTag("Player 1").GetComponent<PlayerInput>();
        players[1] = GameObject.FindGameObjectWithTag("Player 2").GetComponent<PlayerInput>();
    }
    private void Awake()
    {
        if (instance != null) { 
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

    }
    private void OnLevelWasLoaded(int level)
    {
        players[0] = GameObject.FindGameObjectWithTag("Player 1").GetComponent<PlayerInput>();
        players[1] = GameObject.FindGameObjectWithTag("Player 2").GetComponent<PlayerInput>();
    }
}
