using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InfectedFactory {

    private Dictionary<string, Infected> _infected = new Dictionary<string, Infected>();

    public Infected getInfected(string key)
    {
        Infected infected = null;
        if (_infected.ContainsKey(key))
        {
            infected = _infected[key];
        }
        else
        {
            switch (key)
            {
                case "infected civilian":
                    infected = new InfectedCivilian();
                    break;
                case "infected scientist":
                    infected = new InfectedScientist();
                    break;
                case "infected soldier":
                    infected = new InfectedSoldier();
                    break;
            }
            _infected.Add(key, infected);
        }
        return infected.Clone();
    }
}
