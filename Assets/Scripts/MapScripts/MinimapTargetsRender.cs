using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// NPCEnemy have test in method onStart() !!!
public class MinimapTargetsRender : MonoBehaviour
{
    static Dictionary<Transform, GameObject> data = new Dictionary<Transform, GameObject>();
    Camera cam;

    static public void AddTarget(Transform target)
    {
        data.Add(target, target.GetComponent<Marker>().Get());
    }

    Vector2 A, T1, B, T2, C, T3, D, T4, camPos, tarPos;

    private void Awake()
    {
        cam = GetComponentInChildren<Camera>();
    }

    void Update()
    {
        foreach (KeyValuePair<Transform, GameObject> entry in data)
        {
            if(entry.Key == null)
            {
                data.Remove(entry.Key);
                break;
            }
            camPos = new Vector2(cam.transform.position.x, cam.transform.position.z);
            tarPos = new Vector2(entry.Key.position.x, entry.Key.position.z);

            T1 = new Vector2(camPos.x, camPos.y + 45);
            T3 = new Vector2(camPos.x, camPos.y - 45);
            T2 = new Vector2(camPos.x + 45, camPos.y);
            T4 = new Vector2(camPos.x - 45, camPos.y);
           
            A = new Vector2(T4.x, T1.y);
            B = new Vector2(T2.x, T1.y);
            C = new Vector2(T2.x, T3.y);
            D = new Vector2(T4.x, T3.y);           
            
            if(tarPos.y > A.y)
            {
               // UP
               if(tarPos.x < A.x)
               {
                    //Debug.Log("1 MINIMAP SQUARE");
                    entry.Value.transform.position = new Vector3(A.x, 75, A.y);
               } else if(tarPos.x > B.x)
               {
                    //Debug.Log("3 MINIMAP SQUARE");
                    entry.Value.transform.position = new Vector3(B.x, 75, B.y);
                } else
                {
                    //Debug.Log("2 MINIMAP SQUARE");
                    entry.Value.transform.position = new Vector3(tarPos.x, 75, A.y);
                }
            } else if(tarPos.y < D.y)
            {
                // DOWN
                if (tarPos.x < A.x)
                {
                    //Debug.Log("7 MINIMAP SQUARE");
                    entry.Value.transform.position = new Vector3(D.x, 75, D.y);
                }
                else if (tarPos.x > B.x)
                {
                    //Debug.Log("9 MINIMAP SQUARE");
                    entry.Value.transform.position = new Vector3(C.x, 75, C.y);
                }
                else
                {
                    //Debug.Log("8 MINIMAP SQUARE");
                    entry.Value.transform.position = new Vector3(tarPos.x, 75, D.y);
                }
            } else
            {
                // MIDDLE
                if (tarPos.x < A.x)
                {
                    //Debug.Log("4 MINIMAP SQUARE");
                    entry.Value.transform.position = new Vector3(A.x, 75, tarPos.y);
                }
                else if (tarPos.x > B.x)
                {
                    //Debug.Log("6 MINIMAP SQUARE");
                    entry.Value.transform.position = new Vector3(B.x, 75, tarPos.y);

                }
                else
                {
                    //Debug.Log("5 MINIMAP SQUARE - MINIMAP VIEW");
                    entry.Value.transform.position = new Vector3(tarPos.x, 75, tarPos.y);

                }
            } 
        }
    }
}
