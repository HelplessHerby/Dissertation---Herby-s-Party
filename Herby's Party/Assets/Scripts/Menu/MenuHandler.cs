using UnityEngine;

public class MenuHandler : MonoBehaviour
{
    public GameObject[] openScene;
    
    public void Begin()
    {
        foreach (GameObject obj in openScene) { 
            obj.SetActive(false);
        }
    }
}
