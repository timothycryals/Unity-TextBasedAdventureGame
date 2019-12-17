using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parser {
    private CommandWords commands;

    public Parser() : this(new CommandWords())
    {

    }

    public Parser(CommandWords newCommands)
    {
        commands = newCommands;
    }

    public Command parseCommand(string commandString)
    {
        Command command = null;
        string[] words = commandString.Split(' ');
        if (words.Length > 0)
        {
            command = commands.get(words[0]);
            if (command != null)
            {
                if (words.Length == 2)
                {
                    command.secondWord = words[1];
                    //command.thirdWord = words[2];
                }
                else if (words.Length == 3)
                {
                    command.secondWord = words[1];
                    command.thirdWord = words[2];
                }
                else
                {
                    command.secondWord = null;
                    command.thirdWord = null;
                }
            }
            else
            {
                Debug.Log(">>>Did not find the command " + words[0]);
            }
        }
        else
        {
            Debug.Log("No words parsed!");
        }
        return command;
    }

    public string description()
    {
        return commands.description();
    }
}
