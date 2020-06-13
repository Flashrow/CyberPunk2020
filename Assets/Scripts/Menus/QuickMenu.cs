using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuickMenu : MonoBehaviour {
    public void EndMission () {
        Debug.Log ("EndMission ()");
    }
    public void CloseQuickMenu () {
        UImanager.UIPermentClose ();
    }
    public void QuitGame () {
        SceneManager.LoadScene ("MenuStart");
    }
    public void Save () {
        try
        {
            if(SaveLoadSystem.System.Save("SAVE_LOAD_SYSTEM_TEST_3"))
            {
                UImanager.Alert("Game was save", 4.5f);
            }
        } catch
        {
            UImanager.Alert("Can't save game status!!!", 2.5f);
        }

    }
    public void Load () {
        if (SaveLoadSystem.System.Load("SAVE_LOAD_SYSTEM_TEST_3") == false)
        {
            UImanager.Alert("Can't load game status!!!", 2.5f);
        }
    }
}