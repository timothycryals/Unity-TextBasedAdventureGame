using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantCommand : Command {

	public PlantCommand() : base()
    {
        this.name = "plant";
    }

    override
    public bool execute(Player player)
    {
        if (player.currentQuest.type == QuestType.PLANT)
        {
            if (player.currentQuest.objective1.Contains(player.currentRoom.name))
            {
                player.removeFromInventory("c4");
                player.outputMessage("\nYou plant the c4 and take cover. You go through the hole in the wall and escape.");
                player.endGame();
                
            }
            else
            {
                player.outputMessage("\nYou cannot plant the c4 here.");
            }
        }
        return false;
    }
}
