using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DiscoveryStatus { OPENED, CLOSED, EMPTIED };

public class Container {

    private string _name;
    public string name { get { return _name; } }

    private Dictionary<string, Item> contents;
    public Dictionary<string, Item> Contents { get { return contents; } }

    private Dictionary<string, Weapon> weapons;
    public Dictionary<string, Weapon> Weapons { get { return weapons; } }

    private DiscoveryStatus _opened;
    public DiscoveryStatus opened { get { return _opened; } set { _opened = value; } }


    

    public Container(string Name)
    {
        this._name = Name;
        this.contents = new Dictionary<string, Item>();
        this._opened = DiscoveryStatus.CLOSED;
        this.weapons = new Dictionary<string, Weapon>();
    }

    public Item getItem(string itemName, Player p)
    {
        Item item = null;
        this.contents.TryGetValue(itemName, out item);
        if (item == null)
        {
            p.outputMessage("No such item in the container");
        }
        return item;
    }

    public string getContents()
    {
        string itemNames = "";
        Dictionary<string, Item>.KeyCollection keys = contents.Keys;
        foreach (string itemName in keys)
        {
            itemNames += " " + itemName + ",";
        }

        foreach (string weaponName in this.weapons.Keys)
        {
            itemNames += " " + weaponName + ",";
        }

        return itemNames;
    }

    //public string unlock(Item k)
    //{
    //    string output = "";
    //    switch (this.lockstate)
    //    {
    //        case LockState.LOCKED:
    //            if(k == this._key)
    //            {
    //                this._lockstate = LockState.UNLOCKED;
    //                output = "\nYou have unlocked the container";
    //                break;
    //            }
    //            else
    //            {
    //                output = "\nKey does not fit the container";
    //                break;
    //            }
    //        case LockState.UNLOCKED:
    //            output = "\nContainer is already unlocked";
    //            break;

    //    }
    //    return output;
    //}

    public void addItem(Item i)
    {
        Item temp = null;
        this.contents.TryGetValue(i.name, out temp);
        if (temp == null)
        {
            this.contents.Add(i.name, i);
        }
        else
        {
            temp.amount += 1;
            temp.totalWeight += temp.weight;
            this.contents[temp.name] = temp;
        }
    }

    public bool checkContainerForItem(string item)
    {
        Item temp = null;
        this.Contents.TryGetValue(item, out temp);
        if (temp == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void addWeapon(Weapon weapon)
    {
        this.weapons.Add(weapon.Name, weapon);
    }

    public void removeWeapon(string name)
    {
        this.weapons.Remove(name);
    }
}
