using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularLock : LockingStrategy {

    private Item originalKey;
    
 
    private bool locked;

    public RegularLock(Item key)
    {
        locked = false;
        originalKey = key;
    }

    public bool doLock()
    {
        locked = true;
        return locked;
    }

    public bool doUnlock()
    {
        locked = false;
        return locked;
    }

    public bool isLocked()
    {
        return locked;
    }

    public bool isUnlocked()
    {
        return !locked;
    }

    public bool mayClose()
    {
        return true;
    }

    public bool mayOpen()
    {
        return !locked;
    }

    public string getKey()
    {
        return this.originalKey.name;
    }
}
