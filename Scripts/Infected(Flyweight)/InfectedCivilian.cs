using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InfectedCivilian : Infected
{

    public InfectedCivilian()
    {
        this.name = "infected civilian";
        this.health = 50;
        this.state = HealthState.ALIVE;
        this.belongings = new Dictionary<string, Item>();
        Player.civilianAttack += takeDamage;
    }

    override
    public Infected Clone()
    {
        InfectedCivilian other = (InfectedCivilian)this.MemberwiseClone();
        return other;
    }

    override
    public void attack(Player player)
    {
        if (this.state != HealthState.DEAD)
        {
            player.playerState = PlayerState.FIGHTING;
            string output = "";
            int hitChance = UnityEngine.Random.Range(1, 5);
            if (hitChance == 1 || hitChance == 2)
            {
                if (player.Follower == null)
                {
                    if (player.armor == null || player.armor.Condition == ArmorCondition.BROKEN)
                    {
                        player.health -= 8;
                        output = "\nThe infected civilian hit you for 8 damage";
                    }
                    else
                    {
                        player.armor.degrade();
                        player.outputMessage("\nYour armor protected you from the enemy's attack.");
                    }
                }
                else
                {
                    int followerChance = UnityEngine.Random.Range(1, 3);
                    switch (followerChance)
                    {
                        case 1:
                            this.health -= player.Follower.weapon.Damage;
                            output = "\nYou're follower attacked the infected civilian.";
                            break;
                        case 2:
                            if (player.armor == null || player.armor.Condition == ArmorCondition.BROKEN)
                            {
                                player.health -= 8;
                                output = "\nThe infected civilian hit you for 8 damage";
                            }
                            else
                            {
                                player.armor.degrade();
                                player.outputMessage("\nYour armor protected you from the enemy's attack.");
                            }
                            break;
                    }
                }
            }
            else
            {
                output = "\nThe infected civilian tried to attack, but missed.";
            }
            player.outputMessage(output);
        }
    }

    override
    public void takeDamage(int damageAmount, Player player)
    {
        player.currentRoom.getEnemy("infected civilian").damage(damageAmount, player);
        //this.damage(damageAmount, player);
    }

    override
    public void damage(int damageAmount, Player player)
    {
        this.health -= damageAmount;
        if (this.health <= 0)
        {
            this.state = HealthState.DEAD;
            player.outputMessage("\nYou have killed the infected civilian.");
            int respawn = UnityEngine.Random.Range(1, 100);
            player.currentRoom.clearEnemies();
            if ((respawn <= player.currentRoom.enemySpawnChance && player.currentRoom.enemySpawnChance != 0))
            {
                player.currentRoom.addEnemyToRoom(respawn, GameWorld.instance.spawner.getInfected("infected civilian"));
                player.outputMessage("\nAnother infected civilian is coming to attack you");
            }
            else if (player.currentRoom.enemySpawnChance == 0 || respawn > player.currentRoom.enemySpawnChance)
            {
                player.playerState = PlayerState.NEUTRAL;
                player.outputMessage("\nYou have cleared the room of enemies.");
                player.checkQuest();
            }

            
        }
        else
        {
            player.outputMessage("\nYou did " + damageAmount + " damage to the infected civilian.");
            this.attack(player);
        }
    }

    override
    public void addItemToBelongings(Item item)
    {
        if (this.belongings.ContainsKey(item.name))
        {
            this.belongings[item.name].amount += 1;
            this.belongings[item.name].totalWeight += item.weight;
        }
        else
        {
            this.belongings.Add(item.name, item);
        }
    }

    override
    public string viewBelongings()
    {
        string temp = "\nThe body has: ";
        foreach (string i in this.belongings.Keys)
        {
            temp += i;
        }

        return temp;
    }

    override
    public string description(string tag)
    {
        string output = "";
        switch (this.state)
        {
            case HealthState.ALIVE:
                output = "\nIt appears to be a civilian who was infected by the virus.\n" + tag;
                break;
            case HealthState.DEAD:
                output = "\nThe body looks like a civilian who was infected by the virus.\n" + tag;
                break;
        }
        return output;
    }
}
