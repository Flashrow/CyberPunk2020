using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuickMenu : MonoBehaviour {

    RectTransform selectedLoadGame = null;

    public void CloseQuickMenu () {
        UImanager.UIPermentClose ();
    }
    public void QuitGame () {
        SceneManager.LoadScene ("MenuStart");
    }
    public void Save (RectTransform saveInput) {
        string fname = saveInput.GetComponent<Text>().text;
        try
        {
            if(SaveLoadSystem.System.Save(fname))
            {
                UImanager.Alert($"Game was save: {fname}", 4.5f);
            }
        } catch (Exception ex)
        {
            UImanager.Alert($"Can't save game: {ex.Message}", 2.5f);
            Debug.LogWarning(ex.Message);
        }
        CloseQuickMenu();
    }

    public void Load () {
        try
        {
            SaveLoadSystem.System.Load("SAVE_LOAD_SYSTEM_TEST_3");
        } catch (Exception ex)
        {
            UImanager.Alert("Can't load game status!!!", 2.5f);
            Debug.LogWarning(ex.Message);
        }
        CloseQuickMenu();
    }
}