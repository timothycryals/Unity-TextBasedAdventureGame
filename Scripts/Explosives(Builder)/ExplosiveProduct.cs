using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveProduct {

    private List<Item> _parts = new List<Item>();

    public void Add(Item part)
    {
        _parts.Add(part);
    }

    public int getPartsLength()
    {
        return _parts.Count;
    }
    public void Show()
    {
        Debug.Log("\nExplosive parts ------");
        foreach (Item i in _parts)
            Debug.Log(i.name);
    }
}
