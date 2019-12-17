using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkCommand : Command {

	public TalkCommand() : base()
    {
        this.name = "talk";
    }

    override
    public bool execute(Player player)
    {
        if (this.hasSecondWord()  && !this.hasThirdWord())
        {
            Character temp = player.currentRoom.getCharacter(this.secondWord.ToLower());
            player.talkTo(temp);
        }
        else if (this.hasSecondWord() && this.hasThirdWord())
        {
            Character temp = player.currentRoom.getCharacter(this.secondWord.ToLower() + " " + this.thirdWord.ToLower());
            player.talkTo(temp);
        }
        else
        {
            player.outputMessage("\nTalk to <b>Who</b>?");
        }

        return false;
    }
}
