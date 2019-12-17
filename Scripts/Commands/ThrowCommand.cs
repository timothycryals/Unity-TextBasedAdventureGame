using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowCommand : Command {

	public ThrowCommand() : base()
    {
        this.name = "throw";
    }

    override
    public bool execute(Player player)
    {
        if (this.secondWord == "grenade" && player.checkInventoryForItem("grenade") && this.hasThirdWord() == false )
        {
            player.currentRoom.enemySpawnChance -= 20;
            if (player.currentRoom.enemySpawnChance < 0)
            {
                player.currentRoom.enemySpawnChance = 0;
            }
            player.removeFromInventory("grenade");
        }
        else if (this.secondWord == "pipe" && this.thirdWord == "bomb" && player.checkInventoryForItem("pipe pomb"))
        {
            player.currentRoom.enemySpawnChance -= 10;
            if (player.currentRoom.enemySpawnChance < 0)
            {
                player.currentRoom.enemySpawnChance = 0;
            }
            player.removeFromInventory("pipe bomb");
        }
        else
        {
            player.outputMessage("\nThrow <b>What</b>?");
        }

        return false;
    }
}
