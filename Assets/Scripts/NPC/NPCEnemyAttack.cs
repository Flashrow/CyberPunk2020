using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCEnemyAttack : MonoBehaviour {
    public int Luck;
    public float MinDamage;
    public float MaxDamage;
    public float Area = 30f;
    public GameObject impactEffect;
    public ParticleSystem gunFlash;
    void Start () {
        if (Luck < 0 || Luck > 100) Luck = 50;
    }
    public void ShootToPlayer (float distance) {
        //TODO: ANIMATION + AUDIO
        gunFlash.Play ();
        transform.LookAt (PlayerManager.Instance.Player.transform);
        RaycastHit hit;
        Physics.Raycast (transform.position, transform.forward, out hit, Mathf.Infinity);
        if (distance < 10 || UnityEngine.Random.Range (0, 100) <= Luck) {
            PlayerManager.Instance.HeroScript.HitPlayer (Random.Range (MinDamage, MaxDamage));
            Debug.Log ("NPC HIT YOU :(");
        } else Debug.Log ("NPC NOT HIT YOU :D");
        Instantiate (impactEffect, hit.point, Quaternion.LookRotation (hit.normal));
        AudioManager.instance.playSound ("shoot");
    }
}