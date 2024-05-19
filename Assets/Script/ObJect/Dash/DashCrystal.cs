using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DashCrystal : MonoBehaviour
{
    private Vector2 spawnPosition; // ũ����Ż�� �ʱ� ��ġ�� �����ϴ� ����
    private bool isRespawning = false;

    void Start()
    {
        // ũ����Ż�� �ʱ� ��ġ ����
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
        // ������ ������ ����
        float respawnDelay = 3f;
        yield return new WaitForSeconds(respawnDelay);

        // ũ����Ż ������
        Instantiate(gameObject, respawnPosition, Quaternion.identity);

        // �������� �Ϸ�Ǿ����Ƿ� isRespawning ������ false�� ����
        isRespawning = false;
    }
}

