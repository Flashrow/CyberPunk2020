using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCEnemy : NPCCharacter {
    public float MaxHealth = 100;
    public Slider HealthbarHandler;
    public Text HealthbarTextHandler;
    private NPCEnemyAttack attackScript = null;
    private NPCEnemyAnimation anim = null;
    public override void OnDrawGizmosSelected () {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere (transform.position, PlayerDetectArea);
    }
    protected override void onAwake () {
        HealthbarTextHandler.text = $"{MaxHealth}";
        attackScript = GetComponent<NPCEnemyAttack> ();
        anim = GetComponentInChildren<NPCEnemyAnimation> ();
    }
    protected override void onStart () {
        currentHealth = MaxHealth;
    }
    public override void OnHit (float val) {
        anim.Hit ();
        currentHealth -= val;
        float valHealth = currentHealth / MaxHealth;
        HealthbarHandler.value = valHealth;
        HealthbarTextHandler.text = $"{currentHealth} / {MaxHealth}";
    }
    protected override void onDie () {
        anim.Die (gameObject);
    }

    protected override void isMovable (float dist) {
        anim.SetSpeed ((int) movementScript.Agent.speed);
        if (currentHealth <= 0) throw new NPCDie ();
        if (dist <= PlayerDetectArea) {
            FaceTarget ();
            movementScript.Agent.SetDestination (player.position);
            if (dist <= movementScript.Agent.stoppingDistance)
                movementScript.Idle ();
            else
                movementScript.Running ();
            if (dist <= attackScript.Area) {
                anim.StartFire ();
                attackScript.ShootToPlayer (dist);
            } else anim.StopFire ();
        } else movementScript.Spot ();
    }
    protected override void isStatic (float dist) {
        if (currentHealth <= 0) throw new NPCDie ();
        if (dist <= PlayerDetectArea) {
            FaceTarget ();
            if (dist <= attackScript.Area)
                attackScript.ShootToPlayer (dist);
        }
    }
}