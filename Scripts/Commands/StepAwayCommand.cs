using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepAwayCommand : Command {

	public StepAwayCommand() : base()
    {
        this.name = "step";
    }

    override
    public bool execute(Player player)
    {
        if (this.hasSecondWord() &&  !this.hasThirdWord())
        {
            if (this.secondWord == "away")
            {
                player.stepAway();
            }
            else
            {
                player.outputMessage("Command not complete");
            }
        }
        else
        {
            player.outputMessage("Command not complete");
        }
        return false;
    }
}
