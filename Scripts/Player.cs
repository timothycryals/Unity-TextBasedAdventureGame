using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum PlayerState { NEUTRAL, FIGHTING, SEARCHING}

public class Player {
    public static event Action<Player, Dictionary<string, Room>> playerEnteredRoom;
    public static event Action<Item, Player> playerPickedUpItem;
    public static event Action<Player> startBombTimer;
    public static event Action<Player> monitorPlayerHealth;
    public static event Action<int, Player> civilianAttack;
    public static event Action<int, Player> scientistAttack;
    public static event Action<int, Player> soldierAttack;
    public static event Action EndGame;

    private Character follower;
    public Character Follower { get { return follower; } set { follower = value; } }

    private Quest _currentQuest;
    public Quest currentQuest { get { return _currentQuest; } set { _currentQuest = value; } }

    private List<Quest> questLine;

    private List<Room> travelPath;

    private PlayerState _playerState;
    public PlayerState playerState { get { return _playerState; } set { _playerState = value; } }

    private Room _currentRoom = null;
    public Room currentRoom { get{ return _currentRoom;} set { _currentRoom = value; } }

    private Container _currentContainer = null;
    public Container currentContainer { get { return _currentContainer; } set { _currentContainer = value; } }

    private int _health = 100;
    public int health { get { return _health; } set { _health = value; } }

    private Armor _armor;
    public Armor armor { get { return _armor; } set { _armor = value; } }

    private int _maxWeight = 50;
    public int maxWeight { get { return _maxWeight; } }

    private int _weight;
    public int weight { get { return _weight; } set { _weight = value; } }

    private Dictionary<string, Item> inventory;
    public Dictionary<string, Item> Inventory { get { return inventory; } }

    private Dictionary<string, Recipe> recipes;

    private Weapon _primary;
    public Weapon primary { get { return _primary; } set { _primary = value; } }

    private Weapon _secondary;
    public Weapon secondary { get { return _secondary; } set { _secondary = value; } }

    private GameOutput _io = null;

    public Player(Room room, GameOutput output)
    {
        this.travelPath = new List<Room>();
        this.travelPath.Add(room);
        this._playerState = PlayerState.NEUTRAL;
        this.inventory = new Dictionary<string, Item>();
        this.recipes = new Dictionary<string, Recipe>();
        _currentRoom = room;
        _io = output;
        
    }

    public void waltTo(string direction)
    {
        Door door = this._currentRoom.getExit(direction);

        if (door != null)
        {
            if (door.isOpen)
            {
                //Room nextRoom = this._currentRoom.getExit(direction);
                Room nextRoom = door.getOtherRoom(currentRoom);
                if (nextRoom != null && (!nextRoom.hasBarrier() || nextRoom.getBarrierState() == BarrierCondition.DESTROYED))
                {
                    Dictionary<string, Room> envoy = new Dictionary<string, Room>();
                    envoy["beforeRoom"] = currentRoom;
                    envoy["afterRoom"] = nextRoom;
                    this._currentRoom.clearEnemies();
                    if (this.Follower != null)
                    {
                        nextRoom.clearCharacters();
                    }
                    this._currentRoom = nextRoom;
                    Debug.Log(this._currentRoom.name);
                    this.outputMessage("\n" + this._currentRoom.description());
                    if (playerEnteredRoom != null)
                    {
                        playerEnteredRoom(this, envoy);
                    }
                    this.travelPath.Add(nextRoom);
                    
                }
                else
                {
                    this.outputMessage("\nThere is a barrier surrounding that area.");
                }
            }
            else
            {
                this.outputMessage("\nThe door is closed.");
            }
        }
        else
        {
            this.outputMessage("\nThere is no door on " + direction);
        }
          
    }

