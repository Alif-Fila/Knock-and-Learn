using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool gameStarted = false;
    public AudioSource backgroundMusic; // Assign your carnival music here

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        // Start the game paused
        Time.timeScale = 0f;

        if (backgroundMusic != null)
            backgroundMusic.Stop(); // Ensure music doesn't play immediately
    }

    public void StartGame()
    {
        gameStarted = true;
        Time.timeScale = 1f; // Resume game
        if (backgroundMusic != null)
            backgroundMusic.Play(); // Start music
        ScoreManager.Instance.ResetScore(); // Reset score at start
    }
}
