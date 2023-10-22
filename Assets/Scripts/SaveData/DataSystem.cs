using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataSystem
{

    public DataSave data;

    public void saveData(float potions, float positionPlayer1X, float positionPlayer1Y, string level) // write data
    {
        PlayerPrefs.SetString("data",JsonUtility.ToJson(potions+positionPlayer1X+positionPlayer1Y+level)); 
    }


    public void loadData(MonoBehaviour state) // read data
    {
        JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString("data"),data);
    }
}
