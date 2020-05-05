using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class MinimapEvents : MonoBehaviour
{
    static public UnityEvent TurnOff = new UnityEvent();
    static public UnityEvent TurnOn = new UnityEvent();
    static public UnityEvent Switch = new UnityEvent();

    static private bool active = true;

    static private GameObject minimapUI;

    void Awake()
    {
        minimapUI = gameObject;
    }

    private void Start()
    {
        TurnOff.AddListener(TurnOffFun);
        TurnOn.AddListener(TurnOnFun);
        Switch.AddListener(SwitchFun);
    }

    static private void TurnOffFun()
    {
        if(active)
        {
            minimapUI.SetActive(false);
            active = false;
        }
    }

    static private void TurnOnFun()
    {
        if (active == false)
        {
            minimapUI.SetActive(true);
            active = true;
        }
    }

    static public void SwitchFun()
    {
        minimapUI.SetActive(!active);
        active = !active;
    }
}
