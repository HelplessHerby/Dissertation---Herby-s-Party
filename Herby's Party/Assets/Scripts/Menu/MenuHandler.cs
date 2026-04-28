using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    public GameObject[] openScene;
    public GameObject[] mainScene;
    public GameObject startButton;
    public GameObject boardButton;

    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(startButton);

    }

    public void Begin()
    {
        foreach (GameObject obj in openScene) { 
            obj.SetActive(false);
        }
        foreach (GameObject obj in mainScene) {
            obj.SetActive(true);
            
        }
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(boardButton);
    }
    public void loadBoard()
    {
        SceneManager.LoadScene("Board");
    }
}
