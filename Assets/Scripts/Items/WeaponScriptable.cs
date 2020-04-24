using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Inventory/Weapon")]
public class WeaponScriptable : ItemScriptable
{
    public int number = 1;

    public int inGunAmmo;
    public int magazineAmmo;

    public int fireRate = 5;
    public float range = 100f;
    public float dispersionRadius = 0f;
    public float dispersionVariance = 0f;

    public float damage = 10f;

    public bool isAutomatic = false;
    public bool meele = false;

    public GameObject gunFlash;
    public GameObject impactEffect;
}
