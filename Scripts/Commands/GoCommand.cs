using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoCommand : Command {

    public GoCommand() : base()
    {
        this.name = "go";
    }

    override
    public bool execute(Player player)
    {
        if (this.hasSecondWord())
        {
            switch (player.playerState)
            {
                case PlayerState.NEUTRAL:
                    player.waltTo(this.secondWord);
                    player.updateQuest();
                    break;
                case PlayerState.SEARCHING:
                    player.outputMessage("\nYou must step away from the container first.");
                    break;
                case PlayerState.FIGHTING:
                    player.outputMessage("\nYou cannot go anywhere while in combat");
                    break;
            }
            
        }
        else
        {
            player.outputMessage("\nGo <b>Where</b>?");
        }
        return false;
    }
}
