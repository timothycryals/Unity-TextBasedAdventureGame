using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveDirector{

	public void Construct(Explosive bomb) {
        bomb.buildCompound();
        bomb.buildShell();
        bomb.buildFuse();
    }
}
