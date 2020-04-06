using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControl : MonoBehaviour {
    // Start is called before the first frame update
    public Animator anim;
    public GameObject pistol;
    bool aiming = false;
    void Start () {
        anim = GetComponent<Animator> ();
        pistol.SetActive (false);
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown (KeyCode.Alpha1)) {
            aiming = !aiming;
            anim.SetBool ("Aiming", aiming);
            PistolVisibility (aiming);
        }
        var v = Input.GetAxis ("Vertical");
        if (Input.GetKey (KeyCode.Space)) anim.SetTrigger ("Jump");
        if (Input.GetKey (KeyCode.LeftShift)) v = v * 2;
        anim.SetFloat ("Speed", v);
    }

    void PistolVisibility (bool active) {
        //var hand = character.transform.Find("mixamorig:Hips/mixamorig:Spine/mixamorig:Spine1/mixamorig:Spine2/mixamorig:RightShoulder/mixamorig:RightArm/mixamorig:RightForeArm/mixamorig:RightHand");
        pistol.SetActive (active);
    }
}