using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour {

    public GameObject Player;
    public Hero HeroScript;

    #region Singleton

    public static PlayerManager Instance;

    void Awake () {
        Instance = this;
    }

    #endregion

    static public UnityEvent EnableMovement = new UnityEvent();
    static public UnityEvent DisableMovement = new UnityEvent();

    private void Start()
    {
        EnableMovement.AddListener(() =>
        {
            enable();
        });

        DisableMovement.AddListener(() =>
        {
            disable();
        });
    }

    static private void enable()
    {
        Instance.Player.GetComponent<CharacterController>().enabled = true;
    }

    static private void disable()
    {
        Instance.Player.GetComponent<CharacterController>().enabled = false;
    }

}