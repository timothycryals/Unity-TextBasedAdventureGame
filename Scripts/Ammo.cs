using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : Item {

	public Ammo(string Name, int Weight) : base(Name, Weight)
    {
        this.amount = 20;
        this._name = Name;
        this._weight = Weight;
    }
}
