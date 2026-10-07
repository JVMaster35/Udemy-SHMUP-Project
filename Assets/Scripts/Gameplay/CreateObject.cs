using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateObject : MonoBehaviour
{
    public GameObject objectToCreate;
    public int createAmount = 1;

    [Header("Auto-Destory Properties")]
    public bool autoDestory;
    public float timeToDestory;

    private Vector3 position;

    public void Create()
    {
        position = transform.position;
        position.y = 0f;

        for (int i = 0; i < createAmount; i++)
        {
            GameObject temp = PoolingManager.instance.UsedObject(objectToCreate, position, Quaternion.identity);

            if (autoDestory)
            {
                PoolingManager.instance.ReturnObject(gameObject, timeToDestory);
            }
        }
    }
 

}
