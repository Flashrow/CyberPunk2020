using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPCAnimation : MonoBehaviour {
    protected Animator anim { get; private set; }
    void Awake () {
        anim = GetComponent<Animator> ();
    }
    public void SetSpeed (int speed) {
        if (speed < 0) throw null;
        anim.SetInteger ("speed", speed);
    }
    public void Idle () {
        anim.SetInteger ("speed", 0);
    }
    public void Hit () {
        anim.SetTrigger ("hit");
    }
    public void Die (GameObject go) {
        anim.SetTrigger ("dead");
        // TOOD: Check if animation end
        Destroy (go);
    }
}