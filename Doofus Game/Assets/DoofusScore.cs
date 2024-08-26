using UnityEngine;

public class DoofusScore : MonoBehaviour
{
    public ScoreManager scoreManager;
    public float fallThreshold = -10f;

    private Transform currentPlatform;
    private bool hasScored = false;
    private bool isFirstPlatform = true;

    void Start()
    {
        currentPlatform = GetCurrentPlatform();
        if (currentPlatform != null)
        {
            isFirstPlatform = false;
        }
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
            // code for doofus fall or level restart
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
}
