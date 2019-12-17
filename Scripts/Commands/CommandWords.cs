using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandWords {
    Dictionary<string, Command> commands;
    private static Command[] commandArray = {new GoCommand(), new QuitCommand(), new OpenCommand() ,
                                            new StepAwayCommand(), new BackCommand(), new BuildCommand(),
                                             new DropCommand(), new TakeCommand(), new ViewCommand(), new UnlockCommand(),
                                            new VitalsCommand(), new TalkCommand(), new AttackCommand(), new ShootCommand(),
                                            new AcceptCommand(), new CraftCommand(), new UseCommand(), new ThrowCommand(),
                                            new PlantCommand()};

    public CommandWords() : this(commandArray) 
    {
    }

    public CommandWords(Command[] commandList)
    {
        commands = new Dictionary<string, Command>();
        foreach(Command command in commandList)
        {
            commands[command.name] = command;
        }
        Command help = new HelpCommand(this);
        commands[help.name] = help;
    }

    public Command get(string word)
    {
        Command command = null;
        commands.TryGetValue(word, out command);
        return command;
    }

    public string description()
    {
        string commandNames = "";
        Dictionary<string, Command>.KeyCollection keys = commands.Keys;
        foreach (string commandName in keys)
        {
            commandNames += " " + commandName;
        }
        return commandNames;


    }
}
