using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PulpitManager : MonoBehaviour
{
    public GameObject pulpitPrefab;

    public float minPulpitDestroyTime = 4f;
    public float maxPulpitDestroyTime = 5f;
    public float pulpitSpawnTime = 2.5f;

    public float minDistance = 9f;
    
    private GameObject currentPulpit;
    private GameObject nextPulpit;
    
    private bool isGameOver = false;

    void Start()
    {
        float pulpitLifeTime = Random.Range(minPulpitDestroyTime, maxPulpitDestroyTime);

        currentPulpit = Instantiate(pulpitPrefab, Vector3.zero, Quaternion.identity);
        StartCoroutine(UpdateCountdown(currentPulpit, pulpitLifeTime));
        StartCoroutine(HideAndDestroyPulpit(currentPulpit, pulpitLifeTime));

        StartCoroutine(SpawnNextPulpits());
    }

    IEnumerator SpawnNextPulpits()
    {
        while (!isGameOver)
        {
            yield return new WaitForSeconds(pulpitSpawnTime);
                
            float nextPulpitLifetime = Random.Range(minPulpitDestroyTime, maxPulpitDestroyTime);
            Vector3 nextPulpitSpawnPos = GetRandomNextPulpitPos();

            nextPulpit = Instantiate(pulpitPrefab, nextPulpitSpawnPos, Quaternion.identity);
            StartCoroutine(UpdateCountdown(nextPulpit, nextPulpitLifetime));
            StartCoroutine(HideAndDestroyPulpit(nextPulpit, nextPulpitLifetime));
        }
    }

    IEnumerator HideAndDestroyPulpit(GameObject pulpit, float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);

        currentPulpit = nextPulpit;
        
        Destroy(pulpit);
    }

    IEnumerator UpdateCountdown(GameObject pulpit, float lifeTime)
    {
        TextMeshPro countdownText = pulpit.GetComponentInChildren<TextMeshPro>();

        while (lifeTime > 0)
        {
            countdownText.text = lifeTime.ToString("F1") + "s";

            lifeTime -= Time.deltaTime;
            yield return null;
        }

        countdownText.text = "0s";
    }

    Vector3 GetRandomNextPulpitPos()
    {
        Vector3 currentPulpitPos = currentPulpit.transform.position;
        Vector3 nextPulpitPos = Vector3.zero;

        int randomDirectionIndex = Random.Range(0, 4);

        if (randomDirectionIndex == 0)
        {
            nextPulpitPos = new Vector3(currentPulpitPos.x + minDistance, currentPulpitPos.y, currentPulpitPos.z);
        }
        else if (randomDirectionIndex == 1)
        {
            nextPulpitPos = new Vector3(currentPulpitPos.x, currentPulpitPos.y, currentPulpitPos.z + minDistance);
        }
        else if (randomDirectionIndex == 2)
        {
            nextPulpitPos = new Vector3(currentPulpitPos.x - minDistance, currentPulpitPos.y, currentPulpitPos.z);
        }
        else if (randomDirectionIndex == 3)
        {
            nextPulpitPos = new Vector3(currentPulpitPos.x, currentPulpitPos.y, currentPulpitPos.z - minDistance);
        }

        return nextPulpitPos;
    }
}