    public void showVitals()
    {
        if (this._armor != null)
        {
            this.outputMessage("\nHealth: " + this._health + "/100\tWeight: " + this._weight + "/" + this._maxWeight + "\t\tArmor: " + this._armor.Condition);
        }
        else
        {
            this.outputMessage("\nHealth: " + this._health + "/100\tWeight: " + this._weight + "/" + this._maxWeight);
        }
        if (this._primary != null)
        {
            this.outputMessage("\nPrimary: " + this._primary.Condition);
        }
        if (this._secondary != null)
        {
            this.outputMessage("\nSecondary: " + this._secondary.Condition);
        }    
    }

    public void stepAway()
    {
        this._playerState = PlayerState.NEUTRAL;
        string output = "";
        if (this._currentContainer == null)
        {
            output = "You are not looking at a container";
        }
        else
        {
            this._currentContainer = null;
            output = "You have stepped away from the container";
        }
        this.outputMessage("\n" + output);
    }

    public void open(string containerName)
    {
        this._playerState = PlayerState.SEARCHING;
        Container container = null;
        container = this._currentRoom.getContainer(containerName);
        if (container == null)
        {
            this.outputMessage("\nContainer not found");
        }
        else
        {
            this._currentContainer = container;
            string output = "";
            switch (container.opened)
            {
                case DiscoveryStatus.CLOSED:
                        container.opened = DiscoveryStatus.OPENED;
                        output = "\nYou opened the " + container.name + " and it contains the following: ";
                        output += container.getContents();
                        break;
                    
                case DiscoveryStatus.OPENED:
                    output = "You opened the " + container.name + " and it contains the following: ";
                    output += container.getContents();
                    break;
                case DiscoveryStatus.EMPTIED:
                    output = "You open the " + container.name + " and it appears to be empty";
                    break;
            }
            this.outputMessage("\n" + output);
        }
    }

    public string viewInventory()
    {
        string items = "\nInventory: ";
        //Dictionary<string, Item>.KeyCollection keys = inventory.Keys;
        foreach (KeyValuePair <string, Item> item in this.inventory)
        {
            items += " " + item.Value.name + " x" + item.Value.amount;
        }

        return items;
    }

    public void addToInventory(Item item)
    {
        Item temp = null;
        this.inventory.TryGetValue(item.name, out temp);
        if (temp == null)
        {
            this.inventory.Add(item.name, item);
        }
        else
        {
            temp.amount += 1;
            temp.totalWeight += temp.weight;
            this.inventory[temp.name] = temp;
        }
    }

    public void removeFromInventory(string key)
    {
        Item temp = null;
        this.inventory.TryGetValue(key, out temp);
        if (temp == null)
        {
            this.outputMessage("No such item in inventory");
        }
        else
        {
            temp.amount -= 1;
            temp.totalWeight -= temp.weight;
            this._weight -= temp.weight;
            if (temp.amount == 0)
            {
                this.inventory.Remove(key);
            }
            else
            {
                this.inventory[temp.name] = temp;
            }
            
        }
    }

    public void dropFromInventory(string name)
    {
        Item temp = null;
        this.inventory.TryGetValue(name, out temp);
        if (temp == null)
        {
            this.outputMessage("\nNo such item in inventory");
        }
        else if (temp.isQuestItem())
        {
            this.outputMessage("\nCannot drop a quest item");
        }
        else
        {
            if (this._currentContainer == null)
            {
                Item roomItem = null;
                this.currentRoom.Items.TryGetValue(temp.name, out roomItem);
                if (roomItem == null)
                {
                    this._currentRoom.addItem(temp);
                    this._weight -= temp.weight;
                    this.inventory.Remove(temp.name);
                    this.outputMessage("\nItem has been dropped in the current room");
                }
                else
                {
                    roomItem.amount += 1;
                    this._weight -= temp.weight;
                    this.inventory.Remove(temp.name);
                    this._currentRoom.Items[roomItem.name] = roomItem;
                }
            }
            else
            {
                Item containerItem = null;
                this.currentContainer.Contents.TryGetValue(temp.name, out containerItem);
                if (containerItem == null)
                {
                    this.currentContainer.addItem(temp);
                    this.inventory.Remove(temp.name);
                    this._weight -= temp.weight;
                    this.outputMessage("\nItem has been dropped into " + currentContainer.name);
                }
                else
                {
                    containerItem.amount += 1;
                    this.inventory.Remove(temp.name);
                    this._weight -= temp.weight;
                    this.currentContainer.Contents[containerItem.name] = containerItem;
                }
            }
        }
    }

