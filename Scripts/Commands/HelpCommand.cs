using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpCommand : Command {
    CommandWords words;

    public HelpCommand() : this(new CommandWords())
    {
    }

    public HelpCommand(CommandWords commands) : base()
    {
        words = commands;
        this.name = "help";
    }

    override
    public bool execute(Player player)
    {
        if(this.hasSecondWord())
        {
            player.outputMessage("\nI cannot help you with <color=blue><i><b>" + this.secondWord + "</b></i></color>");
        }
        else
        {
            player.outputMessage("\nYou are lost. You are alone. You wander around the university, \n\nYour available commands are <b>" + words.description() + "</b>");
        }
        return false;
    }
}
