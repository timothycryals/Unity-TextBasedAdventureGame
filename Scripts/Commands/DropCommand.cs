using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropCommand : Command {

    public DropCommand() : base()
    {
        this.name = "drop";
    }

    override
    public bool execute(Player player)
    {
        if (this.hasSecondWord() && this.hasThirdWord() == false)
        {
            if (this.secondWord != "c4")
            {
                player.dropFromInventory(this.secondWord);
            }
            else
            {
                player.outputMessage("\nYou cannot drop the c4.");
            }
        }
        else if (this.hasThirdWord())
        {
            player.dropFromInventory(this.secondWord + " " + this.thirdWord);
        }
        else
        {
            player.outputMessage("\nDrop <b>What</b>?");
        }
        return false;
    }
}
