using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnimation : MonoBehaviour {
    protected Animator anim { get; private set; }
    void Awake () {
        anim = GetComponent<Animator> ();
        onAwake ();
    }
    public void AnimSetSpeed (int speed) {
        if (speed < 0) throw null;
        anim.SetInteger ("speed", speed);
    }
    public void AnimIdle () {
        anim.SetInteger ("speed", 0);
    }
    public void AnimHit () {
        anim.SetTrigger ("hit");
    }
    public void AnimStartFire () {
        anim.SetBool ("fire", true);
    }
    public void AnimStopFire () {
        anim.SetBool ("fire", false);
    }
    public void AnimDie (GameObject go) {
        anim.Play ("Falling Back Death");
        Destroy (go, 3f);
    }
    protected virtual void onAwake () { }
}