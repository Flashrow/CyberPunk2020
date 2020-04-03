using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCEnemy : NPCCharacter {
    public Slider HealthbarHandler;
    public Text HealthbarTextHandler;
    public NPCEnemyAttack AttackScript;
    public override void OnDrawGizmosSelected () {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere (transform.position, PlayerDetectArea);
    }
    void Awake () {
        HealthbarTextHandler.text = $"{MaxHealth}";
    }

    public override void OnHit (float val) {
        currentHealth -= val;
        float valHealth = currentHealth / MaxHealth;
        HealthbarHandler.value = valHealth;
        HealthbarTextHandler.text = $"{currentHealth} / {MaxHealth}";
    }
    protected override void Die () {
        // TODO: Animation
        Destroy (gameObject);
    }

    protected override void isMovable (float dist) {
        if (dist <= PlayerDetectArea) {
            FaceTarget ();
            agent.SetDestination (player.position);
            if (dist <= AttackScript.Area)
                AttackScript.ShootToPlayer (dist);
        } else movementScript.Spot (agent);
    }
    protected override void isStatic (float dist) {
        if (dist <= PlayerDetectArea) {
            FaceTarget ();
            if (dist <= AttackScript.Area) AttackScript.ShootToPlayer (dist);
        }
    }
}