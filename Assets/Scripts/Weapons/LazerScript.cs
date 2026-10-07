using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerScript : MonoBehaviour
{
    public float lazerDuration = 3f, animSpeed = 2f;
    public ParticleSystem burstFX;

    private bool lazerFired = false;
    private WaitForSeconds coroutineLaserDur;

    private Collider col;

    // Start is called before the first frame update
    void Start()
    {
        coroutineLaserDur = new WaitForSeconds(lazerDuration);

        lazerDuration = StatsManager.instance.GetStatsValue("Laser", StatsManager.instance.laserUpgradelist).lazerDuration;

        col = GetComponent<Collider>();
        col.enabled = false;

        //lazerDuration = Stats
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("l") && !lazerFired)
        {
            StartCoroutine(FireLaser());
        } 
    }

    public void ShootLazer()
    {
        if(!lazerFired)
        {
            StartCoroutine(FireLaser());
        }
    }

    IEnumerator FireLaser()
    {
        col.enabled = true;

        lazerFired = true;

        transform.localScale = Vector3.zero;

        burstFX.Play();

        while (transform.localScale.sqrMagnitude < 1f)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, animSpeed * Time.deltaTime);

            yield return null;
        }

        transform.localScale = Vector3.one;

        yield return coroutineLaserDur;

        while (transform.localScale.sqrMagnitude > 0.01f)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, animSpeed * Time.deltaTime);

            yield return null;
        }

        transform.localScale = Vector3.zero;

        burstFX.Stop();

        lazerFired = false;

        col.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            HealthSystem health = other.GetComponent<HealthSystem>();

            if (health) 
            {
                health.TakeDamage(100f, other);
            }
        }
    }
}
