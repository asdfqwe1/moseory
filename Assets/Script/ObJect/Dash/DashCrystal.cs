using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DashCrystal : MonoBehaviour
{
    private Vector2 spawnPosition; // 크리스탈의 초기 위치를 저장하는 변수
    private bool isRespawning = false;

    void Start()
    {
        // 크리스탈의 초기 위치 저장
        spawnPosition = transform.position;
    }

    public void RespawnCrystal()
    {
        if (!isRespawning)
        {
            isRespawning = true;
            StartCoroutine(RespawnDelay(spawnPosition));
        }
    }

    public void RespawnCrystal(Vector2 respawnPosition)
    {
        if (!isRespawning)
        {
            isRespawning = true;
            StartCoroutine(RespawnDelay(respawnPosition));
        }
    }

    IEnumerator RespawnDelay(Vector2 respawnPosition)
    {
        // 리스폰 딜레이 설정
        float respawnDelay = 3f;
        yield return new WaitForSeconds(respawnDelay);

        // 크리스탈 리스폰
        Instantiate(gameObject, respawnPosition, Quaternion.identity);

        // 리스폰이 완료되었으므로 isRespawning 변수를 false로 설정
        isRespawning = false;
    }
}

