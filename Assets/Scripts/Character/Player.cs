using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public ushort health = 100;
    public ushort playerAmmo = 60;
    public ushort inGunAmmo = 16;


    public void setPlayerAmmo(ushort ammo)
    {
        this.playerAmmo = ammo;
    }

    public void setHealth(ushort hp)
    {
        this.health = hp;
    }

    public void setGunAmmo(ushort ammo)
    {
        this.playerAmmo = ammo;
    }

}
