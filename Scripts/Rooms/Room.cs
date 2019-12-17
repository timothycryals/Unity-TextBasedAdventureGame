using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room {

    private string _name;
    public string name { get { return _name; } }

    private Dictionary<string, Door> exits;
    private Dictionary<string, Container> containers;

    private Dictionary<string, Item> items;
    public Dictionary<string, Item> Items { get { return items; } }

    private Barrier barrier;
    public Barrier Barrier { get { return barrier; } }

    private Dictionary<string, Weapon> weapons;
    public Dictionary<string, Weapon> Weapons { get { return weapons; } }

    private Character inhabitant;

    //private List<Infected> enemies;
    private Dictionary<int, Infected> enemies;

    private int _enemySpawnChance;
    public int enemySpawnChance { get { return _enemySpawnChance; } set { _enemySpawnChance = value; } }

    private string _tag;
    public string tag
    {
        get
        {
            return _tag;
        }
        set
        {
            _tag = value;
        }
    }

    public Room() : this("No Name","No Tag", 0)
    {

    }

    public Room(string tag)
    {
        this._name = "Room";
        this.containers = new Dictionary<string, Container>();
        this.weapons = new Dictionary<string, Weapon>();
        this.items = new Dictionary<string, Item>();
        exits = new Dictionary<string, Door>();
        this.tag = tag;
        this.barrier = null;
        this._enemySpawnChance = 0;
        this.enemies = new Dictionary<int, Infected>();
    }

    public Room(string name, string tag, int spawnChance)
    {
        this._name = name;
        this.containers = new Dictionary<string, Container>();
        this.items = new Dictionary<string, Item>();
        this.weapons = new Dictionary<string, Weapon>();
        exits = new Dictionary<string, Door>();
        this.tag = tag;
        this.barrier = null;
        this._enemySpawnChance = spawnChance;
        this.enemies = new Dictionary<int, Infected>();
    }

    public Room(string name, string tag, int spawnChance, Barrier barrier)
    {
        this._name = name;
        this.containers = new Dictionary<string, Container>();
        this.items = new Dictionary<string, Item>();
        this.weapons = new Dictionary<string, Weapon>();
        exits = new Dictionary<string, Door>();
        this.tag = tag;
        this.barrier = barrier;
        this.enemySpawnChance = spawnChance;
        this.enemies = new Dictionary<int, Infected>();
    }

    public void setBarrier(Barrier barrier)
    {
        this.barrier = barrier;
    }

    public bool hasBarrier()
    {
        if (this.barrier == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public BarrierCondition getBarrierState()
    {
        return this.barrier.GetBarrierCondition();
    }

    public void setExit(string exitName, Door door)
    {
        exits[exitName] = door;
    }

    public Door getExit(string exitName)
    {
        Door door = null;
        exits.TryGetValue(exitName, out door);
        return door;
    }

    public string getExits()
    {
        string exitNames = "Exits: ";
        Dictionary<string, Door>.KeyCollection keys = exits.Keys;
        foreach(string exitName in keys)
        {
            exitNames += " " + exitName;
        }

        return exitNames;
    }

    public Container getContainer(string containerName)
    {
        Container con = null;
        containers.TryGetValue(containerName, out con);


        return con;
    }

    public void addItemToContainer(Item item, string containerName)
    {
        foreach(string container in this.containers.Keys)
        {
            if(container == containerName)
            {
                this.containers[container].addItem(item);
            }
        }
    }

    public string getContainers()
    {
        string containerNames = "Containers: ";
        Dictionary<string, Container>.KeyCollection keys = containers.Keys;
        foreach(string containerName in keys)
        {
            containerNames += " " + containerName + ",";
        }

        return containerNames;
    }

    public void addContainer(Container c)
    {
        this.containers.Add(c.name, c);
    }

    public void addItem(Item i)
    {
        //this.items.Add(i.name, i);
        Item roomItem = null;
        this.items.TryGetValue(i.name, out roomItem);
        if (roomItem == null)
        {
            this.items.Add(i.name, i);
        }
        else
        {
            roomItem.amount += 1;
            this.items[roomItem.name] = roomItem;
        }
    }
    public string getItems()
    {
        string itemNames = "Items: ";
        Dictionary<string, Item>.KeyCollection keys = items.Keys;
        foreach (string itemName in keys)
        {
            itemNames += " " + itemName + "(" + this.items[itemName].weight + "lbs)" + " x" + this.items[itemName].amount + ",";
        }

        Dictionary<string, Weapon>.KeyCollection weaponKeys = weapons.Keys;
        foreach (string weaponName in weaponKeys)
        {
            itemNames += " " + weaponName + ",";
        }

        return itemNames;
    }

    public Item getItem(string itemName, Player p)
    {
        Item item = null;
        this.items.TryGetValue(itemName, out item);
        if(item == null)
        {
            p.outputMessage("\nNo such item in the room");
        }
        else
        {
            //p.addToInventory(item);
            p.outputMessage("\nYou picked up: " + itemName);
        }
        return item;
    }

    public bool checkRoomForItem(string item)
    {
        Item temp = null;
        this.items.TryGetValue(item, out temp);
        if (temp == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public bool checkContainersForItem(string item)
    {
        foreach ( string c in this.containers.Keys)
        {
            Container con = this.getContainer(c);
            if (con.checkContainerForItem(item))
            {
                return true;
            }
            else
            {
                continue;
            }
        }
        return false;
    }

    public void addEnemyToRoom(int key, Infected npc)
    {
        this.enemies.Add(key, npc);
    }

    public void addCharacterToRoom(Character c)
    {
        this.inhabitant = c;
    }

    public bool characterInRoom(string Name)
    {
        if (this.inhabitant != null && Name == this.inhabitant.name)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public Character getCharacter(string Name)
    {
        if (this.inhabitant != null && Name == this.inhabitant.name)
        {
            return this.inhabitant;
        }
        else
        {
            return null;
        }
    }

    public void addEnemies(Dictionary<int, Infected> npcs)
    {
        this.enemies = npcs;
    }

    public bool hasEnemies()
    {
        if (this.enemies != null)
        {
            if(this.enemies.Count != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public Infected getEnemy(string name)
    {
        if (this.hasEnemies())
        {
            
            foreach (int npc in this.enemies.Keys)
            {
                if (enemies[npc].Name == name && enemies[npc].State == HealthState.ALIVE)
                {
                    return enemies[npc];
                }
                else if(enemies[npc].Name == name && enemies[npc].State == HealthState.DEAD)
                {
                    continue;
                }
                else
                {
                    continue;
                }
            }
            return null;
        }
        else
        {
            return null;
        }
    }

    public Infected getFirstEnemy()
    {
        foreach(Infected npc in this.enemies.Values)
        {
            return npc;
        }
        return null;
    }

    public string enemiesToString()
    {
        string temp = "";
        foreach (int npc in this.enemies.Keys)
        {
            if (enemies[npc].State != HealthState.DEAD)
            {
                temp += enemies[npc].Name + ", ";
            }
        }
        if (temp == "")
        {
            this.enemies = new Dictionary<int, Infected>();
        }
        return temp;
    }

    public void clearEnemies()
    {
        this.enemies.Clear();
    }

    public void clearCharacters()
    {
        this.inhabitant = null;
    }

    public void alertEnemies(Player player)
    {
        int alertChance = Random.Range(1,4);
        if (alertChance == 1 && this.enemies != null && this.enemies.Count != 0)
        {
            foreach (int npc in this.enemies.Keys)
            {
                if (enemies[npc].State == HealthState.ALIVE)
                {
                    player.outputMessage("\nThe enemies in the room are attacking you;");
                    enemies[npc].attack(player);
                    break;
                }
            }
        }
        else if (alertChance != 1 && this.enemies != null && this.enemies.Count != 0)
        {
            player.outputMessage("\nYou enter the room, but the enemies inside haven't noticed you yet.");
        }
        else
        {
            player.outputMessage("");
        }
    }

    public void addWeapon(Weapon weapon)
    {
        this.weapons.Add(weapon.Name, weapon);
    }

    public void removeWeapon(string name)
    {
        this.weapons.Remove(name);
    }

    public string description()
    {
        if (this.inhabitant == null)
        {
            return "\nYou are " + this.tag + ".\n *** " + this.getExits() + "\n *** " + this.getContainers()
                + "\n *** " + this.getItems();
        }
        else
        {
            return "\nYou are " + this.tag + "\n" + this.inhabitant.giveMessage() + "\n * **" + this.getExits() + "\n * **" + this.getContainers()
                + "\n *** " + this.getItems();
        }
    }
}
