using UnityEngine;

public class MegaBombScript : MonoBehaviour
{
    public float radius = 2f;
    public float damage = 5f;
    public ParticleSystem mbFX;

    // Start is called before the first frame update
    void Start()
    {
        var partMain = mbFX.main;
        partMain.startSize = radius * partMain.startSize.constant;

        radius = StatsManager.instance.GetStatsValue("Mega Bomb", StatsManager.instance.megaBombUpgradelist).radius;
        damage = StatsManager.instance.GetStatsValue("Mega Bomb", StatsManager.instance.megaBombUpgradelist).damage;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("b"))
        {
            DeployBomb();
        }
    }

    public void DeployBomb()
    {
        print("Bombs Away!");

        mbFX.Play();

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        for (int i = 0; i < colliders.Length; i++)
        {
            HealthSystem healthSystem = colliders[i].GetComponent<HealthSystem>();

            if(healthSystem != null && colliders[i].CompareTag("Enemy"))
            {
                healthSystem.TakeDamage(damage, colliders[i]);

                print(colliders[i] + " has been hit by the bomb!");
            }
        }

    }
}