    public bool checkInventoryForItem(string name)
    {
        Item temp = null;
        this.inventory.TryGetValue(name, out temp);
        if (temp == null)
        { 
            return false;
        }
        else
        {
            return true;
        }
    }

    public void takeItem(string itemName)
    {
        //if the player is not at a container, check the room for item
        if(this._currentContainer == null)
        {

            if (this._currentRoom.Items.ContainsKey(itemName))
            {
                Item temp = this._currentRoom.Items[itemName];
                
                //Checks to make sure that taking this item won't overencumber the player
                if (this._weight + temp.weight <= this._maxWeight)
                {
                    if (temp.amount == 1)
                    {
                        //If the item is armor, then replace the player's current armor with it.
                        if (temp.GetType() == typeof(Armor))
                        {
                            this._armor = (Armor)temp;
                        }
                        else
                        {
                            playerPickedUpItem(this.currentRoom.getItem(itemName, this), this);
                            this._weight += this._currentRoom.Items[itemName].weight;
                            this.addToInventory(temp);
                        }
                        this._currentRoom.Items.Remove(itemName);
                    }
                    else
                    {
                        this._currentRoom.Items[itemName].amount -= 1;
                        this._currentRoom.Items[itemName].totalWeight -= temp.weight;
                        Item temp2 = new Item(temp.name, temp.weight);
                        if (temp2.GetType() == typeof(Armor))
                        {
                            this._armor = (Armor)temp2;
                        }
                        else
                        {
                            playerPickedUpItem(this.currentRoom.getItem(itemName, this), this);
                            this._weight += this._currentRoom.Items[itemName].weight;
                            this.addToInventory(temp2);
                        }

                    }
                }
                else
                {
                    this.outputMessage("\nYou do not have space for that.");
                }
            }
            else
            {
                Weapon temp2 = null;
                this.currentRoom.Weapons.TryGetValue(itemName, out temp2);
                if (temp2 != null)
                {
                    switch (temp2.Type)
                    {
                        case WeaponType.FIREARM:
                            this.currentRoom.removeWeapon(itemName);
                            if (this._primary == null)
                            {
                                this._primary = temp2;
                            }
                            else
                            {
                                this._currentRoom.addWeapon(this._primary);
                                this._primary = temp2;
                            }
                            break;

                        case WeaponType.MELEE:
                            this.currentRoom.removeWeapon(itemName);
                            if (this._secondary == null)
                            {
                                this._secondary = temp2;
                            }
                            else
                            {
                                this._currentRoom.addWeapon(this._secondary);
                                this._secondary = temp2;
                            }
                            break;
                    }
                }
                else
                {
                    this.outputMessage("\nNo such item in the room.");
                }
            }
        }
        else
        {
            if (this._currentContainer.checkContainerForItem(itemName))
            {
                Item temp = this._currentContainer.getItem(itemName, this);
                if (this._weight + temp.weight < this._maxWeight)
                {
                    if (temp.amount == 1)
                    {
                        if (temp.GetType() == typeof(Armor))
                        {
                            this._armor = (Armor)temp;
                        }
                        else
                        {
                            playerPickedUpItem(this.currentContainer.getItem(itemName, this), this);
                            this._weight += this._currentContainer.Contents[itemName].weight;
                            this.addToInventory(this._currentContainer.getItem(itemName, this));
                        }
                        this._currentContainer.Contents.Remove(itemName);
                    }
                    else
                    {
                        this._currentContainer.Contents[itemName].amount -= 1;
                        this._currentContainer.Contents[itemName].totalWeight -= _currentContainer.Contents[itemName].weight;
                        Item temp2 = new Item(itemName, _currentContainer.Contents[itemName].weight);
                        if (temp2.GetType() == typeof(Armor))
                        {
                            this._armor = (Armor)temp2;
                        }
                        else
                        {
                            playerPickedUpItem(this.currentRoom.getItem(itemName, this), this);
                            this._weight += this._currentContainer.Contents[itemName].weight;
                            this.addToInventory(temp2);
                        }
                    }
                }
                else
                {
                    this.outputMessage("\nYou do not have space for that.");
                }

            }
            else
            {
                Weapon temp2 = null;
                this.currentContainer.Weapons.TryGetValue(itemName, out temp2);
                if (temp2 != null)
                {
                    switch (temp2.Type)
                    {
                        case WeaponType.FIREARM:
                            this.currentContainer.removeWeapon(itemName);
                            if (this._primary == null)
                            {
                                this._primary = temp2;
                            }
                            else
                            {
                                this._currentContainer.addWeapon(this._primary);
                                this._primary = temp2;
                            }
                            break;

                        case WeaponType.MELEE:
                            this.currentContainer.removeWeapon(itemName);
                            if (this._secondary == null)
                            {
                                this._secondary = temp2;
                            }
                            else
                            {
                                this._currentContainer.addWeapon(this._secondary);
                                this._secondary = temp2;
                            }
                            break;
                    }
                }
                else
                {
                    this.outputMessage("\nNo such item in the container.");
                }
            }
        }
    }

