using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSave : MonoBehaviour
{
    public string level;
    public DataSave data = new DataSave();
    public DataSystem dataSystem = new DataSystem();
    public GameObject player;

    void OnTriggerEnter2D(Collider2D collision)
    {
        data.SetPotions(player.GetComponent<State>().potions);
        Debug.Log(player.GetComponent<State>().potions);
        data.SetPositionPlayer1X(player.GetComponent<State>().positionPlayer1X);
        data.SetPositionPlayer1Y(player.GetComponent<State>().positionPlayer1Y);
        data.SetLevel(level);

        if(collision.tag == "Player"){
            dataSystem.saveData(data.GetPotions(), data.GetPositionPlayer1X(), data.GetPositionPlayer1Y(), data.GetLevel());
            Debug.Log(PlayerPrefs.GetString("data"));
            }

    }

}
