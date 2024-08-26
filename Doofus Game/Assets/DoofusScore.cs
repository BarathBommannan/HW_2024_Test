using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DoofusScore : MonoBehaviour
{
    public ScoreManager scoreManager;
    public float fallThreshold = -10f;
    public Text gameOverText;
    public Button retryButton;

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
            GameOver();
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

    void GameOver()
    {
        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(true);
        }

        if (retryButton != null)
        {
            retryButton.gameObject.SetActive(true);
        }

        Debug.Log("Doofus has fallen below the threshold!");
        Time.timeScale = 0; // Pause the game
    }

    void InitializeGame()
    {
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
        }
        else
        {
            Debug.LogError("Retry Button reference is not assigned.");
        }
    }
}
