using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HealthState { ALIVE, DEAD}

public abstract class Infected {

    protected string name;
    public string Name { get { return name; } }
	protected int health;
    public int Health { get { return health; } set { health = value; } }
    protected HealthState state;
    public HealthState State { get { return state; } set { state = value; } }
    protected Dictionary<string, Item> belongings;
    public Dictionary<string, Item> Belongings { get { return belongings; } }

    public abstract Infected Clone();

    public abstract void attack(Player player);

    public abstract void damage(int damageAmount, Player player);

    public abstract void takeDamage(int damageAmount, Player player);

    public abstract void addItemToBelongings(Item item);

    public abstract string viewBelongings();

    public abstract string description(string tag);
}
