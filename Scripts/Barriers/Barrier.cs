using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BarrierCondition {INTACT, WEAK, DESTROYED}

public class Barrier {

    protected BarrierCondition condition = BarrierCondition.INTACT;

    public void damageBarrier()
    {
        if(this.condition == BarrierCondition.INTACT)
        {
            this.condition = BarrierCondition.WEAK;
        }
        else if (this.condition == BarrierCondition.WEAK)
        {
            this.condition = BarrierCondition.DESTROYED;
        }
    }

    public void destroyBarrier()
    {
        this.condition = BarrierCondition.DESTROYED;
    }

    public void repairBarrier()
    {
        if (this.condition == BarrierCondition.WEAK)
        {
            this.condition = BarrierCondition.INTACT;
        }
    }

    public BarrierCondition GetBarrierCondition()
    {
        return this.condition;
    }
}
