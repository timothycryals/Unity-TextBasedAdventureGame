using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcceptCommand : Command {

	public AcceptCommand() : base()
    {
        this.name = "accept";
    }

    override
    public bool execute(Player player)
    {
        if (player.currentRoom.characterInRoom("explosives expert"))
        {
            List<Quest> ExplosivesQuestline = new List<Quest>();
            ExplosivesQuestline.Add(new Quest("Building a Bomb", QuestType.FIND, "Find RDX compound", "Find Plastic", "Find Electronics",
                                            new Item("rdx", 5), new Item("plastic", 2), new Item("electronics", 2), ""));
            ExplosivesQuestline.Add(new Quest("Arming the Bomb", QuestType.BUILD, "Build c4", null , ""));
            ExplosivesQuestline.Add(new Quest("Beat the Clock", QuestType.GO, "Go to the wall", null, ""));
            ExplosivesQuestline.Add(new Quest("Planting the Bomb", QuestType.PLANT, "Plant the bomb on the wall", null, ""));
            player.addQuestline(ExplosivesQuestline);
            player.Follower = player.currentRoom.getCharacter("explosives expert");
            player.giveRecipe(new Recipe("build recipe"));
            player.outputMessage("\nYour new follower has given you the <b>build</b> recipe.\nYou can now build c4 and pipe bombs.");

            player.currentRoom.clearCharacters();
        }
        else if(player.currentRoom.characterInRoom("scavenger"))
        {
            List<Quest> ScavengerQuestline = new List<Quest>();
            ScavengerQuestline.Add(new Quest("Electrical Work", QuestType.FIND, "Find a car battery", "Find a spark plug", "Find a gas can",
                                            new Item("car battery", 10), new Item("spark plug", 4), new Item("gas can", 10), ""));
            ScavengerQuestline.Add(new Quest("Body work", QuestType.FIND, "Find scrap metal", new Item("scrap metal", 10), ""));
            ScavengerQuestline.Add(new Quest("Supply Run", QuestType.GO, "Go to the auto shop", null, "\nYou helped fix the vehicle and manage to drive straight through the front gate."));
            player.addQuestline(ScavengerQuestline);
            player.Follower = player.currentRoom.getCharacter("scavenger");
            player.giveRecipe(new Recipe("craft recipe"));
            player.currentRoom.clearCharacters();
        }
        else if (player.currentRoom.characterInRoom("former soldier"))
        {
            player.Follower = player.currentRoom.getCharacter("former soldier");
            player.currentRoom.clearCharacters();
        }
        else
        {
            player.outputMessage("\nNo offered quests to accept.");
        }
        return false;
    }
}
