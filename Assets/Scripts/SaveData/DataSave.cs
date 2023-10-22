using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSave
{

    private float potions;
    private float positionPlayer1X;
    private float positionPlayer1Y;
    private string level;

    public float GetPotions(){
        return this.potions;
    }

    public float GetPositionPlayer1X(){
        return this.positionPlayer1X;
    }

    public float GetPositionPlayer1Y() {
        return this.positionPlayer1Y;
    }   

    public string GetLevel(){
        return this.level;
    } 

    public void SetPotions(float potions){
        this.potions = potions;
    }

    public void SetPositionPlayer1X(float positionPlayer1X){
        this.positionPlayer1X = positionPlayer1X;
    }

    public void SetPositionPlayer1Y(float positionPlayer1Y){   
        this.positionPlayer1Y = positionPlayer1Y;
    }

    public void SetLevel(string level){
        this.level = level;
    }

}
