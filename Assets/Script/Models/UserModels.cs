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
    public int gold_premium;
    public int health_point;
    public int offensive_value;
    public int defensive_value;
    public int intelligence_value;
    public int speed_value;
    public int mana_value;
    public int level_point;
}

[Serializable]
public class LevelUp {
    public int level;
    public int level_point;
    public int exp;
}

public class NewBehaviourScript : MonoBehaviour {
    // ...
}
