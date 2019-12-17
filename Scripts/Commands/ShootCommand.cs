using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootCommand : Command {

    public ShootCommand() : base()
    {
        this.name = "shoot";
    }

    override
    public bool execute(Player player)
    {
        if (player.primary != null && player.checkInventoryForItem(player.primary.Name + " bullet"))
        {
            if (this.hasSecondWord() && this.hasThirdWord())
            {
                player.attackNPC(this.secondWord + " " + this.thirdWord, player.primary);
                player.removeFromInventory(player.primary.Name + " bullet");
            }
            else
            {
                if (player.currentRoom.getFirstEnemy() != null)
                {
                    string npc = "";
                    npc = player.currentRoom.getFirstEnemy().Name;
                    player.attackNPC(npc, player.primary);
                    player.removeFromInventory(player.primary.Name + " bullet");
                }
                else
                {
                    player.outputMessage("\nNo enemies in this room.");
                }
            }
        }
        else
        {
            player.outputMessage("\nYou don't have any ammo.");
        }
        return false;
    }
}
