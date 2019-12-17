using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeshiftDirector {

    public void Construct(Makeshift armor)
    {
        armor.buildPartA();
        armor.buildPartB();
        armor.buildPartC();
    }
}
