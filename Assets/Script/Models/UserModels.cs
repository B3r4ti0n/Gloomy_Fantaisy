using System;
using UnityEngine;

[Serializable]
public class UserLogged {
    public string _id;
    public string name;
    public string email;
    public string ID_Stats;
    public Stats stats;
}

[Serializable]
public class Stats {
    public string _id;
    public int level;
    public string race;
    public int exp;
    public int gold;
    public int premium_gold;
    public int health_Point;
    public int offensive_value;
    public int defensive_value;
    public int intelligence_value;
    public int speed_value;
    public int mana_value;
}

public class NewBehaviourScript : MonoBehaviour {
    // ...
}
