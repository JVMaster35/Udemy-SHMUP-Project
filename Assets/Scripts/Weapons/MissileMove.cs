using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MissleMove : BulletMove
{
    public float rotateSpeed = 3f, followDuration = 1f;
    public bool isPlayer;
    private Transform target;

    private WaitForSeconds physicsTimeStep;

    // Start is called before the first frame update
    void Awake()
    {
        rgdby = GetComponent<Rigidbody>();
        physicsTimeStep = new WaitForSeconds(Time.fixedDeltaTime);
    }

    private void Start()
    {
        if (!isPlayer)
        {
            if (GameObject.FindGameObjectWithTag("Player"))
            {
                target = GameObject.FindGameObjectWithTag("Player").transform;
            }
        }

    }

    private void OnEnable()
    {
        StartCoroutine(StartFollow(followDuration));

        //Find Nearest Enemy
        if(isPlayer)
        {
            if (GameObject.FindGameObjectWithTag("Enemy"))
            {
                target = FindEnemy();
            }
        }
    }

    IEnumerator StartFollow(float followDuration)
    {
        while (followDuration > 0)
        {
            followDuration -= Time.fixedDeltaTime;

            if (target != null)
            {
                Vector3 dir = target.transform.position - transform.position;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), rotateSpeed * Time.fixedDeltaTime);
            }

            rgdby.linearVelocity = transform.forward * speed;

            yield return physicsTimeStep;
        }
    }

    Transform FindEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        Array.Sort(enemies, delegate (GameObject a, GameObject b)
        {
            return Vector3.Distance(transform.position, a.transform.position)
            .CompareTo(Vector3.Distance(transform.position, b.transform.position));
        });

        if(Application.isEditor)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                print(Vector3.Distance(transform.position, enemies[i].transform.position));
            }
        }

        return enemies[0].transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Activator") || other.CompareTag("Deactivator"))
        {
            PoolingManager.instance.ReturnObject(this.gameObject);
        }
    }
}
