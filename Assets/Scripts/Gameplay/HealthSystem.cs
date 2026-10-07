using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public float maxHealth = 10f;
    public GameObject hitEffect, healthBar;
    public bool isEnemy = true;
    public int minScore = 25, maxScore = 50;

    private string tagName = "Bullet";
    private float currentHealth;
    private DeathSystem deathScript;
    private bool dead;

    //Used for Initialization
    void OnEnable()
    {
        if (isEnemy)
        {
            tagName = "Bullet";
        } 
        else
        {
            tagName = "EnemyBullet";
            maxHealth = StatsManager.instance.GetStatsValue("Health", StatsManager.instance.healthUpgradeList);
        }

        currentHealth = maxHealth;
    }

    private void Start()
    {
        if(isEnemy)
        {
            LevelManager.instance.RegisterEnemy();
        }

        deathScript = GetComponent<DeathSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagName))
        {
            if(!isEnemy)
            {
                LevelManager.instance.PlayerHit();
            }

            //Damaged
            float damage = float.Parse(other.name);
            TakeDamage(damage, other);

            PoolingManager.instance.ReturnObject(other.gameObject); //Disables Bullet
        }
    }

    public void TakeDamage(float damage, Collider other)
    {
        /*if(!isEnemy)
        {
            LevelManager.instance.PlayerHit();
        }*/

        Vector3 triggerPosition = other.ClosestPointOnBounds(transform.position);
        Vector3 direction = triggerPosition - transform.position;

        GameObject fx = PoolingManager.instance.UsedObject(hitEffect, triggerPosition, Quaternion.LookRotation(direction));

        PoolingManager.instance.ReturnObject(fx, 1f);

        currentHealth -= damage;
        HealthCheck();
        UpdateUI();
    }
    void HealthCheck()
    {
        if (currentHealth <= 0f)
        {
            if (healthBar != null)
            {
                healthBar.transform.parent.gameObject.SetActive(false);
            }

            //Die
            if(deathScript != null)
            {
                deathScript.Death();
            }

            //If it's an Enemy, add Points
            if (isEnemy && !dead)
            {
                dead = true;
                gameObject.tag = "Untagged";
                LevelManager.instance.AddEnemyKill(Random.Range(minScore, maxScore));
            }
        }
    }

    void UpdateUI()
    {
        if (healthBar != null)
        {
            Vector3 scale = Vector3.one;
            float value = currentHealth / maxHealth;
            scale.x = value;
            healthBar.transform.localScale = scale;
        }
    }

}
