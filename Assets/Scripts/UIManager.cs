using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void RestartLevel()
    {
        Debug.Log("restart level pressed!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void RestartGame()
    {
        Debug.Log("Restart Game pressed!");
        SceneManager.LoadScene("Introduction"); // Replace with the actual name of your first level
    }
}