using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCommand : Command {

	public OpenCommand() : base()
    {
        this.name = "open";
    }

    override
    public bool execute(Player player)
    {
        if (this.hasSecondWord())
        {
            if (this.hasThirdWord())
            {
                string containerName = this.secondWord + " " + this.thirdWord;
                player.open(containerName);
            }
            else
            {
                string containerName = this.secondWord;
                player.open(containerName);
            }
        }
        else
        {
            player.outputMessage("\nOpen <b>What</b>?");
        }
        return false;
    }
}
