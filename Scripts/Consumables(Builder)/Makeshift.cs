using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Abstract builder class for makeshift armor
public abstract class Makeshift  {

    public abstract void buildPartA();
    public abstract void buildPartB();
    public abstract void buildPartC();

    public abstract Item GetResult(Player p);
}
