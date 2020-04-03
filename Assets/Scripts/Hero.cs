using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour {
    public float BaseHp = 1000;
    public float Hp = 1000;
    public int Coins = 16000;
    public string playerName = "Cyber";
    public ushort playerAmmo = 60;
    public ushort inGunAmmo = 16;

    public Inventory inventory;

    private void Awake () {
        inventory = new Inventory ();
    }

    public void setPlayerAmmo (ushort ammo) {
        this.playerAmmo = ammo;
    }

    public void setHealth (ushort hp) {
        this.Hp = hp;
    }

    public void setGunAmmo (ushort ammo) {
        this.playerAmmo = ammo;
    }

    public void HitPlayer (float val) {
        this.Hp -= val;
    }

    // Start is called before the first frame update
    void Start () {

    }

    // Update is called once per frame
    void Update () {

    }
}