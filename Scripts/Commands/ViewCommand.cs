using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewCommand : Command {

	public ViewCommand() : base()
    {
        this.name = "view";
    }

    override
    public bool execute(Player p)
    {
        if (this.hasSecondWord())
        {
            if (this.secondWord == "quest")
            {
                p.outputMessage(p.currentQuest.ShowQuest());
            }
            else if (this.secondWord == "inventory")
            {
                p.outputMessage(p.viewInventory());
            }
            else
            {
                p.outputMessage("Unknown item");
            }
        }
        else
        {
            p.outputMessage("\nView <b>What</b>?");
        }
        return false;
    }
}
