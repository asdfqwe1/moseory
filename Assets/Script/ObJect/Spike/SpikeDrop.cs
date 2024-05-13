using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeDrop : MonoBehaviour
{
    public GameObject spike; // ¶³¾î¶ß¸± °¡½Ã

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody2D spikeRigidbody = spike.GetComponent<Rigidbody2D>();
            if (spikeRigidbody != null)
            {
                spikeRigidbody.bodyType = RigidbodyType2D.Dynamic;
            }

        }
    }
}
