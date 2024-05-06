using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DashCrystal : MonoBehaviour
{
    public GameObject crystalPrefab;
    public List<Transform> spawnPoints; // 스폰할 위치들의 리스트
    private bool isRespawning = false;

    private void Start()
    {
        SpawnCrystals();
    }

    private void SpawnCrystals()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            // 스폰 포인트에서 크리스탈을 스폰합니다.
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

