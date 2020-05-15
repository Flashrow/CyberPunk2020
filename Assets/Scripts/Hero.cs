using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Hero : MonoBehaviour {
    public float BaseHp = 1000;
    public float Hp = 1000;
    public int Coins = 16000;
    public string playerName = "Cyber";
    public ushort playerAmmo = 60;
    public ushort inGunAmmo = 16;
    
    public Inventory inventory;

    static public UnityEvent OnDieEvent = new UnityEvent();

    private void Awake () {
        inventory = new Inventory ();
        OnDieEvent.AddListener(() =>
        {
            UImanager.Alert($"TODO:// Game Over !!!", 2.5f);
            Invoke("goToMenu", 2.5f);
        });
    }

    void goToMenu()
    {
        Debug.Log("GAME OVER !!!");
        //SceneManager.LoadScene("MenuStart");
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
}