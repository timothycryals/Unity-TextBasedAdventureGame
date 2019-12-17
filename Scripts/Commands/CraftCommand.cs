using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftCommand : Command
{

    public CraftCommand() : base()
    {
        this.name = "craft";
    }

    override
    public bool execute(Player player)
    {
        if (player.checkForRecipe("craft recipe"))
        {
            if (this.hasSecondWord())
            {
                if (this.secondWord == "armor" && this.hasThirdWord() == false)
                {
                    MakeshiftDirector dir = new MakeshiftDirector();
                    Makeshift e = new MakeshiftArmorBuilder();
                    Item makeshiftArmor = null;

                    if (player.checkInventoryForItem("scrap metal") && player.checkInventoryForItem("duct tape")
                        && player.checkInventoryForItem("fabric"))
                    {
                        dir.Construct(e);
                        makeshiftArmor = e.GetResult(player);
                        player.armor = (Armor)makeshiftArmor;
                        player.removeFromInventory("scrap metal");
                        player.removeFromInventory("duct tape");
                        player.removeFromInventory("fabric");
                    
                    }
                    else
                    {
                        player.outputMessage("\nYou do not have the required materials: <b>scrap metal, duct tape, fabric</b>");
                    }
                }
                else
                {
                    player.outputMessage("\nCraft <b>What</b>?");
                }
                
            }
            else
            {
                player.outputMessage("\nCraft <b>What</b>?");
            }

        }
        else
        {
            player.outputMessage("\nYou do not have the <b>craft</b> recipe.");
        }
        return false;
    }
}
