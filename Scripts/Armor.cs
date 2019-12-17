using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ArmorCondition {GOOD, MODERATE, WEAK, BROKEN}

public class Armor : Item {

    private ArmorCondition condition;
    public ArmorCondition Condition { get { return condition; } }

	public Armor(string Name, int Weight, ArmorCondition Condition) : base(Name, Weight)
    {
        this._amount = 1;
        this._weight = Weight;
        this._name = Name;
        this.condition = Condition;
    }

    public void degrade()
    {
        int degradeChance = Random.Range(1, 100);
        switch (this.condition)
        {
            case ArmorCondition.GOOD:
                if(degradeChance <= 20)
                {
                    this.condition = ArmorCondition.MODERATE;
                }
                break;
            case ArmorCondition.MODERATE:
                if(degradeChance <= 30)
                {
                    this.condition = ArmorCondition.WEAK;
                }
                break;
            case ArmorCondition.WEAK:
                if(degradeChance <= 40)
                {
                    this.condition = ArmorCondition.BROKEN;
                }
                break;
        }
    }

    public void repair()
    {
        switch (this.condition)
        {
            case ArmorCondition.MODERATE:
                this.condition = ArmorCondition.GOOD;
                break;
            case ArmorCondition.WEAK:
                this.condition = ArmorCondition.MODERATE;
                break;
            case ArmorCondition.BROKEN:
                this.condition = ArmorCondition.WEAK;
                break;
        }
    }
}
