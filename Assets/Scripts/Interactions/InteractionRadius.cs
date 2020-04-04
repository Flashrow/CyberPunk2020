using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionRadius : MonoBehaviour
{
    public float radius = 3f;
    public GameObject text;
    public Camera mainCamera;

    public InteractionLabel label;
    InteractionLabel labelTemp;

    public delegate void OnIntegrate(string name);
    public static OnIntegrate onIntegrate;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        GameObject nearest = null;
        int i = 0;
        float minLength = radius;
        while (i < hitColliders.Length)
        {
            if (hitColliders[i].gameObject.CompareTag("InteractObject"))
            {
                if (Vector3.Distance(hitColliders[i].gameObject.transform.position, transform.position) < minLength && isObjectInCamera(hitColliders[i].gameObject.transform.position))
                {
                    nearest = hitColliders[i].gameObject;
                    minLength = Vector3.Distance(hitColliders[i].gameObject.transform.position, transform.position);
                }
            }
            i++;
        }
        if (nearest != null && nearest.TryGetComponent<Interacted>(out Interacted interaction2))
        {
            interaction2.Interact(nearest.transform, nearest.GetComponent<Collider>().bounds.size.y);
        }
        try
        {            
            if(nearest == null)
                onIntegrate("");
            else
                onIntegrate(nearest.name);
        }
        catch { }
    }

    bool isObjectInCamera(Vector3 targetPosition)
    {
        Vector3 screenPoint = mainCamera.WorldToViewportPoint(targetPosition);        
        return (screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1);
    }
}
