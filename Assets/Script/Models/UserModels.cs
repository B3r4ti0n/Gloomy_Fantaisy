using System;
using UnityEngine;

[Serializable]
public class UserLogged {
    public int id;
    public string name;
    public string email;
    public string ID_Stats;
    public Stats stats;
}

[Serializable]
public class Stats {
    public int id;
    public int ID_Level;
    public string Race;
    public int Exp;
    public int Gold;
    public int Premium_gold;
    public int Health_Point;
    public int Offensive_value;
    public int Defensive_value;
    public int Intelligence_value;
    public int Speed_value;
    public int Mana_value;
}

public class NewBehaviourScript : MonoBehaviour {
    // ...
}
