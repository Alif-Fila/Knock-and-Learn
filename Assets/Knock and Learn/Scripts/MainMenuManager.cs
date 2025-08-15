using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public GameObject menuCanvas;  // Menu Canvas
    public GameObject gameCanvas;  // Game Canvas

    public void PlayGame()
    {
        menuCanvas.SetActive(false); // Hide menu
        gameCanvas.SetActive(true);  // Show game

        if (GameManager.Instance != null)
            GameManager.Instance.StartGame(); // Start game
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
