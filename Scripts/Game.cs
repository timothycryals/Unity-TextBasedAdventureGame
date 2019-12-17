using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game {
    Player player;
    Parser parser;
    bool playing;
    //Room exit;

    public Game(GameOutput gameIO)
    {
        playing = false;
        parser = new Parser(new CommandWords());
        player = new Player(GameWorld.instance.entrance, gameIO);
        List<Quest> initialQuest = new List<Quest>();
        initialQuest.Add(new Quest("Explore", QuestType.GO, "Explore the world", null, ""));
        player.addQuestline(initialQuest);
        player.startHealthMonitor();
        
    }

    public void start()
    {
        playing = true;
        player.outputMessage(welcome());
    }

    public void end()
    {
        playing = false;
        player.outputMessage(goodbye());
    }

    public string welcome()
    {
        return "Welcome to Quarantined!\n\n Quarantined is a post-apocalyptic adventure game where your main objective is to escape the city.\n\nType 'help' if you need help." + player.currentRoom.description();
    }

    public string goodbye()
    {
        return "\nThank you for playing, Goodbye. \n";
    }

    public bool execute(string commandString)
    {
        bool finished = false;
        if (playing)
        {
            player.outputMessage("\n>" + commandString);
            Command command = parser.parseCommand(commandString);
            if (command != null)
            {
                finished = command.execute(player);
            }
            else
            {
                player.outputMessage("\nI don't understand what you mean by '<b>" + commandString + "</b>'");
            }
        }
        return finished;
    }
}
