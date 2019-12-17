using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface LockingStrategy
{
    bool mayOpen();
    bool mayClose();
    bool doLock();
    bool doUnlock();
    bool isLocked();
    bool isUnlocked();
    string getKey();
}
