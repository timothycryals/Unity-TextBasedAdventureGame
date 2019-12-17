using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockCommand : Command {

	public UnlockCommand() : base()
    {
        this.name = "unlock";
    }

    override
    public bool execute(Player p)
    {
        if (this.hasSecondWord())
        {
            p.openDoor(this.secondWord);
        }
        else
        {
            p.outputMessage("\nUnlock <b>What</b>.");
        }
        return false;
    }
}
