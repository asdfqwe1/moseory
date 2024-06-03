using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeTrigger : MonoBehaviour
{
    public GameObject block1;
    public GameObject block2;

    public FakePlatform block1Movement;
    public FakePlatform block2Movement;

    private Vector3 block1InitialPosition;
    private Vector3 block2InitialPosition;

    void Start()
    {
        block1Movement = block1.GetComponent<FakePlatform>();
        block2Movement = block2.GetComponent<FakePlatform>();

        block1InitialPosition = block1.transform.position;
        block2InitialPosition = block2.transform.position;

        block1Movement.moveRight = false;

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            block1Movement.enabled = true;
            block2Movement.enabled = true;
        }
    }

    public void ResetToInitialState()
    {
        block1.transform.position = block1InitialPosition;
        block2.transform.position = block2InitialPosition;

        block1Movement.enabled = false;
        block2Movement.enabled = false;

        block1Movement.ResetPlatform();
        block2Movement.ResetPlatform();
    }

}
