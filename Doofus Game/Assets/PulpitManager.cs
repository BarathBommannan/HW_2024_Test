using System.Collections;
using UnityEngine;

public class PulpitManager : MonoBehaviour
{
    public GameObject pulpitPrefab;  // The platform prefab to instantiate
    public Transform startPoint;     // The starting point of the platform

    public float minPulpitDestroyTime = 4f;
    public float maxPulpitDestroyTime = 5f;
    public float pulpitSpawnTime = 2.5f;
    public float initialDelay = 1f;  // Delay before the second platform spawns

    private Vector3 lastPosition;
    private GameObject currentPulpit;  // Reference to the current platform
    private GameObject previousPulpit; // Reference to the previous platform

    void Start()
    {
        // Initialize the starting position
        lastPosition = Vector3.zero;  // Start at (0,0,0)
        
        // Instantiate the initial platform at (0,0,0)
        currentPulpit = Instantiate(pulpitPrefab, lastPosition, Quaternion.identity);
        previousPulpit = currentPulpit;

        // Start spawning new platforms with an initial delay
        StartCoroutine(SpawnPulpitWithInitialDelay());
    }

    IEnumerator SpawnPulpitWithInitialDelay()
    {
        // Wait for the initial delay before spawning the second platform
        yield return new WaitForSeconds(initialDelay);

        // Start the spawning process
        StartCoroutine(SpawnPulpit());
    }

    IEnumerator SpawnPulpit()
    {
        while (true)
        {
            // Spawn a new platform
            Vector3 spawnPosition = GetRandomDirection();
            currentPulpit = Instantiate(pulpitPrefab, spawnPosition, Quaternion.identity);

            // Hide and destroy the previous platform after a delay
            if (previousPulpit != null)
            {
                StartCoroutine(HideAndDestroyPulpit(previousPulpit, pulpitSpawnTime));
            }

            // Set the current platform as the previous platform for the next iteration
            previousPulpit = currentPulpit;

            // Calculate the time to wait before spawning the next platform
            float waitTime = Random.Range(minPulpitDestroyTime, maxPulpitDestroyTime) - pulpitSpawnTime;
            waitTime = Mathf.Max(0, waitTime);  // Ensure waitTime is not negative

            yield return new WaitForSeconds(waitTime);

            lastPosition = spawnPosition;
        }
    }

    IEnumerator HideAndDestroyPulpit(GameObject pulpit, float delay)
    {
        yield return new WaitForSeconds(delay);

        MeshRenderer renderer = pulpit.GetComponent<MeshRenderer>();
        if (renderer != null)
        {
            renderer.enabled = false;  // Hide the platform
        }

        Destroy(pulpit, 1f);  // Give a slight delay before destruction to ensure visibility handling
    }

    Vector3 GetRandomDirection()
    {
        Vector3 direction;
        if (Random.Range(0, 2) == 0)
        {
            // Move in x direction
            direction = new Vector3(lastPosition.x + 9, lastPosition.y, lastPosition.z);  // Adjust increment based on platform size
        }
        else
        {
            // Move in z direction
            direction = new Vector3(lastPosition.x, lastPosition.y, lastPosition.z + 9);  // Adjust increment based on platform size
        }
        return direction;
    }
}
