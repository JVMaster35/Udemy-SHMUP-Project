using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStartData : MonoBehaviour
{
    public AutoShoot autoShootComponent;
    public string weaponStatName;

    // Start is called before the first frame update
    void Start()
    {
        if(weaponStatName.Equals("Blaster"))
        {
           autoShootComponent.SwitchProfile(StatsManager.instance.GetStatsValue(weaponStatName, 
           StatsManager.instance.blasterUpgradeList));
        }
        else if (weaponStatName.Equals("Missile"))
        {
            autoShootComponent.SwitchProfile(StatsManager.instance.GetStatsValue(weaponStatName, 
           StatsManager.instance.missileUpgradeList));
        }

        autoShootComponent.SetIntervalValue();
    }
}
