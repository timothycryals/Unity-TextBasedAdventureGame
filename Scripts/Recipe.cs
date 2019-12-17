using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recipe
{
    private string name;
    public string Name { get { return name; } }

    public Recipe(string name)
    {
        this.name = name;
    }
}
