using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

/*
cameraType:
    0 - FirstPersonCamera
    1 - ThirdPersonCamera
Priority:
    Player View -> 10
    > 10 example Quest Interactions
*/
public class CameraManager : MonoBehaviour {
    public bool FirstPearsonDefault;

    #region Singleton
    public static CameraManager Instance;
    void Awake () {
        Instance = this;
    }
    #endregion

    void Start () {
        if (FirstPearsonDefault) {
            FirstPearson.Priority = 10;
            ThirdPearson.Priority = 0;
        } else {
            FirstPearson.Priority = 0;
            ThirdPearson.Priority = 10;
        }
    }

    void Update () {
        switchCamera ();
    }

    void switchCamera () {
        if (Input.GetKeyDown (KeyCode.V)) {
            if (Brain.IsLive(FirstPearson))
            {
                FirstPearson.Priority = 0;
                ThirdPearson.GetComponent<cameraMovement>().enabled = true;
                ThirdPearson.Priority = 10;
                FirstPearson.GetComponent<cameraMovement>().enabled = false;
            }
            else if (Brain.IsLive(ThirdPearson))
            {
                FirstPearson.GetComponent<cameraMovement>().enabled = true;
                FirstPearson.Priority = 10;
                ThirdPearson.Priority = 0;
                ThirdPearson.GetComponent<cameraMovement>().enabled = false;
            }
        }
    }

    public Camera MainCamera;
    public CinemachineBrain Brain;
    public CinemachineVirtualCamera FirstPearson;
    public CinemachineVirtualCamera ThirdPearson;
}