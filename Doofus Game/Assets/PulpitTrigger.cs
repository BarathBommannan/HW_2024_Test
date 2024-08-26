using UnityEngine;

public class PulpitTrigger : MonoBehaviour
{
    public ScoreManager scoreManager;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (scoreManager != null)
            {
                scoreManager.IncrementScore();
            }
        }
    }
}
