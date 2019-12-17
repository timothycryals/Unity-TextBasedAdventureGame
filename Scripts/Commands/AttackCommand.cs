using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCommand : Command {

	public AttackCommand() : base()
    {
        this.name = "attack";
    }

    override
    public bool execute(Player player)
    {

        if (this.hasSecondWord() && this.hasThirdWord())
        {
            player.attackNPC(this.secondWord + " " + this.thirdWord, player.secondary);
        }
        else
        {
            if (player.currentRoom.getFirstEnemy() != null)
            {
                string npc = "";
                npc = player.currentRoom.getFirstEnemy().Name;
                player.attackNPC(npc, player.secondary);
            }
            else
            {
                player.outputMessage("\nNo enemies in this room.");
            }
        }
        return false;
    }
}
