using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class RetryManager : MonoBehaviour
{
    public void Retry()
    {
        Time.timeScale = 1; // Resume the game

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
