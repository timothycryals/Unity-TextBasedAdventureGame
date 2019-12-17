using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct roomStructure
{
    public Room entrance;
    public Room exit;
}

public class GameWorld {

    private static GameWorld _instance = null;
    public static GameWorld instance
    {
        get
        {
            if (_instance == null) {
                _instance = new GameWorld();
            }

            return _instance;
        }
    }

    private Room _entrance;
    public Room entrance { get { return _entrance; } }
    private Room _exit;
    public Room exit { get { return _exit; } }

    private InfectedFactory _spawner;
    public InfectedFactory spawner { get { return _spawner; } }
    

    public GameWorld()
    {
        _spawner = new InfectedFactory();
        roomStructure rooms = createWorld();
        _entrance = rooms.entrance;
        _exit = rooms.exit;
        Player.playerEnteredRoom += playerEnteredRoom;
        Player.playerPickedUpItem += playerPickedUpItem;
    }



    public void playerEnteredRoom(Player player, Dictionary<string, Room> envoy)
    {
        
        Room room = null;
        envoy.TryGetValue("afterRoom", out room);
        if (room != null)
        {
            int spawnChance = Random.Range(1, 100);
            switch (room.name)
            {
                case "military base":
                    if ((spawnChance <= room.enemySpawnChance && room.enemySpawnChance != 0) && room.hasEnemies() == false)
                    {
                        room.addEnemyToRoom(spawnChance, this._spawner.getInfected("infected soldier"));
                    }
                    break;
                case "front gate":
                    if ((spawnChance <= room.enemySpawnChance && room.enemySpawnChance != 0) && room.hasEnemies() == false)
                    {
                        int spawn = Random.Range(1, 2);
                        switch (spawn)
                        {
                            case 1:
                                room.addEnemyToRoom(spawnChance, this._spawner.getInfected("infected civilian"));
                                break;
                            case 2:
                                room.addEnemyToRoom(spawnChance, this._spawner.getInfected("infected soldier"));
                                break;
                        }
                    }
                    break;
                case "armory":
                    if((spawnChance <= room.enemySpawnChance && room.enemySpawnChance != 0) && room.hasEnemies() == false)
                    {
                        room.addEnemyToRoom(spawnChance, this._spawner.getInfected("infected soldier"));
                    }
                    break;
                case "laboratory":
                    if ((spawnChance <= room.enemySpawnChance && room.enemySpawnChance != 0) && room.hasEnemies() == false)
                    {
                        room.addEnemyToRoom(spawnChance, this._spawner.getInfected("infected scientist"));
                    }
                    break;
                default:
                    if ((spawnChance <= room.enemySpawnChance && room.enemySpawnChance != 0) && room.hasEnemies() == false)
                    {
                        room.addEnemyToRoom(spawnChance, this._spawner.getInfected("infected civilian"));
                    }
                    break;

            }

            if ((player.currentQuest.objective1.Contains(room.name)) && (player.currentQuest.type == QuestType.GO))
            {
                player.currentQuest.completeObjective(player.currentQuest.objective1);
                player.outputMessage("\nYou have entered your destination.");
                if (room.hasEnemies())
                {
                    player.outputMessage("\nThe room contains: " + room.enemiesToString());
                    room.alertEnemies(player);
                }
            }
            else
            {
                if (room.hasEnemies())
                {
                    player.outputMessage("\nThe room contains: " + room.enemiesToString());
                    room.alertEnemies(player);
                }
            }
        }
        else
        {
            player.outputMessage("\nNo room");
        }

        if (room.name == this.exit.name)
        {
            player.endGame();
        }
    }

