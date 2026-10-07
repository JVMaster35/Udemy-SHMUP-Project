using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : MonoBehaviour
{
    public float shieldDuration;
    public GameObject hitEffect;

    private WaitForSeconds shieldDelay;
    private Collider col;

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = Vector3.zero;
        shieldDelay = new WaitForSeconds(shieldDuration);

        shieldDuration = StatsManager.instance.GetStatsValue("Shield", StatsManager.instance.shieldUpgradelist).shieldDuration;

        col = GetComponent<Collider>();
        col.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("s"))
        {
            ShieldUp();
        }
    }

    public void ShieldUp()
    {
        if(!col.enabled)
        {
           StartCoroutine(EngageShield());
        }
    }

    IEnumerator EngageShield()
    {
        col.enabled = true;

        float inAnimDuration = 0.5f;
        float outAnimDuration = 0.5f;

        while (inAnimDuration > 0f)
        {
            inAnimDuration -= Time.deltaTime;
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, 0.1f);

            yield return null;
        }

        yield return shieldDelay;

        while (outAnimDuration > 0f)
        {
            outAnimDuration -= Time.deltaTime;
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, 0.1f);

            yield return null;
        }

        transform.localScale = Vector3.zero;
        col.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy") || other.CompareTag("EnemyBullet"))
        {
            Vector3 triggerPosition = other.ClosestPointOnBounds(transform.position);
            Vector3 direction = triggerPosition - transform.position;

            GameObject fx = PoolingManager.instance.UsedObject(hitEffect, triggerPosition, Quaternion.LookRotation(direction));

            PoolingManager.instance.ReturnObject(fx, 1f);

            HealthSystem enemyHealth = GetComponent<HealthSystem>();

            if (enemyHealth) 
            {
                enemyHealth.TakeDamage(1000, other);
            }
            else
            {
                PoolingManager.instance.ReturnObject(other.gameObject);
            }
        }
    }
}
