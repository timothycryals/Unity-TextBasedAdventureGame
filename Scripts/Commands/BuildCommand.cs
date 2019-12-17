using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildCommand : Command {

    public BuildCommand() : base()
    {
        this.name = "build";
    }

    override
    public bool execute(Player player)
    {
        if (player.checkForRecipe("build recipe"))
        {
            if (this.hasSecondWord())
            {
                if (this.secondWord == "c4" && !this.hasThirdWord() && player.currentQuest.objective1 == "Build c4")
                {
                    ExplosiveDirector dir = new ExplosiveDirector();
                    Explosive e = new C4Builder();
                    Item c4 = null;

                    if (player.checkInventoryForItem("rdx") && player.checkInventoryForItem("plastic")
                        && player.checkInventoryForItem("electronics"))
                    {
                        if (player.checkInventoryForItem("c4") == false)
                        {
                            dir.Construct(e);
                            c4 = e.GetResult(player);
                            player.addToInventory(c4);
                            player.removeFromInventory("rdx");
                            player.removeFromInventory("plastic");
                            player.removeFromInventory("electronics");
                            player.startTimer();
                            player.currentQuest.completeObjective(player.currentQuest.objective1);
                            player.updateQuest();
                        }
                        else
                        {
                            player.outputMessage("\nYou already have C4");
                        }
                    }
                    else
                    {
                        player.outputMessage("\nYou do not have the required materials: <b>RDX, Plastic, Electronics</b>");
                    }
                }
                else if (this.secondWord == "pipe" && this.thirdWord == "bomb")
                {
                    ExplosiveDirector dir = new ExplosiveDirector();
                    Explosive e = new PipeBombBuilder();
                    Item pipeBomb = null;

                    if (player.checkInventoryForItem("pipe") && player.checkInventoryForItem("fuse")
                        && player.checkInventoryForItem("gunpowder"))
                    {
                        dir.Construct(e);
                        pipeBomb = e.GetResult(player);
                        player.addToInventory(pipeBomb);
                        player.removeFromInventory("pipe");
                        player.removeFromInventory("fuse");
                        player.removeFromInventory("gunpowder");
                    }
                    else
                    {
                        player.outputMessage("\nYou do not have the required materials: <b>Pipe, Fuse, Gunpowder</b>");
                    }
                }
                else
                {
                    Debug.Log(this.thirdWord);
                    player.outputMessage("\nUnknown item");
                }
            }
            else
            {
                player.outputMessage("\nBuild <b>What</b>?");
            }
        }
        else
        {
            player.outputMessage("\nYou do not have the proper recipe to use the <b>build</b> command.");
        }
        return false;
    }
}
