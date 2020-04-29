using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowManager : MonoBehaviour
{
   [SerializeField] private GameObject arrowFirstPerson;
   [SerializeField] private GameObject arrowThirdPerson;
   [SerializeField] private Transform target = null;

    private void Start()
    {
        if (target != null)
        {
            if(CameraManager.Instance.Brain.IsLive(CameraManager.Instance.FirstPearson)) {
                arrowFirstPerson.GetComponent<TargetArrowScript>().SetTarget(target);
            }
            else { 
                arrowThirdPerson.GetComponent<TargetArrowScript>().SetTarget(target);
            }
        }
    }

    private void Update()
    {
        // Todo: ADD EVENT WHEN TARGET OR CAMERA CHANGE OR QUEST INTERACTION
        if (target != null)
        {
            // TMP
            if (CameraManager.Instance.Brain.IsLive(CameraManager.Instance.FirstPearson))
            {
                arrowThirdPerson.GetComponent<TargetArrowScript>().RemoveTarget();
                arrowFirstPerson.GetComponent<TargetArrowScript>().SetTarget(target);
            }
            else
            {
                arrowFirstPerson.GetComponent<TargetArrowScript>().RemoveTarget();
                arrowThirdPerson.GetComponent<TargetArrowScript>().SetTarget(target);
            }
        }
    }



}
