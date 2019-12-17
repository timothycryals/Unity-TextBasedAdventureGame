using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : Item {

    private int healthReward;
    public int HealthReward { get { return healthReward; } }

	public Consumable(string Name, int Weight, int Reward) : base(Name, Weight)
    {
        this._name = Name;
        this._weight = Weight;
        this.healthReward = Reward;
    }
}
