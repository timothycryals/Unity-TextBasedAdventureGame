using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackCommand : Command {

	public BackCommand() : base()
    {
        this.name = "back";
    }

    override
    public bool execute(Player player)
    {
        if (this.hasSecondWord() || this.hasThirdWord() || player.playerState == PlayerState.FIGHTING || player.playerState == PlayerState.SEARCHING)
        {
            player.outputMessage("\nI don't understand that command");
        }
        else
        {
            player.back();
        }
        return false;
    }
}
