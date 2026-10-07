using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shoot Profile", menuName = "Shooting Profile", order = 1)]
public class ShootProfiles : ScriptableObject
{
    public float speed, damage, fireRate, interval, destroyRate, spread;
    public int amount;

}
