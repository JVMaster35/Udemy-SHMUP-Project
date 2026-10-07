using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DeathSystem : MonoBehaviour
{
    public bool destory = true, backToPoll = true;
    public float destoryAfter;
    public CreateObject[] spawnObjects;
    public UnityEvent onDeathEvent;

    private Collider[] colliders;

    private void Start()
    {
        colliders = GetComponents<Collider>();
    }

    public void Death()
    {
        for (int i = 0; i < spawnObjects.Length; i++)
        {
            //Run Spawn Function
            spawnObjects[i].Create();

        }

        onDeathEvent.Invoke();

        if (destory)
        {
            if(backToPoll)
            {
                PoolingManager.instance.ReturnObject(gameObject, destoryAfter);
            }
            else
            {
                Destroy(gameObject, destoryAfter);
            }
            
        }

        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].enabled = false;
        }
    }
}
