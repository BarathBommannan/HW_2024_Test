using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DoofusScore : MonoBehaviour
{
    public ScoreManager scoreManager;
    public float fallThreshold = -10f;
    public Text gameOverText; // Reference to the Game Over Text UI element
    public Button retryButton; // Reference to the Retry Button

    private Transform currentPlatform;
    private bool hasScored = false;
    private bool isFirstPlatform = true;

    void Start()
    {
        InitializeGame();
    }

    void Update()
    {
        Transform newPlatform = GetCurrentPlatform();

        if (newPlatform != currentPlatform)
        {
            if (!hasScored)
            {
                if (!isFirstPlatform && scoreManager != null)
                {
                    scoreManager.IncrementScore();
                }

                currentPlatform = newPlatform;
                hasScored = true;
            }
        }
        else
        {
            hasScored = false;
        }

        if (isFirstPlatform && currentPlatform != null)
        {
            isFirstPlatform = false;
        }

        if (transform.position.y < fallThreshold)
        {
            // Show the Game Over text and Retry button
            if (gameOverText != null)
            {
                gameOverText.gameObject.SetActive(true);
            }

            if (retryButton != null)
            {
                retryButton.gameObject.SetActive(true);
            }

            Debug.Log("Doofus has fallen below the threshold!");
            Time.timeScale = 0; 
        }
    }

    Transform GetCurrentPlatform()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            if (hit.collider.CompareTag("Pulpit"))
            {
                return hit.collider.transform;
            }
        }
        return null;
    }

    void OnRetryButtonClicked()
    {
        // Reload the current scene
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
        Time.timeScale = 1; // Ensure the game resumes
    }

    void InitializeGame()
    {
        // Initialize game elements
        currentPlatform = GetCurrentPlatform();
        if (currentPlatform != null)
        {
            isFirstPlatform = false;
        }
        
        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("Game Over Text reference is not assigned.");
        }

        if (retryButton != null)
        {
            retryButton.gameObject.SetActive(false);
            retryButton.onClick.AddListener(OnRetryButtonClicked);
        }
        else
        {
            Debug.LogError("Retry Button reference is not assigned.");
        }
    }
}
