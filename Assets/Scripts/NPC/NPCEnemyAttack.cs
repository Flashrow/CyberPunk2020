using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCEnemyAttack : MonoBehaviour {
    public int Luck;
    public float MinDamage;
    public float MaxDamage;
    public float Area = 30f;
    public GameObject ImpactEffect;
    public Transform WPX;
    void Start () {
        if (Luck < 0 || Luck > 100) Luck = 50;
    }
    public void ShootToPlayer (float distance) {
        //TODO: ANIMATION + AUDIO
        RaycastHit hit;
        if (Physics.Raycast (WPX.position, WPX.forward, out hit, Mathf.Infinity)) {
            if (hit.transform.name == "Player") {
                if (distance < 5 || UnityEngine.Random.Range (0, 100) <= Luck) {
                    PlayerManager.Instance.HeroScript.HitPlayer (Random.Range (MinDamage, MaxDamage));
                    Debug.Log ("NPC HIT YOU :(");
                } else Debug.Log ("NPC NOT HIT YOU :D");
            } else Debug.Log ("NPC NOT HIT YOU :D");
        }
        Instantiate (ImpactEffect, hit.point, Quaternion.LookRotation (hit.normal));
        AudioManager.instance.playSound ("shoot");
    }
}