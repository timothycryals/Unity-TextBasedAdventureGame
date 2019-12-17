using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType { FIREARM, MELEE}
public enum WeaponCondition { GOOD, FAIR, POOR, BROKEN}

public abstract class Weapon {

    protected string name;
    public string Name { get { return name; } }
    protected WeaponType type;
    public WeaponType Type { get { return type; } }
    protected WeaponCondition condition;
    public WeaponCondition Condition { get { return condition; } }
    protected int damage;
    public int Damage { get { return damage; } } 


    public abstract void degrade();

    public abstract void repair();

}
