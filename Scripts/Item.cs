using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum Condition { GOOD, FAIR, POOR, BROKEN, NA }

public class Item {

    protected string _name;
    public string name { get { return _name; } set { _name = value; } }

    protected int _weight;
    public int weight { get { return _weight; } }

    protected int _totalWeight;
    public int totalWeight { get { return _totalWeight; } set { _totalWeight = value; } }

    protected int _amount;
    public int amount { get { return _amount; } set { _amount = value; } }

    private bool questItem;

    //private Condition _quality;
   // public Condition quality { get { return _quality; } set { this._quality = value; } }

    public Item(string Name, int Weight)
    {
        this.amount = 1;
        this._name = Name;
        this._weight = Weight;
    }

    public bool isQuestItem()
    {
        return questItem;
    }

    
    public override bool Equals(object obj)
    {
        Item item = (Item) obj;
        if (this._name == item.name && this.weight == item.weight)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

}
