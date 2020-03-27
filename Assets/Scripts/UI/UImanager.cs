using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject inventoryUI;
    public GameObject gameIntervaceUI;

    void Start()
    {
        inventoryUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            inventoryUI.SetActive(true);
            gameIntervaceUI.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseInventory();
        }
    }

    public void CloseInventory()
    {
        inventoryUI.SetActive(false);
        gameIntervaceUI.SetActive(true);
    }
}
