using UnityEngine;
using UnityEngine.SceneManagement;

public class InputTester : MonoBehaviour
{
    public void Roll()
    {
        this.gameObject.GetComponent<Renderer>().material.color = Color.green;
    }
}
