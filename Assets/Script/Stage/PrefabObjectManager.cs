using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PrefabState
{
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;
}

public class PrefabObjectManager : MonoBehaviour
{
    private Dictionary<GameObject, PrefabState> initialStates = new Dictionary<GameObject, PrefabState>();

    public string managedObjectTag = "ManagedPrefab";

    void Start()
    {
        foreach (var obj in GameObject.FindGameObjectsWithTag(managedObjectTag))
        {
            SaveInitialState(obj);
        }
    }

    public void SaveInitialState(GameObject obj)
    {
        if (!initialStates.ContainsKey(obj))
        {
            PrefabState state = new PrefabState
            {
                position = obj.transform.position,
                rotation = obj.transform.rotation,
                scale = obj.transform.localScale
            };
            initialStates[obj] = state;
        }
    }

    public void ResetToInitialState()
    {
        foreach (var entry in initialStates)
        {
            var obj = entry.Key;
            var state = entry.Value;

            if (obj != null)
            {
                obj.transform.position = state.position;
                obj.transform.rotation = state.rotation;
                obj.transform.localScale = state.scale;
                obj.SetActive(true);

                MovingBlock movingBlock = obj.GetComponent<MovingBlock>();
                if (movingBlock != null)
                {
                    movingBlock.moving = false;
                }

                Rigidbody2D rb2D = obj.GetComponent<Rigidbody2D>();
                if (rb2D != null)
                {
                    rb2D.bodyType = RigidbodyType2D.Static;
                }

                Disappearing disappearing = obj.GetComponent<Disappearing>();

                if(disappearing != null)
                {
                    disappearing.isDisappearing = false;
                }

            }
        }
    }
}