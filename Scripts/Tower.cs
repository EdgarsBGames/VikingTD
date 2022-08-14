using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerData", menuName = "Tower/TowerData")]
public class Tower : ScriptableObject
{
    public float Damage;
    public float CoolDown;
    public float Range;
    public int BuyPrice;

    [Header("Projectile Data")]
    public GameObject Projectile;
    public float ProjectileSpeed;
    public string TargetTag;

}
