using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character {

    private string _name;
    public string name { get { return _name; } }
    

    private Weapon _weapon;
    public Weapon weapon { get { return _weapon; } }

    private string greetMessage;

    public Character(string Name, Weapon Weapon, string message)
    {
        this._name = Name;
        this._weapon = Weapon;
        this.greetMessage = message;
    }

    public string giveMessage()
    {
        return this.greetMessage;
    }

    //public void attack(Infected npc)
    //{
    //    string output = "";
    //    int hitChance = Random.Range(1, 5);
    //    if (hitChance == 1 || hitChance == 2)
    //    {
    //        npc.damage(this._weapon.damage);
    //}
}
