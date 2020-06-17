using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManag : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            switch (Cursor.visible)
            {
                case false:
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    break;
                case true:
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                    break;
            }
        }
    }
}
