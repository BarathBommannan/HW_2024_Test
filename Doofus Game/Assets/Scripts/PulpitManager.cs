using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PulpitManager : MonoBehaviour
{
    public GameObject pulpitPrefab;
    public Transform startPoint;

    public float minPulpitDestroyTime = 4f;
    public float maxPulpitDestroyTime = 5f;
    public float pulpitSpawnTime = 2.5f;
    public float initialDelay = 1f;
    public float minDistance = 9f;

    private Vector3 lastPosition;
    private GameObject currentPulpit;
    private GameObject previousPulpit;
    private bool isSpawning = true;

    void Start()
    {
        lastPosition = Vector3.zero;

        currentPulpit = Instantiate(pulpitPrefab, lastPosition, Quaternion.identity);
        previousPulpit = currentPulpit;

        StartCoroutine(SpawnPulpitWithInitialDelay());
    }

    IEnumerator SpawnPulpitWithInitialDelay()
    {
        yield return new WaitForSeconds(initialDelay);

        StartCoroutine(SpawnPulpit());
    }

    IEnumerator SpawnPulpit()
    {
        while (isSpawning)
        {
            float waitTime = Random.Range(minPulpitDestroyTime, maxPulpitDestroyTime);
            float destroyTime = Mathf.Max(waitTime - pulpitSpawnTime, 0);

            Vector3 spawnPosition = GetRandomDirection();
            currentPulpit = Instantiate(pulpitPrefab, spawnPosition, Quaternion.identity);

            // Get the countdown text component from the current pulpit
            Text countdownText = currentPulpit.GetComponentInChildren<Text>();
            StartCoroutine(UpdateCountdown(countdownText, destroyTime));

            if (previousPulpit != null)
            {
                StartCoroutine(HideAndDestroyPulpit(previousPulpit, destroyTime));
            }

            previousPulpit = currentPulpit;

            lastPosition = spawnPosition;

            yield return new WaitForSeconds(waitTime);
        }
    }

    IEnumerator HideAndDestroyPulpit(GameObject pulpit, float delay)
    {
        yield return new WaitForSeconds(delay);

        MeshRenderer renderer = pulpit.GetComponent<MeshRenderer>();
        if (renderer != null)
        {
            renderer.enabled = false;
        }

        Destroy(pulpit, 0);
    }

    IEnumerator UpdateCountdown(Text countdownText, float countdownTime)
    {
        while (countdownTime > 0)
        {
            if (countdownText != null)
            {
                countdownText.text = "Destroy in: " + countdownTime.ToString("F1") + "s";
            }
            countdownTime -= Time.deltaTime;
            yield return null;
        }

        if (countdownText != null)
        {
            countdownText.text = "";
        }
    }

    Vector3 GetRandomDirection()
    {
        Vector3 direction;
        if (Random.Range(0, 2) == 0)
        {
            direction = new Vector3(lastPosition.x + minDistance, lastPosition.y, lastPosition.z);
        }
        else
        {
            direction = new Vector3(lastPosition.x, lastPosition.y, lastPosition.z + minDistance);
        }
        return direction;
    }
}
