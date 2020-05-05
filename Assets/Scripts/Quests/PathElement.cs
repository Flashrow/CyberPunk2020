using UnityEngine;
using System.Collections;

public class PathElement : MonoBehaviour
{
    public int order;

    public void OnTriggerEnter(Collider other)
    {
        gameObject.GetComponentInParent<PathContainer>().PullTrigger(other, order);
    }
}
