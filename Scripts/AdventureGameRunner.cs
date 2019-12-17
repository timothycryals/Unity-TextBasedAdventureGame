using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdventureGameRunner : MonoBehaviour {
    public Text output;
    public RectTransform rt;
    public Text input;
    public InputField inputField;
    private GameOutput gameIO;
    private Game game;

    // Use this for initialization
    public void Start () {
        Player.monitorPlayerHealth += monitorHealth;
        gameIO = new GameOutput(output);
        game = new Game(gameIO);
        gameIO.clear();
        game.start();
        inputField.ActivateInputField();
        Player.startBombTimer += startBombTimer;
        Player.EndGame += EndGame;
	}

    public void Update()
    {
         
    }

    public void sendCommand()
    {
        if(input.text.Length > 0)
        {
            game.execute(input.text);
            rt.localPosition = new Vector3(rt.localPosition.x, rt.rect.height, rt.localPosition.z);
            inputField.text = "";
            inputField.ActivateInputField();
        }
    }

    public void monitorHealth(Player player)
    {
        StartCoroutine(checkPlayerHealth(player));
    }

    IEnumerator checkPlayerHealth(Player player)
    {
        while (true)
        { 
            if (player.health <= 0)
            {
                player.outputMessage("\n\nYou were killed by the infected.\n");
                this.game.end();
                StopAllCoroutines();
                yield return new WaitForSeconds(0.1f);
            }
            else
            {
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    public void startBombTimer(Player player)
    {
        StartCoroutine(bombTimer(player));
    }

    IEnumerator bombTimer(Player player)
    {
        int timeLimit = 300;
        while (timeLimit > 0)
        {
            if ((timeLimit % 30) == 0 || timeLimit <= 10)
            {
                player.outputMessage("\nTime Remaining: " + timeLimit.ToString() + " seconds");
            }
            yield return new WaitForSeconds(1f);
            timeLimit -= 1;
        }
        if (player.checkInventoryForItem("c4"))
        {
            player.health = 0;
            player.outputMessage("\nThe C4 exploded while you were holding it.");
            game.end();
            StopAllCoroutines();
        }
        else if (player.currentRoom.checkRoomForItem("c4"))
        {
            player.health = 0;
            player.outputMessage("\nThe C4 exploded while you were in the same room as it.");
            game.end();
            StopAllCoroutines();
        }
        else if (player.currentRoom.checkContainersForItem("c4"))
        {
            player.health = 0;
            player.outputMessage("\nThe C4 exploded while you were in the same room as it.");
            game.end();
            StopAllCoroutines();
        }
        else
        {
            player.outputMessage("");
        }
    }

    public void EndGame()
    {
        this.game.end();
        StopAllCoroutines();
        
    }

}
