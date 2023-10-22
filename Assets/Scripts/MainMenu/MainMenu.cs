using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public static bool coop;
    private float moneyPref = 0;
    private float potionPref = 0;


    public static void PlaySinglePlayer()
    {
        coop = false;
        SceneManager.LoadScene("Nivel1", LoadSceneMode.Single);
    }

    public static void PlayCoop()
    {
        coop = true;
        SceneManager.LoadScene("Nivel1", LoadSceneMode.Single);
    }

    public static void PlayCredits()
    {
        SceneManager.LoadScene("Credits", LoadSceneMode.Single);
    }

    public static void LoadGame() // en progeso
    {
        coop = false;
        //PlayerData playerData = DataSystem.loadData();
        //SceneManager.LoadScene(playerData.level, LoadSceneMode.Single);
        //PlayerPrefs.SetFloat("moneyPref", playerData.money);
        //PlayerPrefs.SetFloat("potionPref", playerData.potions);
    }

    public static void Quit() 
    {
        Application.Quit();
    }

}
