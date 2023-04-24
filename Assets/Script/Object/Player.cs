using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    public int maxHealth;
    public int health;

    [SerializeField]
    public int mana;

    [SerializeField]
    public int armor;

    [SerializeField]
    public int speed;

    private void Start() {
        this.health = this.maxHealth;
    }
}
