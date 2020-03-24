using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionRadius : MonoBehaviour
{
    public float radius = 3f;
    public GameObject text;
    public Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        int i = 0;
        GameObject nearest = null;
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
        if (nearest != null)
        {
            OnKeyPress(nearest);
            text.transform.position = nearest.transform.position + new Vector3(0, 1f, 0);
        }
        else
        {
            text.transform.position = new Vector3(0, -1f, 0);
        }
    }

    bool isObjectInCamera(Vector3 targetPosition)
    {
        Vector3 screenPoint = mainCamera.WorldToViewportPoint(targetPosition);        
        return (screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1);
    }

    void OnKeyPress(GameObject nearest)
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if(nearest.TryGetComponent<ChestsInteractions>(out ChestsInteractions chests))
            {
                chests.Interact();
            }
        }
    }
}
