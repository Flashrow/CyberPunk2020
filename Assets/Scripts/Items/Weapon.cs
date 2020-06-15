using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;

[System.Serializable]
public class Weapon : Item {

    [SerializeField] private string spriteName;
    [SerializeField] private string scriptName;

    public Weapon()
    {

    }

    public Weapon(SerializationInfo info, StreamingContext context) : base(info, context)
    {

    }

    public Weapon(string _spriteName, string _scriptName) {
        spriteName = _spriteName;
        scriptName = _scriptName;
        itemId = "gun";
        LoadScriptableObject(scriptName);
    }

    public override void LoadScriptableObject(string scriptName)
    {
        this.data = Resources.Load<WeaponScriptable>("Items/Weapons/" + scriptName);
    }

    public override Item CreateInstance()
    {
        Weapon newWeapon = new Weapon(spriteName,scriptName);
        newWeapon.cost = cost;
        return newWeapon;
    }

    public WeaponScriptable getData()
    {
        return (WeaponScriptable)data;
    }

    public bool needReload()
    {
        if (getData().inGunAmmo == 0) return true;
        else return false;
    }

    public void setMagazineCapacity(int capacity)
    {
        getData().magazineAmmo = capacity;
    }

    public bool singleShoot()
    {
        if (getData().inGunAmmo > 0)
        {
            getData().inGunAmmo--;
            return true;
        }
        return false;
    }

    public int getInGunAmmo()
    {
        return getData().inGunAmmo;
    }

    public int getMagazineCapacity()
    {
        return getData().magazineAmmo;
    }

    public int reload(int ammoToReload)         // returns not loaded ammo
    {
        getData().inGunAmmo += ammoToReload;
        if (getData().inGunAmmo > getData().magazineAmmo)
        {
            getData().inGunAmmo = getData().magazineAmmo;
            return ammoToReload - getData().inGunAmmo;
        }
        else
        {
            return 0;
        }
    }

    public void setModel(Transform transformStickTo)
    {

    }
}
