using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField]
    public int maxHealth;
    public int health;

    [SerializeField]
    public int damage;
    [SerializeField]
    public string weakness;

    [SerializeField]
    public int speed;

    private void Start() {
        this.health = this.maxHealth;
    }
}