    public void playerPickedUpItem(Item item, Player p)
    {
        if (p.currentQuest.type == QuestType.FIND && p.currentQuest != null)
        {
            if (item.name == p.currentQuest.obj1RequiredItem.name)
            {
                p.currentQuest.completeObjective(p.currentQuest.objective1);
                p.outputMessage("\nPicked up a quest item");
            }
            else if (item.name == p.currentQuest.obj2RequiredItem.name)
            {
                p.currentQuest.completeObjective(p.currentQuest.objective2);
                p.outputMessage("\nPicked up a quest item");
            }
            else if (item.name == p.currentQuest.obj3RequiredItem.name)
            {
                p.currentQuest.completeObjective(p.currentQuest.objective3);
                p.outputMessage("\nPicked up a quest item");
            }
        }
    }

    private roomStructure createWorld()
    {
        Barrier standardBarrier = new Barrier();

        Room shelter = new Room("shelter", "in your shelter inside an old apartment building.", 0);
        Room outsideShelter = new Room("outside shelter", "outside of your shelter. Everything looks clear for now.", 0);

        Room intersection = new Room("shopping center intersection", "standing in the middle of any intersection. There are vandalized cars lining the roads.", 0);

        Room shoppingCenterParkingLot = new Room("shopping center parking lot", "you are standing in the parking lot of an old shopping center.", 20);
        Room retailStore = new Room("retail store", "inside of a retail store. Most of the aisles have been picked clean.", 30);
        Room hardwareDepartment = new Room("hardware department", "in the hardware department of the store.", 10);
        Room pharmacyDepartment = new Room("pharmacy department", "in the pharmacy department of the store.", 10);
        Room gunStore = new Room("gun store", "in a ransacked gun store. Mostly everything has been looted or destroyed." +
                                  " There are multiple containers around that may have not been checked.", 20);
        Room electronicsDepartment = new Room("electronics department", "in the electronics department. There's equipment scattered all over the place.", 10);

        Room gasStation = new Room("gas station", "at a gas station. There are a few cars around you could search. There is also an auto shop to the north that appears to have some barricades on it as well as a junkyard to the east.", 10);
        Room convenienceStore = new Room("convenience store", "inside of a convenience store.", 0);

        Room autoShopStreet = new Room("auto shop street","outside of the auto shop. It is surrounded by a chain-link fence. Perhaps boltcutters could get you through.", 20);
        Room autoShop = new Room("auto shop", "inside of the auto shop.", 0, new Fence());

        Room junkyard = new Room("junkyard", "in the junkyard. Perhaps you can make something out of the stuff here.", 30, new Fence());


        Room officeStreet = new Room("street in front of office", "standing on a street in front of an old office building.", 20);
        Room groundFloorOffice = new Room("office ground floor", "on the ground floor of an old office building. The stairs going to the next floor appear to be barricaded, but there is a small gap you can crawl through.", 10);
        Room secondFloorOffice = new Room("office second floor", "on the second floor of the office building.", 0);

        Room downTownIntersection = new Room("downtown intersection", "standing in the middle of the downtown intersection.", 20);

        Room outsideMilitaryBase = new Room("the outside of the military base", "outside the old military base. The area contains a higher concentration of infected people" +
                                     " than most other places within the city.\n(Advised: You may want to find weapons first)", 15);

        Room militaryBase = new Room("military base", "inside the military base. There is a small navigational chart to help you find your way. To the west is the research facility and to the east is the ordinance wing. To the north you will find the Armory.", 60, new Fence());
        Room armory = new Room("armory", "in the armory of the military base", 20);
        Room laboratory = new Room("laboratory", "in a miltary laboratory. Perhaps you will find something useful in here.", 50);
        Room barracks = new Room("barracks", "in the military barracks.", 0);

        Room streetFrontGate = new Room("street next to front gate", "by the wall's front gate. There are lots of infected in your path.\n(Advised: You may want to have weapons, armor, and backup before proceding)", 0);
        Room frontGate = new Room("front gate", "at the front gate. You must clear out all the enemies to get escape", 100);
        Room wall = new Room("the wall", "next to quarantine wall. You notice a weak spot in the wall that could easily be destroyed by explosives.", 0);
        Room outsideWall = new Room("the outside of the wall", "outside of the quarantine wall.", 0, new Barrier());

        Door door = Door.connectRooms(shelter, "west", outsideShelter, "east");
        door = Door.connectRooms(outsideShelter, "north", intersection, "south");
        door = Door.connectRooms(intersection, "west", shoppingCenterParkingLot, "east");
        door = Door.connectRooms(shoppingCenterParkingLot, "south", gunStore, "north");
        door = Door.connectRooms(shoppingCenterParkingLot, "west", retailStore, "east");
        door = Door.connectRooms(retailStore, "north", hardwareDepartment, "south");
        door = Door.connectRooms(retailStore, "west", pharmacyDepartment, "east");
        door = Door.connectRooms(retailStore, "south", electronicsDepartment, "north");
        door = Door.connectRooms(intersection, "east", gasStation, "west");
        door = Door.connectRooms(gasStation, "south", convenienceStore, "north");
        door = Door.connectRooms(gasStation, "north", autoShopStreet, "south");
        door = Door.connectRooms(autoShopStreet, "north", autoShop, "south");
        door = Door.connectRooms(gasStation, "east", junkyard, "west");
        door = Door.connectRooms(intersection, "north", officeStreet, "south");
        door = Door.connectRooms(officeStreet, "west", groundFloorOffice, "east");
        door = Door.connectRooms(groundFloorOffice, "up", secondFloorOffice, "down");
        door = Door.connectRooms(officeStreet, "north", downTownIntersection, "south");
        door = Door.connectRooms(downTownIntersection, "west", wall, "east");
        door = Door.connectRooms(wall, "west", outsideWall, "east");
        door = Door.connectRooms(downTownIntersection, "east", outsideMilitaryBase, "west");
        door = Door.connectRooms(outsideMilitaryBase, "east", militaryBase, "west");
        door = Door.connectRooms(militaryBase, "east", armory, "west");
        door = Door.connectRooms(militaryBase, "north", laboratory, "south");
        door = Door.connectRooms(militaryBase, "south", barracks, "north");
        door = Door.connectRooms(downTownIntersection, "north", streetFrontGate, "south");
        door = Door.connectRooms(streetFrontGate, "north", frontGate, "south");
        door = Door.connectRooms(frontGate, "north", outsideWall, "south");

        militaryBase.setBarrier(new Fence());

        roomStructure output;

        output.exit = outsideWall;
        output.entrance = shelter;

        shelter.addContainer(new Container("dresser"));
        shelter.addContainer(new Container("cabinet"));
        shelter.addItemToContainer(new Consumable("chips", 2, 20), "cabinet");
        shelter.addItemToContainer(new Item("fabric", 1), "dresser");
        shelter.addWeapon(new Knife(WeaponCondition.FAIR));
        shelter.addItem(new Consumable("medkit", 4, 50));
        shelter.addWeapon(new BaseballBat(WeaponCondition.GOOD));

        gunStore.addContainer(new Container("ammo box"));
        gunStore.addContainer(new Container("cardboard box"));
        gunStore.addContainer(new Container("drum"));
        gunStore.addItemToContainer(new Item("gunpowder", 1), "drum");
        gunStore.addItemToContainer(new Item("gunpowder", 1), "drum");
        gunStore.addItemToContainer(new Item("gunpowder", 1), "drum");
        gunStore.addItemToContainer(new Item("gunpowder", 1), "drum");
        gunStore.addItemToContainer(new Item("gunpowder", 1), "drum");
        gunStore.addItemToContainer(new Item("repair kit", 5), "cardboard box");
        gunStore.addItemToContainer(new Ammo("shotgun bullet", 0), "ammo box");
        gunStore.addItemToContainer(new Ammo("pistol bullet", 0), "ammo box");
        gunStore.addWeapon(new Shotgun(WeaponCondition.GOOD));
        gunStore.addWeapon(new Pistol(WeaponCondition.GOOD));
        gunStore.addItem(new Armor("armor", 0, ArmorCondition.MODERATE));

        intersection.addContainer(new Container("blue sedan"));
        intersection.addContainer(new Container("flipped car"));
        intersection.addContainer(new Container("white truck"));
        intersection.addItemToContainer(new Item("plastic", 3), "blue sedan");
        intersection.addItemToContainer(new Item("fabric", 1), "blue sedan");
        intersection.addItemToContainer(new Item("fabric", 1), "white truck");
        intersection.addItemToContainer(new Item("car battery", 10), "flipped car");

        shoppingCenterParkingLot.addContainer(new Container("red van"));
        shoppingCenterParkingLot.addItemToContainer(new Item("spark plug", 4), "red van");
        shoppingCenterParkingLot.addContainer(new Container("rusty truck"));
        shoppingCenterParkingLot.addItemToContainer(new Consumable("chips", 2, 20), "rusty truck");
        shoppingCenterParkingLot.addItemToContainer(new Ammo("shotgun bullet", 0), "rusty truck");

        retailStore.addContainer(new Container("cardboard box"));
        retailStore.addItemToContainer(new Consumable("soup", 3, 30), "cardboard box");
        retailStore.addItemToContainer(new Consumable("soup", 3, 30), "cardboard box");
        pharmacyDepartment.addItem(new Consumable("painkiller", 2, 25));
        pharmacyDepartment.addItem(new Consumable("painkiller", 2, 25));
        pharmacyDepartment.addItem(new Consumable("medkit", 4, 50));
        pharmacyDepartment.addItem(new Consumable("bandage", 2, 20));

        hardwareDepartment.addContainer(new Container("aisle"));
        hardwareDepartment.addItemToContainer(new Item("boltcutters", 8), "aisle");
        hardwareDepartment.addItem(new Item("duct tape", 1));
        hardwareDepartment.addItem(new Item("duct tape", 1));
        hardwareDepartment.addItem(new Item("duct tape", 1));
        hardwareDepartment.addItem(new Item("pipe", 4));
        hardwareDepartment.addItem(new Item("pipe", 4));

        downTownIntersection.addContainer(new Container("ambulance"));
        downTownIntersection.addContainer(new Container("police car"));
        downTownIntersection.addItemToContainer(new Consumable("medkit", 4, 50), "ambulance");
        downTownIntersection.addItemToContainer(new Consumable("medkit", 4, 50), "ambulance");
        downTownIntersection.addItemToContainer(new Consumable("medkit", 4, 50), "ambulance");
        downTownIntersection.addItemToContainer(new Ammo("rifle bullet", 0), "police car");
        downTownIntersection.addItemToContainer(new Ammo("shotgun bullet", 0), "police car");
        downTownIntersection.addItemToContainer(new Ammo("pistol bullet", 0), "police car");
        downTownIntersection.addItemToContainer(new Armor("armor", 0, ArmorCondition.MODERATE), "police car");

        electronicsDepartment.addItem(new Item("car battery", 10));
        electronicsDepartment.addItem(new Item("electronics", 3));
        electronicsDepartment.addItem(new Item("electronics", 3));

        secondFloorOffice.addContainer(new Container("ammo box"));
        secondFloorOffice.addItemToContainer(new Ammo("rifle bullet", 0), "ammo box");
        secondFloorOffice.addItemToContainer(new Ammo("pistol bullet", 0), "ammo box");
        secondFloorOffice.addItemToContainer(new Ammo("shotgun bullet", 0), "ammo box");
        secondFloorOffice.addItem(new Item("repair kit", 5));
        secondFloorOffice.addItem(new Item("repair kit", 5));


        gasStation.addItem(new Item("gas can", 10));
        gasStation.addItem(new Item("gas can", 10));

        convenienceStore.addItem(new Consumable("chips", 2, 20));
        convenienceStore.addItem(new Consumable("chips", 2, 20));
        convenienceStore.addItem(new Consumable("chips", 2, 20));
        convenienceStore.addItem(new Consumable("bandage", 2, 20));
        convenienceStore.addItem(new Consumable("bandage", 2, 20));
        convenienceStore.addItem(new Item("duct tape", 1));

        laboratory.addItem(new Item("rdx", 4));
        laboratory.addItem(new Item("rdx", 4));

        armory.addContainer(new Container("ammo crate"));
        armory.addContainer(new Container("ammo box"));
        armory.addContainer(new Container("explosives crate"));
        armory.addItemToContainer(new Item("grenade", 5), "explosives crate");
        armory.addItemToContainer(new Item("grenade", 5), "explosives crate");
        armory.addItemToContainer(new Item("grenade", 5), "explosives crate");
        armory.addItemToContainer(new Item("fuse", 1), "explosives crate");
        armory.addItemToContainer(new Item("fuse", 1), "explosives crate");
        armory.addItemToContainer(new Item("fuse", 1), "explosives crate");
        armory.addItemToContainer(new Item("fuse", 1), "explosives crate");
        armory.addItemToContainer(new Item("fuse", 1), "explosives crate");
        armory.addItemToContainer(new Ammo("rifle bullet", 0), "ammo crate");
        armory.addItemToContainer(new Ammo("shotgun bullet", 0), "ammo crate");
        armory.addItemToContainer(new Ammo("pistol bullet", 0), "ammo crate");
        armory.addItemToContainer(new Ammo("rifle bullet", 0), "ammo box");
        armory.addItemToContainer(new Ammo("shotgun bullet", 0), "ammo box");
        armory.addItemToContainer(new Ammo("pistol bullet", 0), "ammo box");
        armory.addItem(new Armor("armor", 0, ArmorCondition.GOOD));



        junkyard.addItem(new Item("scrap metal", 5));
        junkyard.addItem(new Item("scrap metal", 5));
        junkyard.addItem(new Item("scrap metal", 5));
        junkyard.addItem(new Item("scrap metal", 5));
        junkyard.addItem(new Item("scrap metal", 5));
        junkyard.addItem(new Item("plastic", 3));
        junkyard.addItem(new Item("plastic", 3));
        junkyard.addItem(new Item("plastic", 3));
        junkyard.addItem(new Item("pipe", 3));
        junkyard.addItem(new Item("pipe", 3));
        junkyard.addItem(new Item("pipe", 3));
        junkyard.addItem(new Item("fabric", 1));
        junkyard.addItem(new Item("fabric", 1));
        junkyard.addItem(new Item("electronics", 3));
        junkyard.addItem(new Item("electronics", 3));
        

        Dictionary<int, Infected> enemies = new Dictionary<int, Infected>();
        enemies.Add(1, _spawner.getInfected("infected soldier"));

        frontGate.addEnemyToRoom(1, _spawner.getInfected("infected soldier"));
        militaryBase.addEnemyToRoom(1, _spawner.getInfected("infected soldier"));
        laboratory.addEnemyToRoom(1, _spawner.getInfected("infected scientist"));


        Weapon newRifle = new Rifle(WeaponCondition.GOOD);

        Character ext = new Character("explosives expert", newRifle, "There is a man that explains that he was an explosives expert in the military before the outbreak and asks for your help on getting out of the quarantine zone.\nWill you <b>accept</b> his help?");
        Character soldier = new Character("former soldier", newRifle, "There is a man that explains that he is a former soldier and has been plotting to try and escape throught the front gate of the quarantine zone.\nWill you <b>accept</b> his help?");
        Character scav = new Character("scavenger", newRifle, "There is a man that explains that he is a scavenger and is working on a custom vehicle that can break through the wall's front gate.\nWill you <b>accept</b> his help?");
        barracks.addCharacterToRoom(ext);
        autoShop.addCharacterToRoom(scav);
        secondFloorOffice.addCharacterToRoom(soldier);

        return output;
    }
}