    public void back()
    {
        if (this.travelPath.Count > 1) {
            Dictionary<string, Room> envoy = new Dictionary<string, Room>();
            envoy["beforeRoom"] = this.currentRoom;
            int temp = this.travelPath.LastIndexOf(this.currentRoom);
            this.currentRoom = travelPath[temp - 1];
            envoy["afterRoom"] = this.currentRoom;
            if (this.follower != null)
            {
                this.currentRoom.clearCharacters();
            }
            this.travelPath.Remove(this.travelPath[temp]);
            
            this.outputMessage("\n" + this._currentRoom.description());
            playerEnteredRoom(this, envoy);
        }
        else
        {
            this.outputMessage("\nYou have not gone anywhere yet.");
        }
    }

    public void openDoor(string exitName)
    {
        Door door = this._currentRoom.getExit(exitName);
        if(door != null)
        {
            if (door.lockStrategy == null)
            {
                door.open();
                this.outputMessage("\nThe door is already unlocked");
            }
            else
            {
                door.lockStrategy.doUnlock();
                if (door.lockStrategy.isUnlocked())
                {
                    door.open();
                    this.outputMessage("\nThe door is unlocked.");

                }
                else
                {
                    this.outputMessage("\nThe door is still locked.");
                }
            }
        }
        else
        {
            this.outputMessage("\nThere is no door.");
        }
    }

    public void attackNPC(string name, Weapon weapon)
    {
        if (this._currentRoom.hasEnemies())
        {
            if (this.currentRoom.getEnemy(name) != null)
            {
                this._playerState = PlayerState.FIGHTING;
                if (weapon == null)
                {
                    this.attackPlayer(5, this.currentRoom.getEnemy(name));
                    //this.outputMessage(this.currentRoom.getEnemy(name).damage(5));
                }
                else
                {
                    this.attackPlayer(weapon.Damage, this._currentRoom.getEnemy(name));
                    //this.outputMessage(this.currentRoom.getEnemy(name).damage(weapon.Damage));
                }
            }
            else
            {
                this.outputMessage("\nNo enemies with that name in this room.");
            }
        }
        else
        {
            this.outputMessage("\nThere are no enemies in this room.");
        }
    }

    public void talkTo(Character character)
    {
        if (character != null)
        {
            if (this._currentRoom.characterInRoom(character.name))
            {
                if (this._currentQuest.objective1.Contains(character.name))
                {
                    this._currentQuest.completeObjective(this._currentQuest.objective1);
                    this.outputMessage("\n" + character.giveMessage());
                }
                else
                {
                    this.outputMessage("\n" + character.giveMessage());
                }
            }
            else
            {
                this.outputMessage("\nNo such character in this room.");
            }
        }
        else
        {
            this.outputMessage("\nNo such character in room.");
        }
    }

