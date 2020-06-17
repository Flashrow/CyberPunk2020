using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCEnemy : NPCCharacter {
    public float DetectPlayerRadius = 25f;
    public float MaxHealth = 100;
    public string NPCid;
    public Slider HealthbarHandler;
    public Text HealthbarTextHandler;
    private NPCEnemyAttack attackScript = null;
    private NPCAnimation anim = null;
    public override void OnDrawGizmosSelected () {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere (transform.position, DetectPlayerRadius);
    }
    protected override void onAwake () {
        HealthbarTextHandler.text = $"{MaxHealth}";
        attackScript = GetComponent<NPCEnemyAttack> ();
        anim = GetComponentInChildren<NPCAnimation> ();
        tg = Resources.Load<TargetsDataMinimap>("TargetsDataMinimap");
    }
    protected override void onStart () {
        try {
            if (tg.Add(transform) == false)
                Debug.Log("NPC Enemy invisble");
        } catch { }
        currentHealth = MaxHealth;
    }
    public override void OnHit (float val) {
        if(anim)
            anim.AnimHit ();
        currentHealth -= val;
        if (currentHealth > 0) {
            HealthbarHandler.value = currentHealth / MaxHealth;
            HealthbarTextHandler.text = $"{currentHealth} / {MaxHealth}";
        } else {
            currentHealth = 0;
            HealthbarHandler.value = 0;
            HealthbarTextHandler.text = $"{currentHealth} / {MaxHealth}";
        }
    }
    protected override void onDie () {
        EventListener.instance.Kills.Invoke(new KillData { NpcId = this.NPCid, position = transform.position });
        anim.AnimDie (gameObject);
    }

    protected override void isMovable (float dist) {
        if (currentHealth <= 0) throw new NPCDie ();
        anim.AnimSetSpeed ((int) movementScript.Agent.speed);
        if (dist <= DetectPlayerRadius) {
            FaceTarget ();
            movementScript.Agent.SetDestination (player.position);
            if (dist <= movementScript.Agent.stoppingDistance)
                movementScript.Idle ();
            else
                movementScript.Running ();
            if (dist <= attackScript.Area) {
                anim.AnimStartFire ();
                attackScript.ShootToPlayer (dist);
            } else anim.AnimStopFire ();
        } else movementScript.Spot ();
    }
    protected override void isStatic (float dist) {
        if (currentHealth <= 0) throw new NPCDie ();
        if (dist <= DetectPlayerRadius) {
            FaceTarget ();
            if (dist <= attackScript.Area)
                attackScript.ShootToPlayer (dist);
        }
    }
}