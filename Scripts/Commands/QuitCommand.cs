﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitCommand : Command {

    public QuitCommand() : base()
    {
        this.name = "quit";
    }

    override
    public bool execute(Player player)
    {
        bool answer = true;
        if (this.hasSecondWord())
        {
            player.outputMessage("\nI cannot quit <b>" + this.secondWord + "</b>");
            answer = false;
        }
        player.endGame();
        return answer;
    }
}
