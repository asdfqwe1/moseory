using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DashCrystal : MonoBehaviour
{
    public GameObject crystalPrefab;
    public List<Transform> spawnPoints;
    private bool isRespawning = false;

    private void Start()
    {
        SpawnCrystals();
    }

    private void SpawnCrystals()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            // 스폰 포인트에서 크리스탈을 스폰함.
            Instantiate(crystalPrefab, spawnPoint.position, Quaternion.identity);
        }
    }

    public void RespawnCrystal(Vector3 respawnPosition, float respawnDelay)
    {
        if (!isRespawning)
        {
            isRespawning = true;
            StartCoroutine(RespawnDelay(respawnPosition, respawnDelay));
        }
    }

    private IEnumerator RespawnDelay(Vector3 respawnPosition, float respawnDelay)
    {
        yield return new WaitForSeconds(respawnDelay);
        Instantiate(crystalPrefab, respawnPosition, Quaternion.identity);
        isRespawning = false;
    }
}

