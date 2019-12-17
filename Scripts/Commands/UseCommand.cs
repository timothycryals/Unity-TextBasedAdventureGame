using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseCommand : Command {

	public UseCommand() : base()
    {
        this.name = "use";
    }

    override
    public bool execute(Player player)
    {
        if (this.hasSecondWord() && this.hasThirdWord() == false)
        {
            if (player.checkInventoryForItem(this.secondWord))
            {
                if (player.Inventory[this.secondWord].GetType() == typeof(Consumable))
                {
                    Consumable temp = (Consumable)player.Inventory[this.secondWord];
                    player.health += temp.HealthReward;
                    if (player.health > 100)
                    {
                        player.health = 100;
                    }
                    player.weight -= temp.weight;
                    player.removeFromInventory(this.secondWord);
                }
                else
                {
                    player.outputMessage("\nYou can not use this item.");
                }
            }
            else
            {
                player.outputMessage("\nNo such item in your inventory.");
            }
        }
        else if (this.hasThirdWord())
        {
            if (this.secondWord == "boltcutters")
            {
                if (player.checkInventoryForItem("boltcutters"))
                {
                    Door temp = player.currentRoom.getExit(this.thirdWord);
                    if (temp != null)
                    {
                        if (temp.getOtherRoom(player.currentRoom).hasBarrier() && temp.getOtherRoom(player.currentRoom).Barrier.GetType() == typeof(Fence))
                        {
                            switch (temp.getOtherRoom(player.currentRoom).getBarrierState())
                            {
                                case BarrierCondition.INTACT:
                                    temp.getOtherRoom(player.currentRoom).Barrier.destroyBarrier();
                                    player.outputMessage("\nYou cut the fence surrounding " + temp.getOtherRoom(player.currentRoom).name);
                                    break;
                                case BarrierCondition.WEAK:
                                    temp.getOtherRoom(player.currentRoom).Barrier.destroyBarrier();
                                    player.outputMessage("\nYou cut the fence surrounding " + temp.getOtherRoom(player.currentRoom).name);
                                    break;
                                case BarrierCondition.DESTROYED:
                                    player.outputMessage("\nThe fence is already cut.");
                                    break;
                            }
                        }
                        else
                        {
                            player.outputMessage("\nThere is no fence surrounding " + temp.getOtherRoom(player.currentRoom).name);
                        }
                    }
                    else
                    {
                        player.outputMessage("\nThere is no exit on " + this.thirdWord);
                    }
                }
                else
                {
                    player.outputMessage("\nYou do not have boltcutters.");
                }
            }
            else
            {
                player.outputMessage("\nYou cannot use that item.");
            }
        }
        else
        {
            player.outputMessage("\nUse <b>What</b>?");
        }
        return false;
    } 
}
