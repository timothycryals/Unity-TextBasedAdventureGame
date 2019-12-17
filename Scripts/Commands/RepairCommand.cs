using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairCommand : Command {

	public RepairCommand() : base()
    {
        this.name = "repair";
    }

    override
    public bool execute(Player player)
    {
        if (player.checkForRecipe("repair recipe"))
        {
            if (player.checkInventoryForItem("repair kit"))
            {
                if (this.hasSecondWord() && this.hasThirdWord() == false)
                {
                    switch (this.secondWord)
                    {
                        case "armor":
                            if (player.armor != null)
                            {
                                if (player.armor.Condition != ArmorCondition.GOOD)
                                {
                                    player.armor.repair();
                                    player.removeFromInventory("repair kit");
                                }
                                else
                                {
                                    player.outputMessage("\nYour armor is already in good condition.");
                                }
                            }
                            else
                            {
                                player.outputMessage("\nYou do not have armor.");
                            }
                            break;
                        case "primary":
                            if (player.primary != null)
                            {
                                if (player.primary.Condition != WeaponCondition.GOOD)
                                {
                                    player.primary.repair();
                                    player.removeFromInventory("repair kit");
                                }
                                else
                                {
                                    player.outputMessage("\nYour primary weapon is already in good condition.");
                                }
                            }
                            else
                            {
                                player.outputMessage("\nYou do not have a primary weapon.");
                            }
                            break;
                        case "secondary":
                            if (player.secondary != null)
                            {
                                if (player.secondary.Condition != WeaponCondition.GOOD)
                                {
                                    player.secondary.repair();
                                    player.removeFromInventory("repair kit");
                                }
                                else
                                {
                                    player.outputMessage("\nYour secondary weapon is already in good condition.");
                                }
                            }
                            else
                            {
                                player.outputMessage("\nYou do not have a secondary weapon.");
                            }
                            break;
                    }
                }
            }
            else
            {
                player.outputMessage("\nYou do not have any repair kits.");
            }
        }
        else
        {
            player.outputMessage("\nYou do not have the <b>repair</b> recipe.");
        }
        return false;
    }
}
