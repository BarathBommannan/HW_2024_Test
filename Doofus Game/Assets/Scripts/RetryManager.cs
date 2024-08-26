using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class RetryManager : MonoBehaviour
{
    public void Retry()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
