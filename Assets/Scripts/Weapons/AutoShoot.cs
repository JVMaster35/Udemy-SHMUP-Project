using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AutoShoot : MonoBehaviour
{
    public ShootProfiles shootProfile;
    public GameObject bulletPrefab;
    public Transform shootPoint;

    private float totalSpread;
    private WaitForSeconds rate, interval; 

    // Start is called before the first frame update
    void OnEnable()
    {
        SetIntervalValue();

        if(shootPoint == null)
        {
            shootPoint = transform;
        }

        StartCoroutine(ShootingSequence());
    }

    public void SetIntervalValue()
    {
        interval = new WaitForSeconds(shootProfile.interval);
        rate = new WaitForSeconds(shootProfile.fireRate);

        totalSpread = shootProfile.spread * shootProfile.amount;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    public void SwitchProfile(ShootProfiles newProfile)
    {
        shootProfile = newProfile;
    }

    IEnumerator ShootingSequence()
    {
        while (true)
        {
            yield return rate;

            float angle = 0f;
            
            for (int i = 0; i < shootProfile.amount; i++)
            {
                angle = totalSpread * (i / (float)shootProfile.amount);
                angle -= (totalSpread / 2f) - (shootProfile.spread / shootProfile.amount);

                Shoot(angle);

                if (shootProfile.fireRate > 0)
                {
                    yield return rate;
                }

            }

            yield return interval;
        }
    }

    void Shoot(float angle)
    {
        Quaternion bulletRotation = Quaternion.Euler(shootPoint.eulerAngles.x, shootPoint.eulerAngles.y, 0f);

        GameObject temp = PoolingManager.instance.UsedObject(bulletPrefab, shootPoint.position, bulletRotation);
        temp.name = shootProfile.damage.ToString();
        temp.transform.Rotate(Vector3.up, angle);
        temp.GetComponent<BulletMove>().speed = shootProfile.speed;
        PoolingManager.instance.ReturnObject(temp, shootProfile.destroyRate);
    }
}
