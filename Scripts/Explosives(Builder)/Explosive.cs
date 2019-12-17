using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Explosive {

    public abstract void buildCompound();
    public abstract void buildShell();
    public abstract void buildFuse();

    public abstract Item GetResult(Player p);
}