    public void addQuestline(List<Quest> list)
    {
        this.questLine = list;
        this._currentQuest = questLine[0];
    }

    public void updateQuest()
    {
        if (this._currentQuest.completed)
        {
            this.outputMessage(this.currentQuest.EndMessage);
            this.questLine.RemoveAt(0);
            this.questLine.TrimExcess();
            if (this.questLine.Count != 0)
            {
                this._currentQuest = this.questLine[0];
                this.outputMessage("\nQuest Complete");
                this.checkQuest();
                this.outputMessage(this._currentQuest.ShowQuest());
            }
            else
            {
                this.outputMessage("\nQuest Complete.");
                this.endGame();
            }
        }
    }

    public void checkQuest()
    {
        switch (this.currentQuest.type)
        {
            case QuestType.FIND:
                if (this.checkInventoryForItem(this.currentQuest.obj1RequiredItem.name))
                {
                    this.currentQuest.completeObjective(this._currentQuest.objective1);
                }
                if (this._currentQuest.objective2 != null && this.checkInventoryForItem(this._currentQuest.obj2RequiredItem.name))
                {
                    this.currentQuest.completeObjective(this._currentQuest.objective2);
                }
                if (this._currentQuest.objective3 != null && this.checkInventoryForItem(this._currentQuest.obj3RequiredItem.name))
                {
                    this.currentQuest.completeObjective(this._currentQuest.objective3);
                }
                this.updateQuest();
                break;
            case QuestType.PLANT:
                if (this._currentQuest.objective1.Contains(this._currentRoom.name))
                {
                    this._currentQuest.completeObjective(this._currentQuest.objective1);
                }
                this.updateQuest();
                break;
            case QuestType.FIGHT:
                if (this._currentQuest.objective1.Contains(this._currentRoom.name))
                {
                    this.currentQuest.completeObjective(this._currentQuest.objective1);
                    this._currentRoom.enemySpawnChance = 0;
                    this._currentRoom.getExit("north").getOtherRoom(this._currentRoom).setBarrier(null);
                }
                this.updateQuest();
                break;
            default:
                break;
        } 
    }

    public void startTimer()
    {
        startBombTimer(this);
    }

    public void startHealthMonitor()
    {
        monitorPlayerHealth(this);
    }
    
    public void attackPlayer(int damage, Infected npc)
    {
        if (npc.Name == "infected civilian")
        {
            civilianAttack(damage, this);
        }
        else if(npc.Name == "infected scientist")
        {
            scientistAttack(damage, this);
        }
        else if (npc.Name == "infected soldier")
        {
            soldierAttack(damage, this);
        }
    }

    public bool checkForRecipe(string recipeName)
    {
        Recipe temp = null;
        this.recipes.TryGetValue(recipeName, out temp);
        if (temp != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void giveRecipe(Recipe recipe)
    {
        this.recipes.Add(recipe.Name, recipe);
    }

    public void outputMessage(string message)
    {
        _io.sendLine(message);
    }

    public void takeAmmo(string name)
    {
        Item temp = null;
        if (this._currentContainer == null)
        {
            this._currentRoom.Items.TryGetValue(name, out temp);
            if (temp != null)
            {
                if (this.inventory.ContainsKey(name))
                {
                    this.inventory[name].amount += temp.amount;
                }
                else
                {
                    this.addToInventory((Ammo)temp);
                }
                this._currentRoom.Items.Remove(name);
            }
            else
            {
                this.outputMessage("\nNo ammo in this room.");
            }
        }
        else
        {
            this._currentContainer.Contents.TryGetValue(name, out temp);
            if (temp != null)
            {
                if(this.inventory.ContainsKey(name))
                {
                    this.inventory[name].amount += temp.amount;
                }
                else
                {
                    this.addToInventory((Ammo)temp);
                }
                this._currentContainer.Contents.Remove(name);
            }
            else
            {
                this.outputMessage("\nNo ammo in this container.");
            }
        }
    }

    public void endGame()
    {
        EndGame();
    }
}
