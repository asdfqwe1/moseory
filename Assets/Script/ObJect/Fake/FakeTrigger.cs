using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeTrigger : MonoBehaviour
{
    // �� ���� ����� �����ϱ� ���� ����
    public GameObject block1;
    public GameObject block2;

    // ��ϵ鿡 ���� ������ ��ũ��Ʈ
    private FakePlatform block1Movement;
    private FakePlatform block2Movement;

    void Start()
    {
        // ��ϵ��� ������ ��ũ��Ʈ�� ����
        block1Movement = block1.GetComponent<FakePlatform>();
        block2Movement = block2.GetComponent<FakePlatform>();

        // �ʱ⿡�� ��ϵ��� ������ ��ũ��Ʈ�� ��Ȱ��ȭ
        block1Movement.enabled = false;
        block2Movement.enabled = false;

        // �� ��° ����� ������ ������ �ݴ�� ����
        block1Movement.moveRight = false;

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Ʈ���ſ� �ٸ� Collider�� �������� ��
        if (other.CompareTag("Player"))
        {
            // ��ϵ��� ������ ��ũ��Ʈ�� Ȱ��ȭ�Ͽ� ������ ����
            block1Movement.enabled = true;
            block2Movement.enabled = true;
        }
    }

}
