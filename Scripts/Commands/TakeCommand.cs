using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeCommand :  Command {

	public TakeCommand() : base()
    {
        this.name = "take";
    }

    override
    public bool execute(Player player)
    {
        if (this.hasSecondWord() && this.hasThirdWord() == false)
        {
            player.takeItem(this.secondWord);
            player.updateQuest();
        }
        else if (this.hasThirdWord())
        {
            if (this.thirdWord == "bullet")
            {
                player.takeAmmo(this.secondWord + " bullet");
                this.secondWord = "";
                this.thirdWord = "";
            }
            else
            {
                player.takeItem(this.secondWord + " " + this.thirdWord);
                player.updateQuest();
                this.secondWord = "";
                this.thirdWord = "";
            }
        }
        else
        {
            player.outputMessage("\nTake <b>What</b>?");
        }
        return false;
            
    }
}
