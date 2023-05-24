using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    //Properties of Player
    [SerializeField]
    private int maxHealth;
    [SerializeField]
    private int health;

    //Use in futur
    [SerializeField]
    private int mana;

    [SerializeField]
    private int armor;

    [SerializeField]
    private int speed;

    private void Start() {
        this.health = this.maxHealth;
    }

    public int GetMaxHealth(){
        return maxHealth;
    }
    public int GetHealth(){
        return health;
    }
    public int GetSpeed(){
        return maxHealth;
    }
    public int GetArmor(){
        return armor;
    }
    public int GetMana(){
        return mana;
    }

    public void ChangeHealth(int newValue){
        health = newValue;
    }

    public void SetMaxHealth(int value){
        maxHealth = value;
    }
    public void SetSpeed(int value){
        speed = value;
    }

    public void SetArmor(int value){
        armor = value;
    }
    public void SetMana(int value){
        mana = value;
    }
}
