using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponModel : MonoBehaviour
{
    public Transform rightHand;
    private Transform leftHand;

    public Transform gunRight;
    private Transform gunLeft;
    private GameObject model;
    public WeaponModel(Transform _rightHand)
    {
        rightHand = _rightHand;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setModel(GameObject gunModel)
    {
        model = gunModel;
    }

    private void prepareWeaponModel()
    {
        gunRight = model.transform;
        model.SetActive(true);
    }
    private void stickToHand()
    {
        gunRight.parent = rightHand;
    }
}
